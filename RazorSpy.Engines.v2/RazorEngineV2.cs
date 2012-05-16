using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Web.Razor;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Engines.v2
{
    [Export(typeof(RazorEngine))]
    [ExportMetadata("Version", "2.0.0.0")]
    public class RazorEngineV2 : RazorEngine
    {
        internal static RazorLanguage CSharpLanguage = new RazorLanguage("csharp", "C#");
        internal static RazorLanguage VBLanguage = new RazorLanguage("vb", "VB");

        public override IEnumerable<RazorLanguage> Languages
        {
            get {
                yield return CSharpLanguage;
                yield return VBLanguage;
            }
        }

        public override TemplateHost CreateHost()
        {
            return new TemplateHostV2();
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
