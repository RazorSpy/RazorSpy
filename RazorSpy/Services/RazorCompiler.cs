using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Services
{
    public class RazorCompiler : IRazorCompiler
    {
        private IRazorEngineReference _engine;
        private ITemplateHost _host;

        public RazorCompiler(IRazorEngineReference engine, ITemplateHost host)
        {
            _engine = engine;
            _host = host;
        }

        public GenerationResult Generate(TextReader reader)
        {
            return _engine.Generate(reader, _host);
        }
    }
}
