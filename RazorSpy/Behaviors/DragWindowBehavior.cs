using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace RazorSpy.Behaviors
{
    public class DragWindowBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        }

        void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window win = Window.GetWindow(AssociatedObject);
            win.DragMove();
        }
    }
}
