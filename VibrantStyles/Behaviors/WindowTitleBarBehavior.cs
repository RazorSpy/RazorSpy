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
    public class WindowTitleBarBehavior : Behavior<FrameworkElement>
    {
        private DateTime _lastClickTime = DateTime.MinValue;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseRightButtonUp += OnMouseRightButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseRightButtonUp -= OnMouseRightButtonUp;
        }

        protected virtual void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Window win = Window.GetWindow(AssociatedObject);
            SystemCommands.ShowSystemMenu(win, win.PointToScreen(e.GetPosition(win)));
        }

        protected virtual void OnDoubleClick()
        {
            Window win = Window.GetWindow(AssociatedObject);
            if (win.WindowState == WindowState.Maximized)
            {
                win.WindowState = WindowState.Normal;
            }
            else if (win.WindowState == WindowState.Normal)
            {
                win.WindowState = WindowState.Maximized;
            }
        }

        protected virtual void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TimeSpan elapsed = DateTime.UtcNow - _lastClickTime;
            if (elapsed.TotalMilliseconds < 200.0)
            {
                OnDoubleClick();
            }
            else
            {
                _lastClickTime = DateTime.UtcNow;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window win = Window.GetWindow(AssociatedObject);
                win.DragMove();
            }
        }
    }
}