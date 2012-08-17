using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;
using RazorSpy.Services;
using ReactiveUI;

namespace RazorSpy.ViewModel
{
    [Export]
    public class MainViewModel : ReactiveObject
    {
        // Services
        private IDocumentService _documentService;
        private IRazorConfigurationService _configService;

        public IEnumerable<IRazorEngineReference> Engines
        {
            get { return _configService.AvailableEngines; }
        }

        public IRazorEngineReference SelectedEngine
        {
            get { return _configService.ActiveEngine; }
            set { _configService.ActiveEngine = value; }
        }

        public RazorLanguage SelectedLanguage
        {
            get { return _configService.ActiveLanguage; }
            set { _configService.ActiveLanguage = value; }
        }

        public IEnumerable<RazorLanguage> Languages
        {
            get { return _configService.AvailableLanguages; }
        }

        public bool MultiEngine { get; private set; }
        public bool SingleEngine { get; private set; }
        
        public MainViewModel() { }

        [ImportingConstructor]
        public MainViewModel(
            IRazorConfigurationService configService, 
            IDocumentService documentService)
        {
            _documentService = documentService;
            _configService = configService;

            MultiEngine = Engines.Count() > 1;
            SingleEngine = Engines.Count() == 1;
        }
    }
}
