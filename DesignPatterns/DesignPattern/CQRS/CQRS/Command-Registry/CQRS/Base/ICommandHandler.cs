using System;
using DI_MessageDispatcher.CQRS.Decorators;

namespace DI_MessageDispatcher.CQRS.Base
{
	public interface ICommandHandler<TCommand, TCommandResult> where TCommand : ICommand where TCommandResult : ICommandResult
	{
		TCommandResult Handle(TCommand command);
	}

	public interface IRetryCommandHandler<TCommand, TCommandResult> where TCommand : ICommand where TCommandResult : ICommandResult
	{
		TCommandResult Handle(TCommand command);
	}
}
