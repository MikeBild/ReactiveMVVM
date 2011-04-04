using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using ReactiveMVVM.Bus;
using ReactiveMVVM.Navigation;

namespace ReactiveMVVM
{
    public abstract class SilverlightAppHost
    {
        private readonly ContainerBuilder _builder = new ContainerBuilder();
        private readonly IList<Assembly> _mergedAssemblies = new List<Assembly>();
        public static IContainer Container { get; private set; }

        protected ContainerBuilder Builder { get { return _builder; } }

        protected SilverlightAppHost RegisterAssembly(Assembly assembly)
        {
            _mergedAssemblies.Add(Assembly.GetCallingAssembly());
            return this;
        }

        public virtual void Configure()
        {
            _builder.Register(x => new RxMessageBus()).As(typeof(IMessageBus)).SingleInstance();
            _builder.RegisterAssemblyTypes(_mergedAssemblies.ToArray()).AssignableTo<ICommandMessage>().Named<ICommandMessage>(u => u.Name);
            _builder.RegisterAssemblyTypes(_mergedAssemblies.ToArray()).AssignableTo<IEventMessage>().Named<IEventMessage>(u => u.Name);
        }

        public void Start()
        {
            Container = _builder.Build();

            var startupBuilder = new ContainerBuilder();
            startupBuilder.RegisterAssemblyTypes(_mergedAssemblies.ToArray()).AssignableTo<IStartable>().As<IStartable>().SingleInstance();
            startupBuilder.Build();
        }
        
        
        public static readonly DependencyProperty RegisterFrameProperty = DependencyProperty.RegisterAttached("RegisterFrame", typeof(Boolean), typeof(SilverlightAppHost), new PropertyMetadata(false));

        public static void SetRegisterFrame(UIElement element, Boolean value)
        {
            _navigation = (element as Frame);
            element.SetValue(RegisterFrameProperty, value);
        }
        public static Boolean GetRegisterFrame(UIElement element)
        {
            return (Boolean)element.GetValue(RegisterFrameProperty);
        }
                
        public static INavigationParameter NavigationContext
        {
            get { return Container.Resolve<INavigationParameter>(); }
        }
        public static IMessageBus MessageBus
        {
            get { return Container.Resolve<IMessageBus>(); }
        }

        private static Frame _navigation;
        public static Frame Navigation
        {
            get
            {                                   
                if(_navigation==null)
                throw new InvalidOperationException("No RootVisual page with navigation found.");
                return _navigation;
            }            
        }        
    }
}