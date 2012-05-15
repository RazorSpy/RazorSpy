using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Contracts.SyntaxTree
{
    public class Span : SyntaxTreeNode
    {
        public string Content { get; set; }
    }
}
