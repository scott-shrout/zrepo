using DEPUI.Extensions;
using DEPUI.Interfaces.Schema;

namespace DEPUI.Models.SchemaNode
{
    public class StringSchemaNode : SchemaNode, IHasSpecificationTypeName
    {
        public override string? TypeDisplayName => "String";
        public string? Format { get; set; }
        public string[]? EnumerationValues { get; set; }
        public string? SpecificationTypeName => "string";
        public StringSchemaNode() { }
        public StringSchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertiesValueObject, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder) : base(name, parent, propertiesValueObject) {
            Format = propertiesValueObject.GetValueOrDefault("format") as string;
        }
        public override bool HasDisplayProperties => base.HasDisplayProperties || Format != null || EnumerationValues != null;

        public override IDictionary<string, object?> GetAvroObject()
        {
            return new Dictionary<string, object?>
            {
                { "default", null },
                {"type",new string[] { "null","string" } },
                {"name",Name }
            };
        }
    }
}
