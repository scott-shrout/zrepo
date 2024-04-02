using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public abstract class ArraySchemaNode : SchemaNode
    {
        public ArraySchemaNode() { }
        public ArraySchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertiesValueObject) : base(name, parent, propertiesValueObject) { }
        public override string? TypeDisplayName => "Array";
        
    }
}
