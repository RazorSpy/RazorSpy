using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RazorSpy
{
    public static class CodeDomExtensions
    {
        public static string GenerateString<T>(this CodeCompileUnit self) where T : CodeDomProvider, new()
        {
            T provider = new T();
            StringBuilder b = new StringBuilder();
            using (TextWriter writer = new StringWriter(b))
            {
                provider.GenerateCodeFromCompileUnit(self, writer, new CodeGeneratorOptions());
            }
            return b.ToString();
        }
    }
}
