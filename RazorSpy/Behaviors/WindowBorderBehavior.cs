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
    public class WindowBorderBehavior : Behavior<UIElement>
    {
        private DateTime _lastClickTime = DateTime.MinValue;
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseMove += OnMouseMove;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
        }

        protected virtual void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
}
