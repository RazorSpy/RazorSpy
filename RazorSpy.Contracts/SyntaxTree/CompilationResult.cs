using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace RazorSpy.Contracts.SyntaxTree
{
    [Serializable]
    public class ParserResult
    {
        public bool Success { get; set; }
        public Block Document { get; set; }
    }

    [Serializable]
    public class GenerationResult : ParserResult
    {
        public CodeCompileUnit Code { get; set; }
    }

    [Serializable]
    public class CompilationResult : GenerationResult
    {
        public CompilerResults Compiler { get; set; }
    }
}
