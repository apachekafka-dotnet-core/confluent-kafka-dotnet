using System;

namespace Ex1.Basic
{
    public interface ICommand
    {
    }

    public interface ICommandResult
    {
    }

    public interface ICommandHandler<TCommand, TCommandResult> where TCommand:ICommand where TCommandResult : ICommandResult
    {
        TCommandResult Handle(TCommand command);
	}

    public sealed class CommandResult : ICommandResult
    {
        public bool IsSuccess  { get; set; }
        public Exception Error { get; set; }
    }

    public sealed class CreateUserCommand : ICommand
    {
		public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public sealed class DeleteUserCommand : ICommand
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CommandResult>
	{
		public CommandResult Handle(CreateUserCommand command)
		{
			throw new NotImplementedException();
		}
	}

    public sealed class DeleteCommandHandler : ICommandHandler<DeleteUserCommand, CommandResult>
    {
        public CommandResult Handle(DeleteUserCommand command)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var handler = new CreateUserCommandHandler();

			var response = handler.Handle(new CreateUserCommand
            {
                UserId = "User1",
                UserName = "User Name"
            });


            Console.WriteLine("Hello World!");
        }
    }
}
