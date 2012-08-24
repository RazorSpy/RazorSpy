using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace RazorSpy.ViewModel
{
    [Export]
    public class MainViewModel : ReactiveObject
    {
        public string Version { get; private set; }

        public MainViewModel()
        {
            Version = typeof(MainViewModel).Assembly.GetName().Version.ToString();
        }
    }
}
