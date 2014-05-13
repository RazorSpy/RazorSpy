using System.CodeDom;
using System.CodeDom.Compiler;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using RazorSpy.Contracts;

namespace RazorSpy.Services
{
    [Export(typeof(ICodeDomCodeGenerator))]
    public class CodeDomCodeGenerator : ICodeDomCodeGenerator
    {
        public string GenerateCode(ITemplateHost host, CodeCompileUnit ccu)
        {
            return GenerateString(ccu, host.Language.CreateCodeDomProvider());
        }

        public static string GenerateString(CodeCompileUnit ccu, CodeDomProvider provider)
        {
            StringBuilder b = new StringBuilder();
            using (TextWriter writer = new StringWriter(b))
            {
                provider.GenerateCodeFromCompileUnit(ccu, writer, new CodeGeneratorOptions());
            }

            return b.ToString();
        }
    }
}
