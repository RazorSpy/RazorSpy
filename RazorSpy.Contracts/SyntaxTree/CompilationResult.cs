using System;
using System.CodeDom.Compiler;

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
        public string Code { get; set; }
    }

    [Serializable]
    public class CompilationResult : GenerationResult
    {
        public CompilerResults Compiler { get; set; }
    }
}
