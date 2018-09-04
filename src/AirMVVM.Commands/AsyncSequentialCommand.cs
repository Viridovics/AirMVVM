using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirMVVM.Commands
{
    public class AsyncSequentialCommand : BaseCommand
    {
        private readonly Task _execution;
        private bool _commandStarted = false;

        public AsyncSequentialCommand(Task execution)
        {
            if (execution == null)
            {
                throw new ArgumentNullException(nameof(execution));
            }
            _execution = execution;
        }

        public override bool CanExecute(object parameter)
        {
            return !_commandStarted;
        }

        public override async void Execute(object parameter)
        {
            if (_commandStarted)
            {
                return;
            }
            _commandStarted = true;
            RaiseCanExecuteChanged();
            await _execution;
            CommandFinished();
        }

        private void CommandFinished()
        {
            _commandStarted = false;
            RaiseCanExecuteChanged();
        }
    }
}
