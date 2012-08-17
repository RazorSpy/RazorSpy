using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using RazorSpy.Contracts;

namespace RazorSpy.Services
{
    [Export(typeof(IRazorConfigurationService))]
    public class RazorConfigurationService : ObservePropertyChangesBase<RazorConfigurationService>, IRazorConfigurationService
    {
        private IRazorCompiler _activeCompiler;
        private IEnumerable<RazorLanguage> _availableLanguages;
        private IRazorEngineReference _activeEngine;
        private RazorLanguage _activeLanguage;

        public IRazorCompiler ActiveCompiler
        {
            get { return _activeCompiler ?? (_activeCompiler = CreateCompiler()); }
        }

        public IEnumerable<IRazorEngineReference> AvailableEngines { get; private set; }
        public IEnumerable<RazorLanguage> AvailableLanguages
        {
            get { return _availableLanguages; }
            private set { SetProperty(ref _availableLanguages, value); }
        }

        public IRazorEngineReference ActiveEngine
        {
            get { return _activeEngine; }
            set { SetProperty(ref _activeEngine, value); }
        }

        public Contracts.RazorLanguage ActiveLanguage
        {
            get { return _activeLanguage; }
            set { SetProperty(ref _activeLanguage, value); }
        }

        [ImportingConstructor]
        public RazorConfigurationService(
            [ImportMany] IEnumerable<Lazy<IRazorEngine, IRazorEngineMetadata>> engines)
        {
            AvailableEngines = engines.Select(l => new RazorEngineReference(l));
            SelectEngine();

            PropertyChanged.ForProperty(s => s.ActiveEngine)
                .Subscribe(e => AvailableLanguages = e.Languages);
            PropertyChanged.ForProperty(s => s.AvailableLanguages)
                .Subscribe(SelectLanguage);
            PropertyChanged.ForAllPropertiesExcept(s => s.ActiveCompiler)
                .Subscribe(_ => _activeCompiler = null);
        }

        public CodeDomProvider CreateCodeDomProvider()
        {
            return ActiveLanguage.CreateCodeDomProvider();
        }

        private void SelectLanguage(IEnumerable<RazorLanguage> languages)
        {
            if (languages != null && languages.Any() && (ActiveLanguage == null || !languages.Contains(ActiveLanguage)))
            {
                ActiveLanguage = languages.FirstOrDefault();
            }
        }

        private void SelectEngine()
        {
            if (AvailableEngines != null && AvailableEngines.Any() && (ActiveEngine == null || !AvailableEngines.Contains(ActiveEngine)))
            {
                ActiveEngine = AvailableEngines.FirstOrDefault();
            }
        }

        private IRazorCompiler CreateCompiler()
        {
            ITemplateHost host = ActiveEngine.CreateHost();
            // Todo: Configure the host
            return new RazorCompiler(ActiveEngine, host);
        }
    }
}
