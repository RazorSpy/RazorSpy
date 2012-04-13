using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace RazorSpy.SyntaxTree
{
    public class ParserResult
    {
        public bool Success { get; set; }
        public SyntaxTreeNode Document { get; set; }
    }

    public class GenerationResult : ParserResult
    {
        public CodeCompileUnit Code { get; set; }
    }

    public class CompilationResult : GenerationResult
    {
        public CompilerResults Compiler { get; set; }
    }
}
