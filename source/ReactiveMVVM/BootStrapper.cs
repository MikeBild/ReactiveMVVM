using System.Reflection;
using Autofac;
using ReactiveMVVM.Bus;

namespace ReactiveMVVM
{
    public abstract class BootStrapper
    {
        private readonly ContainerBuilder _builder;        
        public static IContainer Container { get; private set; }
        protected ContainerBuilder Builder
        {
            get { return _builder; }
        }

        protected BootStrapper()
        {
            _builder = new ContainerBuilder();
            Builder.Register(x => new RxMessageBus()).As(typeof(IMessageBus)).SingleInstance();
            Builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<ICommandMessage>().Named<ICommandMessage>(u => u.Name);
            Builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<IEventMessage>().Named<IEventMessage>(u => u.Name);                        
        }

        protected static void Startup()
        {
            var startupBuilder = new ContainerBuilder();            
            startupBuilder.RegisterAssemblyTypes(Assembly.GetCallingAssembly(),Assembly.GetExecutingAssembly()).AssignableTo<IStartable>().As<IStartable>().SingleInstance();
            startupBuilder.Build();            
        }

        public virtual void Configure()
        {
            Container = _builder.Build();                       
        }
    }
}