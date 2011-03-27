using System;
using System.Windows;
using System.Windows.Interactivity;
using ReactiveMVVM.Navigation;

namespace ReactiveMVVM.Xaml
{
    public class NavigateToTrigger : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty NavigateUrlProperty = DependencyProperty.Register("NavigateUrl", typeof(string), typeof(NavigateToTrigger), new PropertyMetadata(String.Empty, (s, e) => OnMessageChanged(s as NavigateToTrigger, e)));
        public static readonly DependencyProperty NavigationParameterProperty = DependencyProperty.Register("NavigationParameter", typeof(object), typeof(NavigateToTrigger), new PropertyMetadata(null, (s, e) => OnMessageChanged(s as NavigateToTrigger, e)));

        public object NavigationParameter
        {
            get { return GetValue(NavigationParameterProperty); }
            set { SetValue(NavigationParameterProperty, value); }
        }

        public string NavigateUrl
        {
            get { return (string)GetValue(NavigateUrlProperty); }
            set { SetValue(NavigateUrlProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            SilverlightAppHost.NavigationContext.Data = NavigationParameter;
            SilverlightAppHost.Navigation.Navigate(new Uri(NavigateUrl, UriKind.Relative));
            SilverlightAppHost.MessageBus.Publish(new NavigatedTo()
                                        {
                                            NavigationUrl = NavigateUrl,
                                            NavigationParameter = NavigationParameter
                                        });
        }

        private static void OnMessageChanged(NavigateToTrigger sender, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}