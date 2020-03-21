using System;
namespace DI_MessageDispatcher.CQRS.Base
{
    public sealed class CommandResult : ICommandResult
    {
        public bool IsSuccess { get; set; }
        public Exception Error { get; set; }
    }
}
