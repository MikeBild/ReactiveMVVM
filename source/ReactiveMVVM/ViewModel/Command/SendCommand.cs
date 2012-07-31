using System;
using System.Windows.Input;
using Autofac;
using ReactiveMVVM.Bus;

namespace ReactiveMVVM.ViewModel.Command
{
    public class SendCommand<TBinding> : ICommand where TBinding : class
    {
        private readonly Func<TBinding , ICommandMessage> _message;
        public event EventHandler CanExecuteChanged = delegate { };

        public SendCommand(Func<TBinding , ICommandMessage> message)
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
            serviceBus.Send(_message(parameter as TBinding));
        }
    }
}