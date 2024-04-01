using DEPUI.Processing.SchemaTools.SchemaMessageTypes;
using DEPUI.Extensions;
using DEPUI.Models.SchemaNode;
using DEPUI.Processing.SchemaTools.SchemaNode;
namespace DEPUI.Processing.SchemaTools.SchemaTypes
{
    public class AsyncApiSchema : ApiSchema
    {
        public AsyncApiSchema(IDictionary<string, object?>? value) : base(value)
        {
        }

        public override IList<string>? MessageTypes => (value?.GetNestedValue("components.messages") as IDictionary<string,object?>)?.Keys.ToArray();

        public override SchemaMessageType GetMessageType(string messageTypeName)
        {
            var payloadPath = $"components.messages.{messageTypeName}.payload";
            var payloadObject = value!.GetNestedValue(payloadPath) as IDictionary<string,object?>;
            var baseNode = new ObjectSchemaNode();
            var children = GetObjectValue(payloadObject!, baseNode);
            baseNode.Children = children;
            return new SchemaMessageType(baseNode);
        }

        private Models.SchemaNode.SchemaNode[] GetObjectValue(IDictionary<string,object?> inputObject, Models.SchemaNode.SchemaNode parent)
        {
            var schemaNodes = new List<Models.SchemaNode.SchemaNode>();
            if (inputObject.TryGetValue("properties", out var propertyValue) && propertyValue is IDictionary<string, object?> propertyDictionaryValue)
            {
                schemaNodes.AddRange(GetPropertiesResult(propertyDictionaryValue, parent));
            }

            if (inputObject.TryGetValue("allOf", out var allOfValue) && allOfValue is object[] objectArrayAllOfValue)
            {
                schemaNodes.AddRange(GetChildPropertyResult(objectArrayAllOfValue, parent));
            }

           

            return schemaNodes.ToArray();
        }

        private IList<Models.SchemaNode.SchemaNode> GetPropertiesResult(IDictionary<string, object?> inputObject, Models.SchemaNode.SchemaNode parent)
        {
            var schemaNodes = new List<Models.SchemaNode.SchemaNode>();
            foreach (var property in inputObject)
            {
                if (property.Value is IDictionary<string, object?> propertiesValueDictionary)
                {
                    var newSchemaNode = Models.SchemaNode.SchemaNode.FromPropertySpecification(property.Key, parent, propertiesValueDictionary, GetObjectValue);
                    if (newSchemaNode != null)
                    {
                        schemaNodes.Add(newSchemaNode);
                    }
                }
            }

            return schemaNodes;
        }


        private IList<Models.SchemaNode.SchemaNode> GetChildPropertyResult(object[] objectArrayAllOfValue, Models.SchemaNode.SchemaNode parent)
        {
            var schemaNodes = new List<Models.SchemaNode.SchemaNode>();
            foreach(var allOfObject in  objectArrayAllOfValue)
            {
                if (allOfObject is IDictionary<string, object?> propertiesValueDictionary)
                {
                    schemaNodes.AddRange(GetObjectValue(propertiesValueDictionary,parent));
                }
            }

            return schemaNodes;
        }

        

    }
}
