using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;
using Microsoft.AspNet.Razor;

namespace RazorSpy.Engines.vNext
{
    [Export(typeof(IRazorEngine))]
    [ExportMetadata("Version", "vNext")]
    public class RazorEngineVNext : IRazorEngine
    {
        internal static readonly RazorLanguage CSharpLanguage = new RazorLanguage("csharp", "C#", "cshtml");

        public IEnumerable<RazorLanguage> Languages
        {
            get
            {
                return new[] { CSharpLanguage };
            }
        }

        public ITemplateHost CreateHost()
        {
            return new TemplateHostVNext();
        }

        public GenerationResult Generate(TextReader document, ITemplateHost host)
        {
            RazorTemplateEngine engine = CreateEngine(host);
            var result = engine.GenerateCode(document);
            return new GenerationResult()
            {
                Success = result.Success,
                Document = result.Document.ToRazorSpy(),
                Code = result.GeneratedCode,
            };
        }

        private static RazorTemplateEngine CreateEngine(ITemplateHost host)
        {
            RazorTemplateEngine engine = new RazorTemplateEngine(host.CreateHost());
            return engine;
        }
    }
}
