using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace RazorSpy.Services
{
    public interface ISyntaxHighlighterService
    {
        FlowDocument FormatCode(string languageId, string text);
    }
}
