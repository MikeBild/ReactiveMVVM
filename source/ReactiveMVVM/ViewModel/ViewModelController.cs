using System;
using System.ComponentModel;
using Autofac;
using ReactiveMVVM.Bus;

namespace ReactiveMVVM.ViewModel
{
    public abstract class ViewModelController<T> : IViewModelController<T> where T : IViewModel, new()
    {
        public IMessageBus MessageBus { get; private set; }
        public T ViewModel { get; private set; }

        protected ViewModelController()
        {
            ViewModel = new T();
            if (!DesignerProperties.IsInDesignTool)
            {
                MessageBus = BootStrapper.Container.Resolve<IMessageBus>();
                RegisterInstanceInServiceLocator(this, typeof (ViewModelController<T>));
            }
        }

        private static void RegisterInstanceInServiceLocator(object instance, Type asType)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(instance).As(asType).ExternallyOwned();
            builder.Update(BootStrapper.Container);
        }
    }
}