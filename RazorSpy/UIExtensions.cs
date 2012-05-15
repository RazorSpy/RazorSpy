using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace RazorSpy
{
    internal static class UIExtensions
    {
        public static void RegisterWithContainer(this FrameworkElement self)
        {
            try
            {
                App.Container.ComposeParts(self);
            }
            catch (CompositionException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
