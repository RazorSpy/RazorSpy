using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorSpy.Contracts.SyntaxTree;
using System.IO;
using System.CodeDom;

namespace RazorSpy.Contracts
{
    public abstract class RazorEngine
    {
        public abstract ParserResult Parse(TextReader reader);
        public abstract GenerationResult Generate(TextReader reader);
        public abstract CompilationResult Compile(TextReader reader);
    }
}
