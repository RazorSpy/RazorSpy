using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Engines.v1
{
    [Export(typeof(IRazorEngine))]
    [ExportMetadata("Version", "1.0.0.0")]
    public class RazorEngineV1 : IRazorEngine
    {
        internal static RazorLanguage CSharpLanguage = new RazorLanguage("csharp", "C#", "cshtml");
        internal static RazorLanguage VBLanguage = new RazorLanguage("vb", "VB", "vbhtml");

        [Import]
        private ICodeDomCodeGenerator CodeGenerator { get; set; }

        public IEnumerable<RazorLanguage> Languages
        {
            get
            {
                yield return CSharpLanguage;
                yield return VBLanguage;
            }
        }

        public ITemplateHost CreateHost()
        {
            return new TemplateHostV1();
        }

        public GenerationResult Generate(TextReader document, ITemplateHost host)
        {
            RazorTemplateEngine engine = CreateEngine(host);
            var result = engine.GenerateCode(document);
            return new GenerationResult()
            {
                Success = result.Success,
                Document = result.Document.ToRazorSpy(),
                Code = CodeGenerator.GenerateCode(host, result.GeneratedCode),
            };
        }

        private static RazorTemplateEngine CreateEngine(ITemplateHost host)
        {
            RazorTemplateEngine engine = new RazorTemplateEngine(host.CreateHost());
            return engine;
        }
    }
}
