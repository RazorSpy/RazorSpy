using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace RazorSpy.Modules
{
    /// <summary>
    /// Interaction logic for ParseTreeDisplayModule.xaml
    /// </summary>
    public partial class ParseTreeDisplayModule : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(ParseTreeDisplayModuleModel), typeof(ParseTreeDisplayModule));

        [Import]
        public ParseTreeDisplayModuleModel ViewModel
        {
            get { return (ParseTreeDisplayModuleModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public ParseTreeDisplayModule()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                ViewModel = new ParseTreeDisplayModuleModel();
            }
            else
            {
                this.RegisterWithContainer();
            }
            InitializeComponent();
        }
    }
}
