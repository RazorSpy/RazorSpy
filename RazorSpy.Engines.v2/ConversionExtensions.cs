using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Razor = System.Web.Razor.Parser.SyntaxTree;
using Spy = RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.Engines.v2
{
    public static class ConversionExtensions
    {
        public static Spy.Block ToRazorSpy(this Razor.Block block)
        {
            return new Spy.Block()
            {
                Type = block.Type.ToString(),
                Children = block.Children.Select(n => n.ToRazorSpy())
            };
        }

        public static Spy.Span ToRazorSpy(this Razor.Span span)
        {
            return new Spy.Span()
            {
                Content = span.Content
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
    }
}
