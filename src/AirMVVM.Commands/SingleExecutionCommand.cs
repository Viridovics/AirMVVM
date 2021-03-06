﻿using System;
using System.Windows.Input;

namespace AirMVVM.Commands
{
    public class SingleExecutionCommand : BaseCommand
    {
        private readonly Action _execute;
        private readonly Action<object> _executeWithParam;

        private bool _commandStarted = false;

        public SingleExecutionCommand(Action execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            _execute = execute;
            _executeWithParam = null;
        }

        public SingleExecutionCommand(Action<object> execute)
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
            _execute?.Invoke();
            _executeWithParam?.Invoke(parameter);
        }
    }
}
