using System;
using DI_MessageDispatcher.CQRS.Base;
using DI_MessageDispatcher.CQRS.Decorators;

namespace DI_MessageDispatcher.CQRS.Command
{
    public sealed class CreateUserCommand : ICommand
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
