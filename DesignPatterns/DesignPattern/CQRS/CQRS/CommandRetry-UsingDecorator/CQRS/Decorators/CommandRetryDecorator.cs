using System;
using DI_MessageDispatcher.CQRS.Base;
using DI_MessageDispatcher.CQRS.Command;
using Microsoft.Extensions.Logging;

namespace DI_MessageDispatcher.CQRS.Decorators
{	
	public class RetryException:Exception
	{

	}
	public sealed class CommandRetryDecorator<TCommand, TCommandResult>
		: ICommandHandler<TCommand, TCommandResult>
		where TCommand:ICommand
		where TCommandResult:ICommandResult
	{
		ICommandHandler<TCommand, TCommandResult> _handler;

		public CommandRetryDecorator(ICommandHandler<TCommand, TCommandResult> handler)
		{
			_handler = handler;
		}
		public TCommandResult Handle(TCommand command)
		{
			Exception exp = null;
			for (int retry = 0;; retry++)
			{
				try
				{
					return _handler.Handle(command);
				}
				catch (Exception ex) when (ex.GetType() == typeof(RetryException) && retry<3)
				{
					exp = ex;
					Console.WriteLine($"Retry command..{command.ToString()}, try count {retry}");
				}
			}
			throw exp ?? new Exception("Failed to execute command");
		}
	}
}
