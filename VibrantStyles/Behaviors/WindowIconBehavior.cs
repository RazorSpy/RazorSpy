using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace VibrantStyles.Behaviors
{
    public class WindowIconBehavior : Behavior<FrameworkElement>
    {
        private DateTime _lastClickTime = DateTime.MinValue;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        }

        protected virtual void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window win = Window.GetWindow(AssociatedObject);
            TimeSpan elapsed = DateTime.UtcNow - _lastClickTime;
            if (elapsed.TotalMilliseconds < 200.0)
            {
                win.Close();
                return;
            }
            else
            {
                _lastClickTime = DateTime.UtcNow;
            }

            SystemCommands.ShowSystemMenu(win, AssociatedObject.PointToScreen(new Point(0, AssociatedObject.ActualHeight)));
            e.Handled = true;
        }
    }
}