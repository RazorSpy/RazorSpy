using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorSpy.SyntaxTree;
using System.IO;
using System.CodeDom;

namespace RazorSpy.Engines
{
    public abstract class RazorEngine
    {
        public abstract ParserResult Parse(TextReader reader);

        public virtual GenerationResult Generate(TextReader reader)
        {
            return Generate(Parse(reader).Document);
        }        
        public abstract GenerationResult Generate(SyntaxTreeNode document);

        public virtual CompilationResult Compile(TextReader reader)
        {
            return Compile(Generate(reader).Code);
        }

        public virtual CompilationResult Compile(SyntaxTreeNode document)
        {
            return Compile(Generate(document).Code);            
        }

        public abstract CompilationResult Compile(CodeCompileUnit code);
    }
}
