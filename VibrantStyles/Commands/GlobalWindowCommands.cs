using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VibrantStyles.Commands
{
    public static class GlobalWindowCommands
    {
        public static readonly ICommand CloseWindow = new RelayCommand(CloseWindowHandler, canExecute: true);
        public static readonly ICommand MinimizeWindow = new RelayCommand(SetWindowStateHandler(WindowState.Minimized), canExecute: true);
        public static readonly ICommand MaximizeWindow = new RelayCommand(SetWindowStateHandler(WindowState.Maximized), canExecute: true);
        public static readonly ICommand RestoreWindow = new RelayCommand(SetWindowStateHandler(WindowState.Normal), canExecute: true);

        private static Action<object> SetWindowStateHandler(WindowState newState)
        {
            return o =>
            {
                Window win = ExtractWindowFromParameter(o);
                win.WindowState = newState;
            };
        }

        private static void CloseWindowHandler(object parameter)
        {
            Window win = ExtractWindowFromParameter(parameter);
            win.Close();
        }

        private static Window ExtractWindowFromParameter(object parameter)
        {
            Window win = parameter as Window;
            if (win == null)
            {
                win = Application.Current.MainWindow;
            }
            return win;
        }
    }
}