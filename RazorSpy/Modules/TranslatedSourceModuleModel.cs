using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using RazorSpy.Services;
using ReactiveUI;

namespace RazorSpy.Modules
{
    [Export]
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

        internal TranslatedSourceModuleModel() { }

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
                _compilationManager.GenerationResults.Select(r => r.Code),
                vm => vm.GeneratedCode);
        }
    }
}
