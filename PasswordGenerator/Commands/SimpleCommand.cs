using System;
using System.Windows.Input;

namespace PasswordGenerator
{
    public class SimpleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<bool> _canExecute;
        private Action _execute;

        public SimpleCommand(Func<bool> canExecute, Action execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute.Invoke();
            else
                return true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
