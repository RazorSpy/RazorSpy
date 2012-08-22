using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace RazorSpy.Behaviors
{
    public class WindowTitleBarBehavior : Behavior<UIElement>
    {
        private IDisposable _subscription;
        private DateTime _lastClickTime = DateTime.MinValue;
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseDown += OnMouseDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseDown -= OnMouseDown;
            _subscription.Dispose();
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

        protected virtual void OnMouseDown(object sender, MouseButtonEventArgs e)
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

            Window win = Window.GetWindow(AssociatedObject);
            win.DragMove();
        }
    }
}
