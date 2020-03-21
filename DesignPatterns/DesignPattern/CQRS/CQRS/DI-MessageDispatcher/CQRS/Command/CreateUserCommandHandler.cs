using System;
using DI_MessageDispatcher.CQRS.Base;

namespace DI_MessageDispatcher.CQRS.Command
{
	public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CommandResult>
	{
		public CommandResult Handle(CreateUserCommand command)
		{
			return new CommandResult { IsSuccess = true, };
		}
	}
}
