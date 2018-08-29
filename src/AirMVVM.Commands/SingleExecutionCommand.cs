using System;
using System.Windows.Input;

namespace AirMVVM.Commands
{
    public class SingleExecutionCommand : ICommand
    {
        private readonly Action _execute;

        private bool _commandStarted = false;

        public SingleExecutionCommand(Action execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
        }
        public event EventHandler CanExecuteChanged;

        private void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return !_commandStarted;
        }

        public void Execute(object parameter)
        {
            if (_commandStarted)
            {
                return;
            }
            _commandStarted = true;
            RaiseCanExecuteChanged();
            _execute();
        }
    }
}
