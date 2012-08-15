using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Contracts.SyntaxTree
{
    [Serializable]
    public class Span : SyntaxTreeNode
    {
        public string Kind { get; set; }
        public string Content { get; set; }
        public SourceLocation GeneratedCodeStart { get; set; }
        public int GeneratedCodeLength { get; set; }

        public bool HasContent { get { return !String.IsNullOrEmpty(Content); } }

        public string FormattedContent
        {
            get
            {
                return Content.Replace(' ', '·').Replace(Environment.NewLine, "↲" + Environment.NewLine);
            }
        }
    }
}
