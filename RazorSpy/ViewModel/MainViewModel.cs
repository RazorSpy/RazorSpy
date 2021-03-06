﻿using System.ComponentModel.Composition;
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
