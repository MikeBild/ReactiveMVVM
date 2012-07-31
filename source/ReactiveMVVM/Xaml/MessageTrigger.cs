using System;
using System.Windows;
using System.Windows.Interactivity;
using Autofac;
using ReactiveMVVM.Bus;

namespace ReactiveMVVM.Xaml
{
    public class MessageTrigger : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandMessageProperty = DependencyProperty.Register("CommandMessage", typeof(string), typeof(MessageTrigger), new PropertyMetadata(String.Empty, (s, e) => OnMessageChanged(s as MessageTrigger, e)));
        public static readonly DependencyProperty EventMessageProperty = DependencyProperty.Register("EventMessage", typeof(string), typeof(MessageTrigger), new PropertyMetadata(String.Empty, (s, e) => OnMessageChanged(s as MessageTrigger, e)));
        public static readonly DependencyProperty ObjectGuidParameterProperty = DependencyProperty.Register("ObjectGuidParameter", typeof(Guid), typeof(MessageTrigger), new PropertyMetadata(Guid.NewGuid(), (s, e) => OnMessageChanged(s as MessageTrigger, e)));

        public Guid ObjectGuidParameter
        {
            get { return (Guid)GetValue(ObjectGuidParameterProperty); }
            set { SetValue(ObjectGuidParameterProperty, value); }
        }

        public string CommandMessage
        {
            get { return (string)GetValue(CommandMessageProperty); }
            set { SetValue(CommandMessageProperty, value); }
        }
        public string EventMessage
        {
            get { return (string)GetValue(EventMessageProperty); }
            set { SetValue(EventMessageProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (!SendToBusIfCommandMessage())
                PublishToBusIfEventMessage();
        }

        private bool PublishToBusIfEventMessage()
        {
            object message;
            if (!String.IsNullOrEmpty(EventMessage))
                if (SilverlightAppHost.Container.TryResolveNamed(EventMessage, typeof(IEventMessage), out message))
                {
                    ((IMessage)message).Id = ObjectGuidParameter;
                    SilverlightAppHost.MessageBus.Publish(message as IEventMessage);
                    return true;
                }
            return false;
        }

        private bool SendToBusIfCommandMessage()
        {
            object message;
            if (!String.IsNullOrEmpty(CommandMessage))
                if (SilverlightAppHost.Container.TryResolveNamed(CommandMessage, typeof(ICommandMessage), out message))
                {
                    ((IMessage)message).Id = ObjectGuidParameter;
                    SilverlightAppHost.MessageBus.Send(message as ICommandMessage);
                    return true;
                }
            return false;
        }

        private static void OnMessageChanged(MessageTrigger sender, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}