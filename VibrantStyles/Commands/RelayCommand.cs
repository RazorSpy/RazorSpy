using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VibrantStyles.Commands
{
    // Simple relay command for when ReactiveCommand is overkill
    public class RelayCommand : ICommand
    {
        private Action<object> _act;
        private bool _canExecute;

        public bool CommandCanExecute
        {
            get { return _canExecute; }
            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    OnCanExecuteChanged();
                }
            }
        }

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action act, bool canExecute)
            : this(_ => act(), canExecute)
        {
        }

        public RelayCommand(Action<object> act, bool canExecute)
        {
            _act = act;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _act(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return CommandCanExecute;
        }

        private void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}