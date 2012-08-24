using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace RazorSpy.Modules
{
    /// <summary>
    /// Interaction logic for TranslatedSourceModule.xaml
    /// </summary>
    public partial class TranslatedSourceModule : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(TranslatedSourceModuleModel), typeof(TranslatedSourceModule));

        [Import]
        public TranslatedSourceModuleModel ViewModel
        {
            get { return (TranslatedSourceModuleModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public TranslatedSourceModule()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                ViewModel = new TranslatedSourceModuleModel();
            }
            else
            {
                this.RegisterWithContainer();
            }
            InitializeComponent();
        }
    }
}
