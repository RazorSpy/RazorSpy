using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using RazorSpy.Services;
using ReactiveUI;
using System.Windows.Input;
using ReactiveUI.Xaml;
using Microsoft.Win32;
using System.IO;

namespace RazorSpy.Modules
{
    [Export]
    public class SourceEditorModuleModel : ReactiveObject
    {
        // Services
        private IDocumentService _documentService;

        // Bindable Properties
        public string DocumentText
        {
            get { return _documentService.ActiveDocument.Text; }
            set { _documentService.ActiveDocument.Text = value; }
        }

        public ReactiveCommand Open { get; set; }

        internal SourceEditorModuleModel() { }

        [ImportingConstructor]
        public SourceEditorModuleModel(IDocumentService documentService)
        {
            _documentService = documentService;
            _documentService.ActiveDocument.PropertyChanged.ForProperty(d => d.Text).Subscribe(_ => this.RaisePropertyChanged(s => s.DocumentText));

            Open = new ReactiveCommand();
            Open.Subscribe(_ =>
            {
                OpenFileDialog ofd = new OpenFileDialog()
                {
                    Filter = "Razor Documents (*.cshtml, *.vbhtml)|*.cshtml;*.vbhtml|All Files (*.*)|*.*"
                };
                if (ofd.ShowDialog() == true)
                {
                    _documentService.OpenDocument(ofd.FileName);
                }
            });
        }
    }
}