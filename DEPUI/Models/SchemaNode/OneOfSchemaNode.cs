using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public class OneOfSchemaNode : SchemaNode, IHasChildNodes
    {
        public IList<SchemaNode>? Children { get; set; }
        public OneOfSchemaNode(string name, SchemaNode parent, IList<OneOfObjectGroupingItem> items, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name, parent)
        {
            Children = items.Select(item =>
            {
                item.Value!.SequenceNumber = item.SequenceNumber;
                item.Value.Parent = this;
                return item!.Value;
            }).ToArray();
        }
        public OneOfSchemaNode() { }
        public override string? TypeDisplayName => "OneOf";
        public override SchemaNode[] TreeNodes => (Children ?? Array.Empty<SchemaNode>()).SelectMany(x => x.TreeNodes).Union(base.TreeNodes).ToArray();

        public override IDictionary<string, object?> GetAvroObject()
        {
            return new Dictionary<string, object?>()
            {
                {"default", null },
                {"type", Children?.Select(child => child.GetAvroObject()).Cast<object>().Prepend("null").ToArray() ?? Array.Empty<object>() },
                {"name", Name }
            };
        }
    }
}
