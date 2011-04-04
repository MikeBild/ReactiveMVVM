using System;
using System.Windows.Input;

namespace ReactiveMVVM.ViewModel.Command
{
    public class GoForwardCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };

        public GoForwardCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SilverlightAppHost.Navigation.GoForward();
        }
    }
}