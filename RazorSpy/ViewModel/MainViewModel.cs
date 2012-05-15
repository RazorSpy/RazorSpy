using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Xaml;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using RazorSpy.Contracts;
using RazorSpy.Contracts.SyntaxTree;
using System.IO;
using Microsoft.CSharp;

namespace RazorSpy.ViewModel
{
    [Export]
    public class MainViewModel : ReactiveObject
    {
        private string _razorCode;

        public string RazorCode
        {
            get { return _razorCode; }
            set { _razorCode = this.RaiseAndSetIfChanged(m => m.RazorCode, value); }
        }
        
        private ObservableAsPropertyHelper<string> _generatedCode;

        public string GeneratedCode
        {
            get { return _generatedCode.Value; }
        }

        [Import]
        public RazorEngine Engine { get; set; }

        public MainViewModel()
        {
            _generatedCode = this.ObservableToProperty(
                this.ObservableForProperty(vm => vm.RazorCode)
                    .ObserveOn(RxApp.DeferredScheduler)
                    .Select(chg => GenerateCode(chg.GetValue())),
                vm => vm.GeneratedCode);
        }

        private string GenerateCode(string razor)
        {
            GenerationResult result;
            using (TextReader reader = new StringReader(razor))
            {
                result = Engine.Generate(reader);
            }
            if (result != null)
            {
                return result.Code.GenerateString<CSharpCodeProvider>();
            }
            return null;
        }
    }
}
