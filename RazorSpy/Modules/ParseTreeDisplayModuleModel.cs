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
    [Export]
    public class ParseTreeDisplayModuleModel : ReactiveObject
    {
        // Service references
        private ICompilationService _compilationService;
        private IDocumentService _documentService;
        private IRazorConfigurationService _configService;

        // State
        private ICompilationManager _compilationManager;
        private ObservableAsPropertyHelper<IEnumerable<Block>> _tree;

        public IEnumerable<Block> Tree
        {
            get { return _tree.Value; }
        }

        internal ParseTreeDisplayModuleModel() { }

        [ImportingConstructor]
        public ParseTreeDisplayModuleModel(
            ICompilationService compilationService,
            IDocumentService documentService,
            IRazorConfigurationService configService)
        {
            _compilationService = compilationService;
            _documentService = documentService;
            _configService = configService;

            _compilationManager = _compilationService.CreateCompilationManager(_documentService.ActiveDocument);

            _tree = this.ObservableToProperty(
                _compilationManager.GenerationResults.Select(r => new [] { r.Document }),
                vm => vm.Tree);
        }
    }
}
