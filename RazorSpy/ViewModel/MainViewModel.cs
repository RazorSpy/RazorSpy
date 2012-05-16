using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reactive.Linq;
using System.Linq;
using Microsoft.CSharp;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;

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
        private ObservableAsPropertyHelper<string> _generatedCode;
        private ObservableAsPropertyHelper<IEnumerable<RazorLanguage>> _languages;
        private ObservableAsPropertyHelper<bool> _multiEngine;
        private ObservableAsPropertyHelper<bool> _singleEngine;

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

        public IEnumerable<RazorLanguage> Languages
        {
            get { return _languages.Value; }
        }

        public string GeneratedCode
        {
            get { return _generatedCode.Value; }
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

            _generatedCode = this.ObservableToProperty(
                Observable.Merge(
                    this.ObservableForProperty(vm => vm.DesignTimeMode).IgnoreValues(),
                    this.ObservableForProperty(vm => vm.SelectedEngine).IgnoreValues(),
                    this.ObservableForProperty(vm => vm.SelectedLanguage).IgnoreValues(),
                    this.ObservableForProperty(vm => vm.RazorCode).Buffer(TimeSpan.FromMilliseconds(100)).IgnoreValues())
                    .Select(_ => Regenerate()),
                vm => vm.GeneratedCode);
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

        private string Regenerate()
        {
            if (SelectedEngine != null && SelectedLanguage != null && !String.IsNullOrEmpty(RazorCode))
            {
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
                    return result.Code.GenerateString(SelectedLanguage.CreateCodeDomProvider());
                }
            }
            return String.Empty;
        }
    }
}
