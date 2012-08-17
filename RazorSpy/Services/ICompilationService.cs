using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Services
{
    public interface ICompilationService
    {
        ICompilationManager CreateCompilationManager(IRazorDocument document);
    }
}
