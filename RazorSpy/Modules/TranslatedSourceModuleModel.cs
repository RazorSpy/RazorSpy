using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using RazorSpy.Contracts.SyntaxTree;
using RazorSpy.Services;
using ReactiveUI;

namespace RazorSpy.Modules
{
    public class TranslatedSourceModuleModel : ReactiveObject
    {
        // Service references
        private ICompilationService _compilationService;
        private IDocumentService _documentService;
        private IRazorConfigurationService _configService;

        // State
        private ICompilationManager _compilationManager;
        private ObservableAsPropertyHelper<string> _generatedCode;

        public string GeneratedCode
        {
            get { return _generatedCode.Value; }
        }

        [ImportingConstructor]
        public TranslatedSourceModuleModel(
            ICompilationService compilationService,
            IDocumentService documentService,
            IRazorConfigurationService configService)
        {
            _compilationService = compilationService;
            _documentService = documentService;
            _configService = configService;

            _compilationManager = _compilationService.CreateCompilationManager(_documentService.ActiveDocument);

            _generatedCode = this.ObservableToProperty(
                _compilationManager.GenerationResults.Select(GenerateCodeText),
                vm => vm.GeneratedCode);
        }

        private string GenerateCodeText(GenerationResult result)
        {
            return result.Code.GenerateString(_configService.CreateCodeDomProvider());
        }
    }
}
