using System.Collections.Generic;
using System.IO;
using RazorSpy.Contracts.SyntaxTree;

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
