using System;
using DI_MessageDispatcher.CQRS.Base;
using DI_MessageDispatcher.CQRS.Command;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DI_MessageDispatcher.CQRS.Decorators
{	
	public sealed class LogDecorator<TCommand, TCommandResult>
		: ICommandHandler<TCommand, TCommandResult>
		where TCommand:ICommand
		where TCommandResult:ICommandResult
	{
		ICommandHandler<TCommand, TCommandResult> _handler;

		public LogDecorator(ICommandHandler<TCommand, TCommandResult> handler)
		{
			_handler = handler;
		}
		public TCommandResult Handle(TCommand command)
		{
			var obj = JsonConvert.SerializeObject(command);
			Console.WriteLine($"Executing command: {obj}");
			return _handler.Handle(command);
		}
	}
}
