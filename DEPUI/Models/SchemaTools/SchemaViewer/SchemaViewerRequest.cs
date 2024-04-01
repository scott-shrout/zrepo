using DEPUI.Processing.SchemaTools.SchemaMessageTypes;
using DEPUI.Processing.SchemaTools.SchemaTypes;
using Microsoft.AspNetCore.Components.Forms;

namespace DEPUI.Models.SchemaTools.SchemaViewer
{
    public class SchemaViewerRequest
    {
        public string? LayoutType { get; set; }
        public string? MessageTypeName { get; set; }
        public ApiSchema? ApiSchema { get; set; }
        public SchemaMessageType? MessageType { get; set; }
        private string? searchPath = "$";
        public string? SearchPath
        {
            get
            {
                return searchPath;
            }
            set
            {
                if (value == null ||  value == string.Empty)
                {
                    searchPath = "$";
                }
                else if (value.Length > 2 && value.Substring(0, 2) != "$.")
                {
                    searchPath = $"$.{value}";
                }
                else
                {
                    searchPath = value;
                }
            }
        }
        public string? SearchName { get; set; }
        public string? SearchType { get; set; }
    }
}
