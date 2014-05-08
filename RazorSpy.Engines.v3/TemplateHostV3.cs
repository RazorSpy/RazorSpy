using System;
using System.Web.Razor;
using System.Web.Razor.Generator;
using RazorSpy.Contracts;

namespace RazorSpy.Engines.v3
{
    public class TemplateHostV3 : ITemplateHost
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
                defineSectionMethodName: "DefineSection",
                beginContextMethodName: "BeginContext",
                endContextMethodName: "EndContext");
        }

        private RazorCodeLanguage CreateLanguage()
        {
            if (ReferenceEquals(Language, RazorEngineV3.CSharpLanguage))
            {
                return new CSharpRazorCodeLanguage();
            }
            else if (ReferenceEquals(Language, RazorEngineV3.VBLanguage))
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
            TemplateHostV3 host = self as TemplateHostV3;
            return host == null ? null : host.CreateHost();
        }
    }
}
