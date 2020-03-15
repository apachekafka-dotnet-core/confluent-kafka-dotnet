using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mediator1
{
	public abstract class Mediator
	{
		public abstract void Send(string message, Colleague colleague);
		public abstract void Register(Colleague colleague);
	}

	public class ConcreteMediator : Mediator
	{
		List<Colleague> colleagues = new List<Colleague>();

		public override void Register(Colleague colleague)
		{
			colleague.SetMediator(this);
			colleagues.Add(colleague);
		}

		public override void Send(string message, Colleague colleague)
		{
			colleagues.Where(a => a != colleague)
				.ToList()
				.ForEach((c) => c.HandleNotification(message));
		}
	}
}
