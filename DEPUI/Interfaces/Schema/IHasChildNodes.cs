using DEPUI.Models.SchemaNode;

namespace DEPUI.Interfaces.Schema
{
    public interface IHasChildNodes
    {
        IList<SchemaNode>? Children { get; set; } 
    }
}
