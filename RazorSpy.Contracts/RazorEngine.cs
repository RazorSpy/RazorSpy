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

    public abstract class RazorEngine
    {
        public abstract IEnumerable<RazorLanguage> Languages { get; }

        public abstract TemplateHost CreateHost();
        public abstract GenerationResult Generate(TextReader reader, TemplateHost host);
    }
}
