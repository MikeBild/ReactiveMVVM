using System;
using System.Windows.Input;

namespace ReactiveMVVM.ViewModel.Command
{
    public class NavigateCommand : ICommand
    {
        private readonly string _navigateUrl;
        private readonly object _navigationParameter;
        public event EventHandler CanExecuteChanged = delegate { };

        public NavigateCommand(string navigateUrl, object navigationParameter = null)
        {
            _navigateUrl = navigateUrl;
            _navigationParameter = navigationParameter;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SilverlightAppHost.NavigationContext.Data = _navigationParameter;
            SilverlightAppHost.Navigation.Navigate(new Uri(_navigateUrl, UriKind.Relative));
        }
    }
}