using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor;
using RazorSpy.Contracts;

namespace RazorSpy.Engines.v2
{
    public class TemplateHostV2 : TemplateHost
    {
        public RazorEngineHost CreateHost()
        {
            RazorCodeLanguage language = CreateLanguage();
            return new RazorEngineHost(language)
            {
                DesignTimeMode = DesignTimeMode
            };
        }

        private RazorCodeLanguage CreateLanguage()
        {
            if (ReferenceEquals(Language, RazorEngineV2.CSharpLanguage))
            {
                return new CSharpRazorCodeLanguage();
            }
            else if (ReferenceEquals(Language, RazorEngineV2.VBLanguage))
            {
                return new VBRazorCodeLanguage();
            }
            else
            {
                throw new NotSupportedException("Language not supported: " + Language.Name);
            }
        }
    }

    internal static class HostExtensions
    {
        public static RazorEngineHost CreateHost(this TemplateHost self)
        {
            TemplateHostV2 host = self as TemplateHostV2;
            return host == null ? null : host.CreateHost();
        }
    }
}
