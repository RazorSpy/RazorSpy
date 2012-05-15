using System;
using System.CodeDom;
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
        public override ParserResult Parse(TextReader reader)
        {
            RazorTemplateEngine engine = CreateEngine();
            var result = engine.ParseTemplate(reader);
            return new ParserResult()
            {
                Success = result.Success,
                Document = result.Document.ToRazorSpy()
            };
        }

        public override GenerationResult Generate(TextReader document)
        {
            RazorTemplateEngine engine = CreateEngine();
            var result = engine.GenerateCode(document);
            return new GenerationResult()
            {
                Success = result.Success,
                Document = result.Document.ToRazorSpy(),
                Code = result.GeneratedCode
            };
        }

        public override CompilationResult Compile(TextReader code)
        {
            throw new NotImplementedException();
        }

        private static RazorTemplateEngine CreateEngine()
        {
            RazorEngineHost host = new RazorEngineHost(new CSharpRazorCodeLanguage());
            RazorTemplateEngine engine = new RazorTemplateEngine(host);
            return engine;
        }
    }
}
