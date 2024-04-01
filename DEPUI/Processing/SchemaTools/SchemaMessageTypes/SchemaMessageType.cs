using DEPUI.Processing.SchemaTools.SchemaNode;

namespace DEPUI.Processing.SchemaTools.SchemaMessageTypes
{
    public class SchemaMessageType
    {
        private Models.SchemaNode.SchemaNode baseNode;
        public Models.SchemaNode.SchemaNode BaseNode
        {
            get
            {
                return baseNode;
            }
            set
            {
                AllNodes = value.TreeNodes;
                baseNode = value;
            }

        }
        public Models.SchemaNode.SchemaNode[] AllNodes { get; set; }
        public SchemaMessageType(Models.SchemaNode.SchemaNode baseNode)
        {
            BaseNode = baseNode;
        }








    }
}
