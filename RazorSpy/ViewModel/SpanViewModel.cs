using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorSpy.Contracts.SyntaxTree;

namespace RazorSpy.ViewModel
{
    public class SpanViewModel : SyntaxTreeNodeViewModel
    {
        private string _kind;
        private string _content;
        
        private ObservableAsPropertyHelper<bool> _hasContent;
        private ObservableAsPropertyHelper<string> _formattedContent;

        public string Kind
        {
            get { return _kind; }
            set { _kind = this.RaiseAndSetIfChanged(vm => vm.Kind, value); }
        }

        public string Content
        {
            get { return _content; }
            set { _content = this.RaiseAndSetIfChanged(vm => vm.Content, value); }
        }

        public bool HasContent
        {
            get { return _hasContent.Value; }
        }

        public string FormattedContent
        {
            get { return _formattedContent.Value; }
        }

        public SpanViewModel()
            : base()
        {
            InitCommon();
        }

        public SpanViewModel(Span baseNode)
            : base(baseNode)
        {
            InitCommon();
            Kind = baseNode.Kind;
            Content = baseNode.Content;
        }

        private void InitCommon()
        {
            _hasContent = this.ObservableToProperty(
                this.ObservableForProperty(vm => vm.Content).Select(c => !String.IsNullOrEmpty(c.GetValue())),
                vm => vm.HasContent);

            _formattedContent = this.ObservableToProperty(
                this.ObservableForProperty(vm => vm.Content).Select(c => FormatContent(c.GetValue())),
                vm => vm.FormattedContent);
        }

        private string FormatContent(string content)
        {
            return content.Replace(' ', '·')
                          .Replace(Environment.NewLine, "↲" + Environment.NewLine);
        }
    }
}
