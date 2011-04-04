using System;
using System.Windows.Input;
using Autofac;
using ReactiveMVVM.Bus;

namespace ReactiveMVVM.ViewModel.Command
{
    public class PublishCommand<TBinding> : ICommand where TBinding : class
    {
        private readonly Func<TBinding, IEventMessage> _message;
        public event EventHandler CanExecuteChanged = delegate { };

        public PublishCommand(Func<TBinding, IEventMessage> message)
        {
            _message = message;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var serviceBus = SilverlightAppHost.Container.Resolve<IMessageBus>();
            serviceBus.Publish(_message(parameter as TBinding));
        }
    }
}