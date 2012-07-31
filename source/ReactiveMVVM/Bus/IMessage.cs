using System;

namespace ReactiveMVVM.Bus
{
    public interface IMessage
    {
        Guid Id { get; set; }
    }
}