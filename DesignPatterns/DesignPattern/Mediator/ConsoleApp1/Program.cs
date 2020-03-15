using Mediator1;
using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var mediator = new ConcreteMediator();

			mediator.Colleague1 = new Colleague1(mediator);
			mediator.Colleague2 = new Colleague2(mediator);

			mediator.Send("Hello world this - colleage 1 message", mediator.Colleague1);
			mediator.Send("Hello world this - colleage 2 message", mediator.Colleague2);

			Console.ReadKey();
		}
	}
}
