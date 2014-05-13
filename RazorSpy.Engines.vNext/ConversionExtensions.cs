using Microsoft.AspNet.Razor.Text;
using System.Linq;
using Razor = Microsoft.AspNet.Razor.Parser.SyntaxTree;
using Spy = RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Engines.vNext
{
    public static class ConversionExtensions
    {
        public static Spy.Block ToRazorSpy(this Razor.Block block)
        {
            return new Spy.Block()
            {
                Type = block.Type.ToString(),
                Children = block.Children.Select(n => n.ToRazorSpy()),
                Start = block.Start.ToRazorSpy(),
                Length = block.Length
            };
        }

        public static Spy.Span ToRazorSpy(this Razor.Span span)
        {
            return new Spy.Span()
            {
                Kind = span.Kind.ToString(),
                Content = span.Content,
                Start = span.Start.ToRazorSpy(),
                Length = span.Length
            };
        }

        public static Spy.SyntaxTreeNode ToRazorSpy(this Razor.SyntaxTreeNode node)
        {
            if (node.IsBlock)
            {
                return ((Razor.Block)node).ToRazorSpy();
            }
            return ((Razor.Span)node).ToRazorSpy();
        }

        public static Spy.SourceLocation ToRazorSpy(this SourceLocation self)
        {
            return new Spy.SourceLocation(self.AbsoluteIndex, self.CharacterIndex, self.LineIndex);
        }
    }
}
