using System;
namespace DI_MessageDispatcher.CQRS.Base
{
	public interface ICommandHandler<TCommand, TCommandResult> where TCommand : ICommand where TCommandResult : ICommandResult
	{
		TCommandResult Handle(TCommand command);
	}
}
