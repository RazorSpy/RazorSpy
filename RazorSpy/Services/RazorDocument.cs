using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using ReactiveUI;

namespace RazorSpy.Services
{
    public class RazorDocument : ObservePropertyChangesBase<RazorDocument, IRazorDocument>, IRazorDocument
    {
        private string _text;
        
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}
