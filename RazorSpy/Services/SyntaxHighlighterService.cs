using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace RazorSpy.Services
{
    public class SyntaxHighlighterService : ISyntaxHighlighterService
    {
        private Dictionary<string, ISyntaxHighlighter> _highlighers = new Dictionary<string, ISyntaxHighlighter>(StringComparer.OrdinalIgnoreCase)
        {
            { "c#", new CSharpSyntaxHighlighter() }
        };

        public FlowDocument FormatCode(string languageId, string text)
        {
            FlowDocument doc = new FlowDocument(new Paragraph(new Run(text)));
            ISyntaxHighlighter highlighter = null;
            if (_highlighers.TryGetValue(languageId, out highlighter))
            {
                highlighter.Highlight(doc, text);
            }
            return doc;
        }
    }
}
