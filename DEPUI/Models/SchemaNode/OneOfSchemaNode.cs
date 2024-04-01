using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public class OneOfSchemaNode : SchemaNode, IHasChildNodes
    {
        public IList<SchemaNode>? Children { get; set; }
        public OneOfSchemaNode(string name, object[] oneOfItems, SchemaNode parent, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name,parent)
        {
            Parent = parent;
            Children = oneOfItems.Cast<IDictionary<string,object?>>().Select((child,index) => new OneOfObjectSchemaNode(this,child,index,childrenBuilder)).ToArray();
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
