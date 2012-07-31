using System;
using System.Windows.Input;
using Autofac;
using ReactiveMVVM.Bus;

namespace ReactiveMVVM.ViewModel.Command
{
    public class RegisterCommand<TMessage> : ICommand where TMessage : class, IMessage
    {
        private readonly Action<TMessage> _message;
        private readonly Func<TMessage, bool> _predicate;
        public event EventHandler CanExecuteChanged = delegate { };

        public RegisterCommand(Action<TMessage> message)
            : this(message, x => true)
        {
        }

        public RegisterCommand(Action<TMessage> message, Func<TMessage, bool> predicate)
        {
            _message = message;
            _predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return _predicate(parameter as TMessage);
        }

        public void Execute(object parameter)
        {
            var serviceBus = SilverlightAppHost.Container.Resolve<IMessageBus>();
            serviceBus.Register(_message);
        }
    }
}