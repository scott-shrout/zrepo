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

        protected override IDictionary<string, object?> GetSchemaObject(IDictionary<string, object?> input) => input;
       
    }
}
