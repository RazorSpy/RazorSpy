﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Contracts
{
    [Serializable]
    public class RazorLanguage
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string FileExtension { get; private set; }
        
        public RazorLanguage(string id, string name, string fileExtension)
        {
            Id = id;
            Name = name;
            FileExtension = fileExtension;
        }

        public CodeDomProvider CreateCodeDomProvider()
        {
            if (!CodeDomProvider.IsDefinedLanguage(Id))
            {
                throw new NotSupportedException("Language not supported: " + Id);
            }
            return CodeDomProvider.GetCompilerInfo(Id).CreateProvider();
        }
    }
}
