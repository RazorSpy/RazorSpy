using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Contracts.SyntaxTree
{
    public abstract class SyntaxTreeNode
    {
        public SourceLocation Start { get; set; }
        public int Length { get; set; }
    }
}
