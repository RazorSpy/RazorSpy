using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace RazorSpy.Modules
{
    /// <summary>
    /// Interaction logic for SourceEditorModule.xaml
    /// </summary>
    public partial class SourceEditorModule : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(SourceEditorModuleModel), typeof(SourceEditorModule));

        [Import]
        public SourceEditorModuleModel ViewModel
        {
            get { return (SourceEditorModuleModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public SourceEditorModule()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                ViewModel = new SourceEditorModuleModel();
            }
            else
            {
                this.RegisterWithContainer();
            }
            InitializeComponent();
        }
    }
}
