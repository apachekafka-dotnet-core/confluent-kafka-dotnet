using System;
using DI_MessageDispatcher.CQRS.Base;
using DI_MessageDispatcher.CQRS.Decorators;

namespace DI_MessageDispatcher.CQRS.Command
{
	public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CommandResult>
	{
		public CommandResult Handle(CreateUserCommand command)
		{
			throw new RetryException();
			//return new CommandResult { IsSuccess = true, };
		}
	}
}
