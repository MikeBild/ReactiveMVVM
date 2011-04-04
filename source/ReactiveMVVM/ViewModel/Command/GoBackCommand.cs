using System;
using System.Windows.Input;

namespace ReactiveMVVM.ViewModel.Command
{
    public class GoBackCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };

        public GoBackCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {            
            SilverlightAppHost.Navigation.GoBack();
        }
    }
}