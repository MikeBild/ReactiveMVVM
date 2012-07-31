using System;
using System.Reactive.Concurrency;

namespace ReactiveMVVM.Bus
{
    public interface IMessageBus
    {
        void Send(ICommandMessage domainCommand);
        IDisposable Register<T>(Action<T> action) where T : IMessage;
        IDisposable Register<T>(Action<T> action, Func<T, bool> predicate) where T : IMessage;
        IDisposable Register<T>(Action<T> action, IScheduler scheduler) where T : IMessage;
        void Publish(IEventMessage domainEvent);
    }
}