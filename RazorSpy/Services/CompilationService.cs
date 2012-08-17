using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace RazorSpy.Services
{
    [Export(typeof(ICompilationService))]
    public class CompilationService : ICompilationService
    {
        private IRazorConfigurationService _config;

        [ImportingConstructor]
        public CompilationService(IRazorConfigurationService config)
        {
            _config = config;
        }

        public ICompilationManager CreateCompilationManager(IRazorDocument document)
        {
            return new CompilationManager(document, _config);
        }
    }
}
