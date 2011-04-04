using System;
using System.ComponentModel;
using Autofac;

namespace ReactiveMVVM.Navigation
{
    public class NavigationParameter : INavigationParameter
    {
        public object Data { get; set; }
        
        public NavigationParameter()
        {                        
            if (!DesignerProperties.IsInDesignTool)            
                RegisterInstanceInServiceLocator(this, typeof(INavigationParameter));            
        }

        private static void RegisterInstanceInServiceLocator(object instance, Type asType)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(instance).As(asType).ExternallyOwned();
            builder.Update(SilverlightAppHost.Container);
        }
    }
}