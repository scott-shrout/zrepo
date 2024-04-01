namespace DEPUI.Models.SchemaNode
{
    public class SchemaNodeBase
    {
        public bool Open { get; set; }
        public SchemaNode? Base { get; set; }
        public IList<SchemaNode>? Children { get; set; }
       
    }
}
