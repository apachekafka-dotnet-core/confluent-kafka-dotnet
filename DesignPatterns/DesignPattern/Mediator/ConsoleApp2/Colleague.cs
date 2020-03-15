using System;

namespace Mediator1
{
	public abstract class Colleague
	{
		protected Mediator Mediator;
		public void SetMediator(Mediator mediator)
		{
			this.Mediator = mediator;
		}

		public virtual void Send(string message)
		{
			this.Mediator.Send(message, this);
		}

		public abstract void HandleNotification(string message);

	}

	public class Colleague1:Colleague
	{
		public override void HandleNotification(string message)
		{
			Console.WriteLine($"Colleague1 service received a notification:{message}");
		}
	}

	public class Colleague2 : Colleague
	{
		public override void HandleNotification(string message)
		{
			Console.WriteLine($"Colleague2 service received a notification:{message}");
		}
	}
}
