using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Services
{
    public interface IRazorEngineReference : IRazorEngine, IRazorEngineMetadata
    {
    }

    public class RazorEngineReference : IRazorEngineReference
    {
        private Lazy<IRazorEngine, IRazorEngineMetadata> _inner;

        public IEnumerable<RazorLanguage> Languages
        {
            get { return _inner.Value.Languages; }
        }

        public string Version
        {
            get { return _inner.Metadata.Version; }
        }

        public RazorEngineReference(Lazy<IRazorEngine, IRazorEngineMetadata> inner)
        {
            _inner = inner;
        }

        public ITemplateHost CreateHost()
        {
            return _inner.Value.CreateHost();
        }

        public GenerationResult Generate(TextReader reader, ITemplateHost host)
        {
            return _inner.Value.Generate(reader, host);
        }
    }
}
