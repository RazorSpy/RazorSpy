using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorSpy.SyntaxTree;
using System.IO;
using System.CodeDom;
using System.Web.Razor;

namespace RazorSpy.Engines.v2
{
    public class RazorEngineV2 : RazorEngine
    {
        public override ParserResult Parse(TextReader reader)
        {
            RazorEngineHost host = new RazorEngineHost(new CSharpRazorCodeLanguage());
            RazorTemplateEngine engine = new RazorTemplateEngine(host);
            var result = engine.ParseTemplate(reader);
            return new ParserResult()
            {
                Success = result.Success
            };
        }

        public override SyntaxTree.GenerationResult Generate(SyntaxTreeNode document)
        {
            throw new NotImplementedException();
        }

        public override SyntaxTree.CompilationResult Compile(CodeCompileUnit code)
        {
            throw new NotImplementedException();
        }
    }
}
