using DEPUI.Models.SchemaNode;
using DEPUI.Processing.SchemaTools.SchemaMessageTypes;

namespace DEPUI.Processing.SchemaTools.SchemaTypes
{
    public abstract class ApiSchema
    {
        public abstract IList<string>? MessageTypes { get;  }
        protected IDictionary<string, object?>? value { get; set; }

        public ApiSchema(IDictionary<string, object?>? value)
        {
            this.value = value;
        }

        public ApiSchema() { }

        public abstract SchemaMessageType GetMessageType(string messageTypeName);
        protected abstract IDictionary<string, object?> GetSchemaObject(IDictionary<string, object?> input);

        protected Models.SchemaNode.SchemaNode[] GetObjectValue(IDictionary<string, object?> inputObject, Models.SchemaNode.SchemaNode? parent)
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

            if (inputObject.TryGetValue("oneOf", out var oneOf) && oneOf is object[] objectArrayOneOfValue)
            {
                var oneOfObjects = objectArrayOneOfValue.Cast<IDictionary<string, object?>>()
                    .Select((item, index) => new { sequenceNumber = index, currentSchemaNodes = GetObjectValue(item, null) })
                    .SelectMany(item => item.currentSchemaNodes, (item, groupingItem) => new { item.sequenceNumber, groupingItem })
                     .GroupBy(item => item.groupingItem.Name, (name, items) => new { field = name, items = items.Select(item => new OneOfObjectGroupingItem() { SequenceNumber = item.sequenceNumber, Value = item.groupingItem }).ToArray() })
                    .Select(item => new OneOfSchemaNode(item.field!, parent!, item.items, GetObjectValue));
                schemaNodes.AddRange(oneOfObjects);
            }

            return schemaNodes.ToArray();
        }

        protected IList<Models.SchemaNode.SchemaNode> GetPropertiesResult(IDictionary<string, object?> inputObject, Models.SchemaNode.SchemaNode? parent)
        {
            var schemaNodes = new List<Models.SchemaNode.SchemaNode>();
            foreach (var property in inputObject)
            {
                if (property.Value is IDictionary<string, object?> propertiesValueDictionary)
                {
                    var schemaObject = GetSchemaObject(propertiesValueDictionary);
                    var newSchemaNode = Models.SchemaNode.SchemaNode.FromPropertySpecification(property.Key, parent, schemaObject, GetObjectValue);
                    if (newSchemaNode != null)
                    {
                        schemaNodes.Add(newSchemaNode);
                    }
                }
            }

            return schemaNodes;
        }


        protected IList<Models.SchemaNode.SchemaNode> GetChildPropertyResult(object[] objectArrayAllOfValue, Models.SchemaNode.SchemaNode? parent)
        {
            var schemaNodes = new List<Models.SchemaNode.SchemaNode>();
            foreach (var allOfObject in objectArrayAllOfValue)
            {
                
                if (allOfObject is IDictionary<string, object?> propertiesValueDictionary)
                {
                    var propertiesObject = GetSchemaObject(propertiesValueDictionary);
                 
                    schemaNodes.AddRange(GetObjectValue(propertiesObject, parent));
                }
            }

            return schemaNodes;
        }
    }
}
