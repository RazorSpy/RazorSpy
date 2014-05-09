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
    public class RazorConfigurationService : ObservePropertyChangesBase<RazorConfigurationService, IRazorConfigurationService>, IRazorConfigurationService
    {
        private IRazorCompiler _activeCompiler;
        private ICollection<RazorLanguage> _availableLanguages;
        private IRazorEngineReference _activeEngine;
        private RazorLanguage _activeLanguage;
        private bool _designTimeMode;

        public IRazorCompiler ActiveCompiler
        {
            get { return _activeCompiler; }
            private set { SetProperty(ref _activeCompiler, value); }
        }

        public ICollection<IRazorEngineReference> AvailableEngines { get; private set; }
        public ICollection<RazorLanguage> AvailableLanguages
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

        public bool DesignTimeMode
        {
            get { return _designTimeMode; }
            set { SetProperty(ref _designTimeMode, value); }
        }

        [ImportingConstructor]
        public RazorConfigurationService(
            [ImportMany] IEnumerable<Lazy<IRazorEngine, IRazorEngineMetadata>> engines)
        {
            AvailableEngines = engines.Select(l => new RazorEngineReference(l))
                                      .ToList<IRazorEngineReference>();
            PropertyChanged.ForProperty(s => s.ActiveEngine)
                .Subscribe(e => AvailableLanguages = e.Languages.ToList());
            PropertyChanged.ForProperty(s => s.AvailableLanguages)
                .Subscribe(SelectLanguage);
            PropertyChanged.ForAllPropertiesExcept(s => s.ActiveCompiler)
                .Subscribe(_ => RebuildCompiler());

            SelectEngine();
            RebuildCompiler();
        }

        public CodeDomProvider CreateCodeDomProvider()
        {
            return ActiveLanguage.CreateCodeDomProvider();
        }

        private void SelectLanguage(IEnumerable<RazorLanguage> languages)
        {
            if (languages != null && languages.Any())
            {
                if (ActiveLanguage == null || !languages.Select(l => l.Name)
                                                        .Any(name => name != ActiveLanguage.Name))
                {
                    ActiveLanguage = languages.FirstOrDefault();
                }
                else
                {
                    ActiveLanguage = languages.FirstOrDefault(language => language.Name == ActiveLanguage.Name);
                }
            }
        }

        private void SelectEngine()
        {
            if (AvailableEngines != null && AvailableEngines.Any() && (ActiveEngine == null || !AvailableEngines.Contains(ActiveEngine)))
            {
                ActiveEngine = AvailableEngines.OrderByDescending(e => e.Version).FirstOrDefault();
            }
        }

        private void RebuildCompiler()
        {
            ITemplateHost host = ActiveEngine.CreateHost();
            host.Language = ActiveLanguage;
            host.DesignTimeMode = DesignTimeMode;
            ActiveCompiler = new RazorCompiler(ActiveEngine, host);
        }
    }
}
