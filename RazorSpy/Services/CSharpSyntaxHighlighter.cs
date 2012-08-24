using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;

namespace RazorSpy.Services
{
    [Export]
    public class CSharpSyntaxHighlighter : ISyntaxHighlighter
    {
        private static readonly Regex SingleLineCommentRegex = new Regex("^//.*$", RegexOptions.Multiline);
        public void Highlight(FlowDocument targetDocument, string text)
        {
            TextPointer start = targetDocument.ContentStart;
            foreach (Match m in SingleLineCommentRegex.Matches(text))
            {
                TextPointer matchStart = start.GetPositionAtOffset(m.Index);
                TextPointer matchEnd = matchStart.GetPositionAtOffset(m.Length);
                TextRange range = new TextRange(matchStart, matchEnd);
                range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.LimeGreen);
            }
        }
    }
}
