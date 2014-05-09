using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.ViewModel
{
    public static class ViewModelBuilder
    {
        public static SyntaxTreeNodeViewModel CreateViewModel(SyntaxTreeNode node)
        {
            Block b = node as Block;
            if (b != null)
            {
                return new BlockViewModel(b);
            }
            else
            {
                return new SpanViewModel((Span)node);
            }
        }
    }
}
