using System;
using System.Windows.Input;

namespace AirMVVM.Commands
{
    public class SequentialCommand : BaseCommand
    {
        private readonly Action<Action> _execute;
        private readonly Action<object, Action> _executeWithParam;

        private bool _commandStarted = false;

        public SequentialCommand(Action<Action> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            _execute = execute;
            _executeWithParam = null;
        }

        public SequentialCommand(Action<object, Action> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            _execute = null;
            _executeWithParam = execute;
        }

        public override bool CanExecute(object parameter)
        {
            return !_commandStarted;
        }

        public override void Execute(object parameter)
        {
            if (_commandStarted)
            {
                return;
            }
            _commandStarted = true;
            RaiseCanExecuteChanged();
            _execute?.Invoke(CommandFinishedCallback);
            _executeWithParam?.Invoke(parameter, CommandFinishedCallback);
        }

        private void CommandFinishedCallback()
        {
            _commandStarted = false;
            RaiseCanExecuteChanged();
        }
    }
}
