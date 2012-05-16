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
    [Export(typeof(RazorEngine))]
    [ExportMetadata("Version", "1.0.0.0")]
    public class RazorEngineV1 : RazorEngine
    {
        internal static RazorLanguage CSharpLanguage = new RazorLanguage("csharp", "C#");
        internal static RazorLanguage VBLanguage = new RazorLanguage("vb", "VB");

        public override IEnumerable<RazorLanguage> Languages
        {
            get
            {
                yield return CSharpLanguage;
                yield return VBLanguage;
            }
        }

        public override TemplateHost CreateHost()
        {
            return new TemplateHostV1();
        }

        public override GenerationResult Generate(TextReader document, TemplateHost host)
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

        private static RazorTemplateEngine CreateEngine(TemplateHost host)
        {
            RazorTemplateEngine engine = new RazorTemplateEngine(host.CreateHost());
            return engine;
        }
    }
}
