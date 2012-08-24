using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RazorSpy.Modules
{
    /// <summary>
    /// Interaction logic for ConfigurationToolbarModule.xaml
    /// </summary>
    public partial class ConfigurationToolbarModule : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(ConfigurationToolbarModuleModel), typeof(ConfigurationToolbarModule));

        [Import]
        public ConfigurationToolbarModuleModel ViewModel
        {
            get { return (ConfigurationToolbarModuleModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public ConfigurationToolbarModule()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                ViewModel = new ConfigurationToolbarModuleModel();
            }
            else
            {
                this.RegisterWithContainer();
            }
            InitializeComponent();
        }
    }
}
