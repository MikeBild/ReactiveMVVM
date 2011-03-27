using System;
using System.Windows.Input;

namespace ReactiveMVVM.ViewModel.Command
{
    public class DelegateCommand<T> : ICommand where T : class
    {
        public event EventHandler CanExecuteChanged;

        readonly Func<T, bool> _canExecute;
        readonly Action<T> _executeAction;
        bool _canExecuteCache;

        public DelegateCommand(Action<T> executeAction)
            : this(executeAction, x=>true)
        {            
        }

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecute)
        {
            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var tempCanExecute = _canExecute(parameter as T);

            if (_canExecuteCache != tempCanExecute)
            {
                _canExecuteCache = tempCanExecute;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }
            return _canExecuteCache;
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter as T);
        }
    }
}