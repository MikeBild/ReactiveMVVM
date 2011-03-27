using System;
using System.Collections.Generic;
using System.Concurrency;
using System.Linq;

namespace ReactiveMVVM.Bus
{
    public class RxMessageBus : IMessageBus
    {
        private readonly Subject<IMessage> _subject = new Subject<IMessage>();        

        public void Send(ICommandMessage domainCommand)
        {
            try
            {           
                _subject.OnNext(domainCommand);
            }
            catch (Exception e)
            {
                _subject.OnError(e);
            }
        }

        public IDisposable Register<T>(Action<T> action) where T: IMessage
        {
            return _subject
                .OfType<T>()
                .Subscribe(action);
        }

        public IDisposable Register<T>(Action<T> action, Func<T,bool> predicate) where T : IMessage
        {
            return _subject
                .OfType<T>()
                .Where(predicate)
                .Subscribe(action);
        }

        public IDisposable Register<T>(Action<T> action, IScheduler scheduler) where T : IMessage
        {
            return _subject.OfType<T>()
                .SubscribeOn(scheduler)
                .Subscribe(action);
        }


        public void Publish(IEventMessage domainEvent)
        {
            try
            {
                _subject.OnNext(domainEvent);
            }
            catch (Exception e)
            {
                _subject.OnError(e);
            }
        }

    }   
}