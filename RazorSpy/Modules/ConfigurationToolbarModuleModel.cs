using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using RazorSpy.Contracts.SyntaxTree;
using RazorSpy.Services;
using ReactiveUI;
using RazorSpy.Contracts;

namespace RazorSpy.Modules
{
    [Export]
    public class ConfigurationToolbarModuleModel : ReactiveObject
    {
        // Service references
        private IRazorConfigurationService _configService;

        public ICollection<IRazorEngineReference> Engines
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

        public ICollection<RazorLanguage> Languages
        {
            get { return _configService.AvailableLanguages; }
        }

        public bool DesignTimeMode
        {
            get { return _configService.DesignTimeMode; }
            set { _configService.DesignTimeMode = value; }
        }

        public bool MultiEngine { get; private set; }
        public bool SingleEngine { get; private set; }

        internal ConfigurationToolbarModuleModel() { }

        [ImportingConstructor]
        public ConfigurationToolbarModuleModel(
            IRazorConfigurationService configService)
        {
            _configService = configService;

            MultiEngine = Engines.Count() > 1;
            SingleEngine = Engines.Count() == 1;

            _configService.PropertyChanged
                          .ForProperty(c => c.AvailableLanguages)
                          .Subscribe(_ => raisePropertyChanged("Languages"));
            _configService.PropertyChanged
                          .ForProperty(c => c.AvailableEngines)
                          .Subscribe(_ => raisePropertyChanged("Engines"));
            _configService.PropertyChanged
                          .ForProperty(c => c.ActiveLanguage)
                          .Subscribe(_ => raisePropertyChanged("SelectedLanguage"));
            _configService.PropertyChanged
                          .ForProperty(c => c.ActiveEngine)
                          .Subscribe(_ => raisePropertyChanged("SelectedEngine"));
            _configService.PropertyChanged
                          .ForProperty(c => c.DesignTimeMode)
                          .Subscribe(_ => raisePropertyChanged("DesignTimeMode"));
        }
    }
}
