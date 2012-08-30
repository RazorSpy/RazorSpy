using RazorSpy.Contracts.SyntaxTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
