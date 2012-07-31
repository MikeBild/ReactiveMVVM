using System;
using ReactiveMVVM.Bus;

namespace ReactiveMVVM.Navigation.Command
{
    public class NavigatedTo : IEventMessage
    {
        public Guid Id { get; set; }
        public string NavigationUrl { get; set; }
        public object NavigationParameter { get; set; }
    }
}