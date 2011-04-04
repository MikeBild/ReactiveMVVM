using System;
using System.Windows;
using System.Windows.Interactivity;
using ReactiveMVVM.Navigation.Command;

namespace ReactiveMVVM.Xaml
{
    public class NavigateTrigger : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty NavigateUrlProperty = 
            DependencyProperty.Register("NavigateUrl",
            typeof(string), typeof(NavigateTrigger),
            new PropertyMetadata(String.Empty,
            (s, e) => OnMessageChanged(s as NavigateTrigger, e)));

        public static readonly DependencyProperty NavigationParameterProperty = 
            DependencyProperty.Register("NavigationParameter",
            typeof(object), typeof(NavigateTrigger),
            new PropertyMetadata(null,
            (s, e) => OnMessageChanged(s as NavigateTrigger, e)));

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

        private static void OnMessageChanged(NavigateTrigger sender, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}