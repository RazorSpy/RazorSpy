using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Services
{
    public interface IRazorCompiler
    {
        GenerationResult Generate(TextReader reader);
    }
}
