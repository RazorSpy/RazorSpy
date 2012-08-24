using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace RazorSpy.Services
{
    public interface ISyntaxHighlighter
    {
        void Highlight(FlowDocument targetDocument, string text);
    }
}
