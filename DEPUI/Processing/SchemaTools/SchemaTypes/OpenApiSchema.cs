using DEPUI.Processing.SchemaTools.SchemaMessageTypes;

namespace DEPUI.Processing.SchemaTools.SchemaTypes
{
    public class OpenApiSchema : ApiSchema
    {

        public OpenApiSchema(IDictionary<string, object?>? value) : base(value)
        {
        }

        public override IList<string> MessageTypes => throw new NotImplementedException();

        public override SchemaMessageType GetMessageType(string messageTypeName)
        {
            throw new NotImplementedException();
        }
    }
}
