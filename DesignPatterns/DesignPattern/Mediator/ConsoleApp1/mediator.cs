using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator1
{
	public abstract class Mediator
	{
		public abstract void Send(string message, Colleague colleague);
	}

	public class ConcreteMediator : Mediator
	{

		public Colleague1 Colleague1 { get; set; }
		public Colleague2 Colleague2 { get; set; }

		public override void Send(string message, Colleague colleague)
		{
			if (Colleague1 == colleague)
				this.Colleague2.HandleNotification(message);
			else
				this.Colleague1.HandleNotification(message);
		}
	}
}
