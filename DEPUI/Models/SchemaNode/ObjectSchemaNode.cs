using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public class ObjectSchemaNode : SchemaNode, IHasChildNodes, IHasSpecificationTypeName
    {
        public IList<SchemaNode>? Children { get; set; }
        public string? SpecificationTypeName => "object";
        public override string? TypeDisplayName => "Object";
       public ObjectSchemaNode() { }
        public ObjectSchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertiesValueObject, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name,parent, propertiesValueObject)
        {
            Children = childrenBuilder(propertiesValueObject, this);
        }
        public override SchemaNode[] TreeNodes => (Children ?? Array.Empty<SchemaNode>()).SelectMany(x => x.TreeNodes).Union(base.TreeNodes).ToArray();

       
        public override IDictionary<string, object?> GetAvroObject()
        {
           
            
            var objectType = new Dictionary<string, object?>
            {
                { "type", "record" },
                { "name", AvroNodePath },
                { "fields", Children?.Select(child => child.GetAvroObject()).ToArray() ?? Array.Empty<IDictionary<string, object?>>() }
            };

            if(Parent == null || Parent is OneOfSchemaNode)
            {
                return objectType;
            }

            return new Dictionary<string, object?>
            {
                 { "default", null },
                {"type",new object[] { "null",objectType } },
                {"name",Name }
            };
        }


        
    }
}
