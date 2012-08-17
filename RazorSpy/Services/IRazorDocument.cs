using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Services
{
    public interface IRazorDocument : IObservePropertyChanges<IRazorDocument>
    {
        string Text { get; set; }
    }
}
