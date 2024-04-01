
namespace DEPUI.Models.SchemaNode
{
    public class ObjectArraySchemaNode : ArraySchemaNode
    {
        public ObjectArraySchemaNode() { }
        public override string? TypeDisplayName => "Array (Object)";
        public ObjectArraySchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertiesValueObject, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name,parent, propertiesValueObject)
        {
            var items = propertiesValueObject["items"] as IDictionary<string,object?>;
            Children = childrenBuilder(items!, this);

        }

        public override SchemaNode[] TreeNodes => (Children ?? Array.Empty<SchemaNode>()).SelectMany(x => x.TreeNodes).Union(base.TreeNodes).ToArray();

        public override IDictionary<string, object?> GetAvroObject()
        {
            var name = NodePath == "$." ? "" : NodePath!.Substring(2).Replace(".", "");
            var objectType = new Dictionary<string, object?>
            {
                { "type", "record" },
                { "name", name },
                { "fields", Children?.Select(child => child.GetAvroObject()).ToArray() ?? Array.Empty<IDictionary<string, object?>>() }
            };

            return new Dictionary<string, object?>
            {
                 { "default", null },
                {"type",new object[] { "null",new Dictionary<string,object?> {
                    {"type", "array" },
                    { "items", objectType },
                    { "name", name + "Array"}
                } }},
                {"name",Name }
            };
        }
    }
}
