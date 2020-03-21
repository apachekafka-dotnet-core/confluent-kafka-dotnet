using System;
using DI_MessageDispatcher.CQRS.Base;
using DI_MessageDispatcher.CQRS.CommandAttributes;
using DI_MessageDispatcher.CQRS.Decorators;

namespace DI_MessageDispatcher.CQRS.Command
{
	
	[AuditLog]
	//[Retry]
	public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CommandResult>
	{
		public CommandResult Handle(CreateUserCommand command)
		{
			throw new RetryException();
			//return new CommandResult { IsSuccess = true, };
		}
	}
}
