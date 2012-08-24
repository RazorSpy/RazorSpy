using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using RazorSpy.Services;
using ReactiveUI;

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

        internal SourceEditorModuleModel() { }

        [ImportingConstructor]
        public SourceEditorModuleModel(IDocumentService documentService)
        {
            _documentService = documentService;
            _documentService.ActiveDocument.PropertyChanged.ForProperty(d => d.Text).Subscribe(_ => this.RaisePropertyChanged(s => s.DocumentText));
        }
    }
}