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
        

    }
}
