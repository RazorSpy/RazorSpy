using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;

namespace RazorSpy.Services
{
    // Service to manage a single-document environment
    [Export(typeof(IDocumentService))]
    public class SingleDocumentService : IDocumentService
    {
        private IRazorConfigurationService _configService;

        private RazorDocument _activeDocument = new RazorDocument();

        public IRazorDocument ActiveDocument
        {
            get { return _activeDocument; }
        }

        [ImportingConstructor]
        public SingleDocumentService(IRazorConfigurationService configService)
        {
            _configService = configService;
        }

        public void OpenDocument(string fileName)
        {
            // Get the extension and use it to select a language
            string extension = Path.GetExtension(fileName).TrimStart('.');

            var language = _configService.AvailableLanguages
                                         .Where(l => String.Equals(l.FileExtension, extension, StringComparison.OrdinalIgnoreCase))
                                         .FirstOrDefault();
            if (language != null)
            {
                _configService.ActiveLanguage = language;
            }

            // Dump the text in to the document
            _activeDocument.Text = File.ReadAllText(fileName);
        }
    }
}
