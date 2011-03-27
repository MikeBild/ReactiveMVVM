using System.Collections.Generic;
using System.Reflection;
using Autofac;
using ReactiveMVVM.Bus;
using System.Linq;

namespace ReactiveMVVM
{
    public abstract class BootStrapper
    {
        private readonly ContainerBuilder _builder;
        private readonly IList<Assembly> _mergedAssemblies = new List<Assembly>();
        public static IContainer Container { get; private set; }
        
        protected ContainerBuilder Builder { get { return _builder; }}

        protected BootStrapper RegisterAssembly(Assembly assembly)
        {
            _mergedAssemblies.Add(Assembly.GetCallingAssembly());
            return this;
        }

        protected BootStrapper()
        {
            _builder = new ContainerBuilder();
        }

        protected void Startup()
        {
            var startupBuilder = new ContainerBuilder();
            startupBuilder.RegisterAssemblyTypes(_mergedAssemblies.ToArray()).AssignableTo<IStartable>().As<IStartable>().SingleInstance();
            startupBuilder.Build();
        }

        public virtual void Configure()
        {
            _builder.Register(x => new RxMessageBus()).As(typeof(IMessageBus)).SingleInstance();
            _builder.RegisterAssemblyTypes(_mergedAssemblies.ToArray()).AssignableTo<ICommandMessage>().Named<ICommandMessage>(u => u.Name);
            _builder.RegisterAssemblyTypes(_mergedAssemblies.ToArray()).AssignableTo<IEventMessage>().Named<IEventMessage>(u => u.Name);
            Container = _builder.Build();
        }
    }
}