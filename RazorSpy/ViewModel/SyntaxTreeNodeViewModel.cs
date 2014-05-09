using RazorSpy.Contracts.SyntaxTree;
using ReactiveUI;

namespace RazorSpy.ViewModel
{
    public abstract class SyntaxTreeNodeViewModel : ReactiveObject
    {
        private int _offset;
        private int _line;
        private int _column;
        private int _length;
        private bool _isSelected;
        private bool _isExpanded;

        public int Offset
        {
            get { return _offset; }
            set { _offset = this.RaiseAndSetIfChanged(vm => vm.Offset, value); }
        }

        public int Line
        {
            get { return _line; }
            set { _line = this.RaiseAndSetIfChanged(vm => vm.Line, value); }
        }

        public int Column
        {
            get { return _column; }
            set { _column = this.RaiseAndSetIfChanged(vm => vm.Column, value); }
        }

        public int Length
        {
            get { return _length; }
            set { _length = this.RaiseAndSetIfChanged(vm => vm.Length, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = this.RaiseAndSetIfChanged(vm => vm.IsSelected, value); }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = this.RaiseAndSetIfChanged(vm => vm.IsExpanded, value); }
        }

        protected SyntaxTreeNodeViewModel() { }
        protected SyntaxTreeNodeViewModel(SyntaxTreeNode baseNode)
        {
            Offset = baseNode.Start.Offset;
            Line = baseNode.Start.Line;
            Column = baseNode.Start.Column;
            Length = baseNode.Length;
        }
    }
}
