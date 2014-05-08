using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Web.Razor;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Engines.v3
{
    [Export(typeof(IRazorEngine))]
    [ExportMetadata("Version", "3.0.0.0")]
    public class RazorEngineV3 : IRazorEngine
    {
        internal static readonly RazorLanguage CSharpLanguage = new RazorLanguage("csharp", "C#", "cshtml");
        internal readonly static RazorLanguage VBLanguage = new RazorLanguage("vb", "VB", "vbhtml");

        public IEnumerable<RazorLanguage> Languages
        {
            get {
                yield return CSharpLanguage;
                yield return VBLanguage;
            }
        }

        public ITemplateHost CreateHost()
        {
            return new TemplateHostV3();
        }

        public GenerationResult Generate(TextReader document, ITemplateHost host)
        {
            RazorTemplateEngine engine = CreateEngine(host);
            var result = engine.GenerateCode(document);
            return new GenerationResult()
            {
                Success = result.Success,
                Document = result.Document.ToRazorSpy(),
                Code = result.GeneratedCode
            };
        }

        private static RazorTemplateEngine CreateEngine(ITemplateHost host)
        {
            RazorTemplateEngine engine = new RazorTemplateEngine(host.CreateHost());
            return engine;
        }
    }
}
