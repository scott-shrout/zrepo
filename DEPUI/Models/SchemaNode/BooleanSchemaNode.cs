using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public class BooleanSchemaNode : SchemaNode, IHasSpecificationTypeName
    {
        public string? SpecificationTypeName => "boolean";
        public BooleanSchemaNode() { }
        public override string? TypeDisplayName => "Boolean";
        public BooleanSchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertiesValueObject, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name,parent, propertiesValueObject)
        {
            
        }

        public override IDictionary<string, object?> GetAvroObject()
        {
            return new Dictionary<string, object?>
            {
                 { "default", null },
                {"type",new string[] { "null","boolean" } },
                {"name",Name }
            };
        }
    }
}
