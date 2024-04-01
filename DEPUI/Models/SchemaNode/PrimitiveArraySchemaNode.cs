namespace DEPUI.Models.SchemaNode
{
    public class PrimitiveArraySchemaNode : ArraySchemaNode
    {
        public override string? TypeDisplayName => "Array (String)";
        public PrimitiveArraySchemaNode() { }
        public PrimitiveArraySchemaNode(string name, SchemaNode parent,  IDictionary<string, object?> propertiesValueObject, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name, parent, propertiesValueObject) 
        {
            
        
        }

        public override IDictionary<string, object?> GetAvroObject()
        {
            return new Dictionary<string, object?>
            {
                 { "default", null },
                {"type",new object[] { "null",
                    new Dictionary<string, object?> {
                        {"type" , "array" },
                        {"items", "string" }
                
                } } },
                {"name",Name }
            };
        }
    }
}
