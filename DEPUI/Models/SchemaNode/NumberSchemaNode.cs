using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public class NumberSchemaNode : SchemaNode, IHasSpecificationTypeName
    {
        public string? SpecificationTypeName => "number";
        public NumberSchemaNode() { }
        public override string? TypeDisplayName => "Number";
        public NumberSchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertiesValueObject, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name,parent,propertiesValueObject) { }
   
        public override IDictionary<string, object?> GetAvroObject()
        {
            return new Dictionary<string, object?>
            {
                {"type","int" },
                {"name",Name }
            };
        }
    
    }
}
