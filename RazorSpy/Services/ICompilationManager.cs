using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Services
{
    public interface ICompilationManager
    {
        IObservable<GenerationResult> GenerationResults { get; }
    }
}
