using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public abstract class ArraySchemaNode : SchemaNode, IHasChildNodes
    {
        public ArraySchemaNode() { }
        public ArraySchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertiesValueObject) : base(name, parent, propertiesValueObject) { }
        public override string? TypeDisplayName => "Array";
        public IList<SchemaNode>? Children { get; set; }
        public override SchemaNode[] TreeNodes => (Children ?? Array.Empty<SchemaNode>()).SelectMany(x => x.TreeNodes).Union(base.TreeNodes).ToArray();
    }
}
