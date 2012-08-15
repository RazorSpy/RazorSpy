using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor;
using System.Web.Razor.Generator;
using RazorSpy.Contracts;

namespace RazorSpy.Engines.v1
{
    public class TemplateHostV1 : ITemplateHost
    {
        public RazorLanguage Language { get; set; }
        public bool DesignTimeMode { get; set; }

        public RazorEngineHost CreateHost()
        {
            RazorCodeLanguage language = CreateLanguage();
            return new RazorEngineHost(language)
            {
                DesignTimeMode = DesignTimeMode,
                GeneratedClassContext = CreateGeneratedClassContext()
            };
        }

        private GeneratedClassContext CreateGeneratedClassContext()
        {
            return new GeneratedClassContext(
                executeMethodName: "Execute",
                writeMethodName: "Write",
                writeLiteralMethodName: "WriteLiteral",
                writeToMethodName: "WriteTo",
                writeLiteralToMethodName: "WriteLiteralTo",
                templateTypeName: "HelperResult",
                defineSectionMethodName: "DefineSection");
        }

        private RazorCodeLanguage CreateLanguage()
        {
            if (ReferenceEquals(Language, RazorEngineV1.CSharpLanguage))
            {
                return new CSharpRazorCodeLanguage();
            }
            else if (ReferenceEquals(Language, RazorEngineV1.VBLanguage))
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
        public static RazorEngineHost CreateHost(this ITemplateHost self)
        {
            TemplateHostV1 host = self as TemplateHostV1;
            return host == null ? null : host.CreateHost();
        }
    }
}
