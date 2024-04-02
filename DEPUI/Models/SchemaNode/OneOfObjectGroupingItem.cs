namespace DEPUI.Models.SchemaNode
{
    public record OneOfObjectGroupingItem
    {
        public int SequenceNumber { get; set; }
        public SchemaNode? Value { get; set; }
    }
}
