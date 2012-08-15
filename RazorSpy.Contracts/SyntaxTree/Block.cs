using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Contracts.SyntaxTree
{
    [Serializable]
    public class Block : SyntaxTreeNode
    {
        public string Type { get; set; }
        public IEnumerable<SyntaxTreeNode> Children { get; set; }
    }
}
