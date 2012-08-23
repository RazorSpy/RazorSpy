using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorSpy.Contracts;
using ReactiveUI;

namespace RazorSpy.Services
{
    public interface IRazorConfigurationService : IObservePropertyChanges<IRazorConfigurationService>
    {
        IRazorCompiler ActiveCompiler { get; }
        ICollection<IRazorEngineReference> AvailableEngines { get; }
        ICollection<RazorLanguage> AvailableLanguages { get; }

        IRazorEngineReference ActiveEngine { get; set; }
        RazorLanguage ActiveLanguage { get; set; }

        CodeDomProvider CreateCodeDomProvider();
    }
}
