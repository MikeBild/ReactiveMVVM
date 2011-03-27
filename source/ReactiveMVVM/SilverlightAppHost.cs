using System;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using ReactiveMVVM.Bus;
using ReactiveMVVM.Navigation;

namespace ReactiveMVVM
{
    public abstract class SilverlightAppHost : IStartable
    {
        public static readonly DependencyProperty RegisterFrameProperty = DependencyProperty.RegisterAttached("RegisterFrame", typeof(Boolean), typeof(SilverlightAppHost), new PropertyMetadata(false));

        public static void SetRegisterFrame(UIElement element, Boolean value)
        {
            _navigation = (element as INavigate);
            element.SetValue(RegisterFrameProperty, value);
        }
        public static Boolean GetRegisterFrame(UIElement element)
        {
            return (Boolean)element.GetValue(RegisterFrameProperty);
        }
                
        public static INavigationParameter NavigationContext
        {
            get { return BootStrapper.Container.Resolve<INavigationParameter>(); }
        }
        public static IMessageBus MessageBus
        {
            get { return BootStrapper.Container.Resolve<IMessageBus>(); }
        }

        private static INavigate _navigation;
        public static INavigate Navigation
        {
            get
            {                                   
                if(_navigation==null)
                throw new InvalidOperationException("No RootVisual page with navigation found.");
                return _navigation;
            }            
        }

        public abstract void Start();
    }
}