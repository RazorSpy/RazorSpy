using System;
using RazorSpy.Contracts;
using Microsoft.AspNet.Razor;
using Microsoft.AspNet.Razor.Generator;

namespace RazorSpy.Engines.vNext
{
    public class TemplateHostVNext : ITemplateHost
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
            if (ReferenceEquals(Language, RazorEngineVNext.CSharpLanguage))
            {
                return new CSharpRazorCodeLanguage();
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
            TemplateHostVNext host = self as TemplateHostVNext;
            return host == null ? null : host.CreateHost();
        }
    }
}
