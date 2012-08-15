using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorSpy.Contracts.SyntaxTree;
using System.IO;
using System.CodeDom;

namespace RazorSpy.Contracts
{
    public interface IRazorEngineMetadata
    {
        string Version { get; }
    }

    public interface IRazorEngine
    {
        IEnumerable<RazorLanguage> Languages { get; }
        ITemplateHost CreateHost();
        GenerationResult Generate(TextReader reader, ITemplateHost host);
    }
}
