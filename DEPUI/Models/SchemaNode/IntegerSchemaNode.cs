using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public class IntegerSchemaNode : SchemaNode, IHasSpecificationTypeName
    {
        public string? SpecificationTypeName => "integer";
        public IntegerSchemaNode() { }
        public override string? TypeDisplayName => "Integer";
        public IntegerSchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertiesValueObject, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name, parent, propertiesValueObject) { }

        public override IDictionary<string, object?> GetAvroObject()
        {
            return new Dictionary<string, object?>
            {
                 { "default", null },
                {"type",new string[] { "null","int" } },
                {"name",Name }
            };
        }
    }
}
