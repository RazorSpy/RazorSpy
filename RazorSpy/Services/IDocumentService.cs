using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Services
{
    public interface IDocumentService
    {
        IRazorDocument ActiveDocument { get; }
    }
}
