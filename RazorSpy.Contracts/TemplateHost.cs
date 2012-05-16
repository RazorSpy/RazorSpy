using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Contracts
{
    public abstract class TemplateHost
    {
        public RazorLanguage Language { get; set; }
        public bool DesignTimeMode { get; set; }
    }
}
