using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AirMVVM.Commands
{
    public class RelayCommand : BaseCommand
    {
        private readonly Action<RelayCommand, object> _selfExecuteWithParams;
        private readonly Func<RelayCommand, object, bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null) :
            this((_, __) => execute(), 
                canExecute != null ? (_, __) => canExecute() : (Func<RelayCommand, object, bool>)null)
        { }

        public RelayCommand(Action<object> execute, Func<bool> canExecute = null) : 
            this((_, param) => execute(param), 
                canExecute != null ? (_, __) => canExecute() : (Func<RelayCommand, object, bool>)null)
        { }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null) :
            this((_, param) => execute(param),
                canExecute != null ? (_, param) => canExecute(param) : (Func<RelayCommand, object, bool>)null)
        { }

        public RelayCommand(Action<RelayCommand> execute, Func<RelayCommand, bool> canExecute = null) :
            this((self, _) => execute(self),
                canExecute != null ? (self, _) => canExecute(self) : (Func<RelayCommand, object, bool>)null)
        { }

        public RelayCommand(Action<RelayCommand, object> execute, Func<RelayCommand, bool> canExecute = null) :
             this(execute,
                canExecute != null ? (self, _) => canExecute(self) : (Func<RelayCommand, object, bool>)null)
        { }

        public RelayCommand(Action<RelayCommand, object> execute, Func<RelayCommand, object, bool> canExecute = null)
        {
            _selfExecuteWithParams = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            Storage = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Storage { get; private set; }

        public override bool CanExecute(object parameter)
        {
            return this?._canExecute(this, parameter) ?? true;
        }

        public override void Execute(object parameter)
        {
            _selfExecuteWithParams.Invoke(this, parameter);
        }
    }
}