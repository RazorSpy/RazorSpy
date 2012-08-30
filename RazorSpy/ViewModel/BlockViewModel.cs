using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.ViewModel
{
    public class BlockViewModel : SyntaxTreeNodeViewModel
    {
        private string _type;
        private ObservableCollection<SyntaxTreeNodeViewModel> _children;

        public string Type
        {
            get { return _type; }
            set { _type = this.RaiseAndSetIfChanged(vm => vm.Type, value); }
        }

        public ObservableCollection<SyntaxTreeNodeViewModel> Children
        {
            get { return _children; }
        }

        public BlockViewModel()
            : base()
        {
            _children = new ObservableCollection<SyntaxTreeNodeViewModel>();
        }

        public BlockViewModel(Block baseNode)
            : base(baseNode)
        {
            Type = baseNode.Type;
            _children = new ObservableCollection<SyntaxTreeNodeViewModel>(
                baseNode.Children.Select(ViewModelBuilder.CreateViewModel));
        }
    }
}
