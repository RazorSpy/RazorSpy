using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;
using ReactiveUI;

namespace RazorSpy.ViewModel
{
    [Export]
    public class MainViewModel : ReactiveObject
    {
        private ReactiveCollection<Lazy<RazorEngine, IRazorEngineMetadata>> _engines = new ReactiveCollection<Lazy<RazorEngine, IRazorEngineMetadata>>();
        private Lazy<RazorEngine, IRazorEngineMetadata> _selectedEngine;
        private string _razorCode;
        private RazorLanguage _selectedLanguage;
        private bool _designTimeMode;
        private IEnumerable<Block> _generatedTree;
        private string _generatedCode;
        private string _status;
        private StatusType _statusType;

        private ObservableAsPropertyHelper<IEnumerable<RazorLanguage>> _languages;
        private ObservableAsPropertyHelper<bool> _multiEngine;
        private ObservableAsPropertyHelper<bool> _singleEngine;

        public string Status
        {
            get { return _status; }
            set { _status = this.RaiseAndSetIfChanged(m => m.Status, value); }
        }

        public string RazorCode
        {
            get { return _razorCode; }
            set { _razorCode = this.RaiseAndSetIfChanged(m => m.RazorCode, value); }
        }

        [ImportMany]
        public ReactiveCollection<Lazy<RazorEngine, IRazorEngineMetadata>> Engines
        {
            get { return _engines; }
            set { _engines = this.RaiseAndSetIfChanged(m => m.Engines, value); }
        }

        public Lazy<RazorEngine, IRazorEngineMetadata> SelectedEngine
        {
            get { return _selectedEngine; }
            set { _selectedEngine = this.RaiseAndSetIfChanged(m => m.SelectedEngine, value); }
        }

        public RazorLanguage SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { _selectedLanguage = this.RaiseAndSetIfChanged(m => m.SelectedLanguage, value); }
        }

        public bool DesignTimeMode
        {
            get { return _designTimeMode; }
            set { _designTimeMode = this.RaiseAndSetIfChanged(m => m.DesignTimeMode, value); }
        }

        public IEnumerable<Block> GeneratedTree
        {
            get { return _generatedTree; }
            private set { _generatedTree = this.RaiseAndSetIfChanged(m => m.GeneratedTree, value); }
        }

        public string GeneratedCode
        {
            get { return _generatedCode; }
            private set { _generatedCode = this.RaiseAndSetIfChanged(m => m.GeneratedCode, value); }
        }

        public IEnumerable<RazorLanguage> Languages
        {
            get { return _languages.Value; }
        }

        public bool MultiEngine
        {
            get { return _multiEngine.Value; }
        }

        public bool SingleEngine
        {
            get { return _singleEngine.Value; }
        }

        public MainViewModel()
        {
            _multiEngine = this.ObservableToProperty(
                Engines.Changed.Select(_ => Engines.Count > 1),
                vm => vm.MultiEngine);
            _singleEngine = this.ObservableToProperty(
                Engines.Changed.Select(_ => Engines.Count == 1),
                vm => vm.SingleEngine);
            _languages = this.ObservableToProperty(
                this.ObservableForProperty(vm => vm.SelectedEngine)
                    .Select(_ => SelectedEngine.Value.Languages),
                vm => vm.Languages);
            _languages.Subscribe(_ => EnsureLanguage());
            Engines.Changed.Subscribe(_ => EnsureEngine());

            Observable.Merge(
                this.ObservableForProperty(vm => vm.DesignTimeMode).IgnoreValues(),
                this.ObservableForProperty(vm => vm.SelectedEngine).IgnoreValues(),
                this.ObservableForProperty(vm => vm.SelectedLanguage).IgnoreValues(),
                this.ObservableForProperty(vm => vm.RazorCode).IgnoreValues())
                .ObserveOn(RxApp.DeferredScheduler)
                .Subscribe(_ => Regenerate());

            this.PropertyChanged += MainViewModel_PropertyChanged;
        }

        void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void EnsureLanguage()
        {
            if (Languages != null && Languages.Any() && (SelectedLanguage == null || !Languages.Contains(SelectedLanguage)))
            {
                SelectedLanguage = Languages.FirstOrDefault();
            }
        }

        private void EnsureEngine()
        {
            if (Engines != null && Engines.Any() && (SelectedEngine == null || !Engines.Contains(SelectedEngine)))
            {
                SelectedEngine = Engines.FirstOrDefault();
            }
        }

        private void Regenerate()
        {
            if (SelectedEngine != null && SelectedLanguage != null && !String.IsNullOrEmpty(RazorCode))
            {
                Status = "Compiling...";
                // Configure the host
                RazorEngine engine = SelectedEngine.Value;
                TemplateHost host = engine.CreateHost();
                host.Language = SelectedLanguage;
                host.DesignTimeMode = DesignTimeMode;

                // Generate the template
                GenerationResult result;
                using (TextReader reader = new StringReader(RazorCode))
                {
                    result = SelectedEngine.Value.Generate(reader, host);
                }
                if (result != null)
                {
                    GeneratedCode = result.Code.GenerateString(SelectedLanguage.CreateCodeDomProvider());
                    GeneratedTree = new[] { result.Document };
                    Status = result.Success ? "Success" : "Errors during compilation";
                    return;
                }
            }
            GeneratedCode = String.Empty;
            GeneratedTree = null;
        }
    }
}
