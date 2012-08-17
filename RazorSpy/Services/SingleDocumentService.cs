using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace RazorSpy.Services
{
    // Service to manage a single-document environment
    [Export(typeof(IDocumentService))]
    public class SingleDocumentService : IDocumentService
    {
        private RazorDocument _activeDocument = new RazorDocument();

        public IRazorDocument ActiveDocument
        {
            get { return _activeDocument; }
        }
    }
}
