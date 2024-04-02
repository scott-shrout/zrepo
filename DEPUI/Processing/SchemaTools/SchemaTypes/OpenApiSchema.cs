using DEPUI.Extensions;
using DEPUI.Models.SchemaNode;
using DEPUI.Processing.SchemaTools.SchemaMessageTypes;

namespace DEPUI.Processing.SchemaTools.SchemaTypes
{
    public class OpenApiSchema : ApiSchema
    {

        public OpenApiSchema(IDictionary<string, object?>? value) : base(value)
        {
        }

        public override IList<string> MessageTypes => (value!.GetNestedValue("paths") as Dictionary<string, object?>)!.Keys.ToArray();

        public override SchemaMessageType GetMessageType(string messageTypeName)
        {
            var schemaBaseRef = value!.GetNestedValue($"paths.{messageTypeName}.post.requestBody.content.application/json.schema.$ref") as string;
            var schemaBase = GetSchemaByRef(schemaBaseRef!);
            
            var baseNode = new ObjectSchemaNode();
            baseNode.Children = GetObjectValue(schemaBase, baseNode);
            return new SchemaMessageType(baseNode);
        }

        private IDictionary<string, object?>? GetSchemaByRef(string refValue)
        {
            var refPath = refValue.Substring(2).Replace("/", ".");
            return value!.GetNestedValue(refPath) as IDictionary<string, object?>;
        }

        protected override IDictionary<string, object?> GetSchemaObject(IDictionary<string, object?> input)
        {
            if (input.ContainsKey("$ref"))
            {
                var refPath = input["$ref"]!.ToString()!.Substring(2).Replace("/", ".");
                return (value!.GetNestedValue(refPath) as IDictionary<string, object?>)!;
            }
            return input;
        }

    }
}
