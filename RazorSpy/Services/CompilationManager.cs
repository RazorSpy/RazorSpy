using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Services
{
    public class CompilationManager : ICompilationManager
    {
        private IRazorDocument _document;
        private IRazorConfigurationService _config;
        private IObservable<GenerationResult> _generationResults;

        public IObservable<GenerationResult> GenerationResults
        {
            get { return _generationResults; }
        }

        public CompilationManager(IRazorDocument document, IRazorConfigurationService config)
        {
            _document = document;
            _config = config;

            _generationResults = 
                Observable.Merge(
                    _document.PropertyChanged.ForProperty(d => d.Text),
                    _config.PropertyChanged.ForProperty(c => c.ActiveCompiler).Select(_ => _document.Text))
                .Select(Compile);
        }

        private GenerationResult Compile(string razorCode)
        {
            // Get the active compiler
            var compiler = _config.ActiveCompiler;
            
            // Generate the template
            using (TextReader reader = new StringReader(razorCode))
            {
                return compiler.Generate(reader);
            }
        }
    }
}
