using System;
using System.ComponentModel;
using Autofac;
using ReactiveMVVM.Bus;

namespace ReactiveMVVM.ViewModel
{
    public abstract class ViewController<T> : IViewController<T> where T : IViewModel, new()
    {
        public IMessageBus MessageBus { get; private set; }
        public T ViewModel { get; private set; }

        protected ViewController()
        {
            ViewModel = new T();
            if (!DesignerProperties.IsInDesignTool)
            {
                MessageBus = SilverlightAppHost.Container.Resolve<IMessageBus>();
                RegisterInstanceInServiceLocator(this, typeof (ViewController<T>));
            }
        }

        private static void RegisterInstanceInServiceLocator(object instance, Type asType)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(instance).As(asType).ExternallyOwned();
            builder.Update(SilverlightAppHost.Container);
        }
    }
}