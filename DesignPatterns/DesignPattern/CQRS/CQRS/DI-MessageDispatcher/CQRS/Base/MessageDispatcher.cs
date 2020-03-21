using System;
namespace DI_MessageDispatcher.CQRS.Base
{
	public interface IMessageDispatcher
	{
		T Send<T>(ICommand cmd);
	}
	public class MessageDispatcher: IMessageDispatcher
	{
		IServiceProvider _serviceProvider = null;
		public MessageDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public T Send<T>(ICommand cmd)
		{
			Type type = typeof(ICommandHandler<,>);//creating handler
			Type[] types = { cmd.GetType(), typeof(T) }; // getting provided command type
			Type cmdType = type.MakeGenericType(types); // creating actual command impleting ICommand and calling send method 

			dynamic handler = _serviceProvider.GetService(cmdType); // DI- getting concreat object
			T result = handler.Handle((dynamic) cmd); // calling Handle (resolved at run time)
			return result;
		}
	}
}
