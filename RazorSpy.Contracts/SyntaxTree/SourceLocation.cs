using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorSpy.Contracts.SyntaxTree
{
    public struct SourceLocation
    {
        public int Offset { get; set; }
        public int Column { get; set; }
        public int Line { get; set; }

        public SourceLocation(int offset, int column, int line) : this()
        {
            Offset = offset;
            Column = column;
            Line = line;
        }
    }
}
