
using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public class OneOfObjectSchemaNode : SchemaNode, IHasChildNodes
    {
        public OneOfObjectSchemaNode() { }
        public override string? TypeDisplayName => "One Of Object";
        public int SequenceNumber { get; set; }
        public OneOfObjectSchemaNode(SchemaNode parent, IDictionary<string, object?> propertiesValueObject, int sequenceNumber, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) 
        {
            Parent = parent;
            Name = Parent!.Name + SequenceNumber.ToString();
            SequenceNumber = sequenceNumber;
            Children = childrenBuilder(propertiesValueObject, this);
        }

        public IList<SchemaNode>? Children { get; set; }

        public override SchemaNode[] TreeNodes => (Children ?? Array.Empty<SchemaNode>()).SelectMany(x => x.TreeNodes).Union(base.TreeNodes).ToArray();

        public override IDictionary<string, object?> GetAvroObject()
        {
             return new Dictionary<string, object?>
            {
                { "type", "record" },
                { "name", Name },
                { "fields", Children?.Select(child => child.GetAvroObject()).ToArray() ?? Array.Empty<IDictionary<string, object?>>() }
            };

           
        }
    }
}
