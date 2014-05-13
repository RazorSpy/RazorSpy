using System.CodeDom;

namespace RazorSpy.Contracts
{
    public interface ICodeDomCodeGenerator
    {
        string GenerateCode(ITemplateHost host, CodeCompileUnit ccu);
    }
}
