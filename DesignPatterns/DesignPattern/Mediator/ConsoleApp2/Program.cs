using Mediator1;
using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var mediator = new ConcreteMediator();

			//var coll1 = new Colleague1();
			//var coll2 = new Colleague2();

			//mediator.Register(coll1);
			//mediator.Register(coll2);

			var coll1 = mediator.CreateColleague<Colleague1>();
			var coll2 = mediator.CreateColleague<Colleague2>();


			coll1.Send("Hello world this - colleage 1 message");
			coll2.Send("Hello world this - colleage 2 message");

			Console.ReadKey();
		}
	}
}
