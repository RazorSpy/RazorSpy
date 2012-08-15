using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Contracts
{
    public interface ITemplateHost
    {
        RazorLanguage Language { get; set; }
        bool DesignTimeMode { get; set; }
    }
}
