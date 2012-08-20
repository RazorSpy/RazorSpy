using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RazorSpy
{
    public class MetroWindowChrome : ContentControl
    {
        public static readonly DependencyProperty WindowTitleProperty = DependencyProperty.Register(
            "WindowTitle", typeof(string), typeof(MetroWindowChrome));

        public string WindowTitle
        {
            get { return (string)GetValue(WindowTitleProperty); }
            set { SetValue(WindowTitleProperty, value); }
        }

        static MetroWindowChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroWindowChrome), new FrameworkPropertyMetadata(typeof(MetroWindowChrome)));
        }
    }
}
