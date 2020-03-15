using System;
using System.Collections.Generic;

namespace ObserverPattern
{
	public interface ISubject
	{
		void Attach(IObserver observer);

		void Notify();

		int Price { get; set; }
	}

	public interface IObserver
	{
		void Update(ISubject subject);
	}

	public class Subject : ISubject
	{
		private int _price;
		public int Price
		{
			get { return _price; }
			set
			{
				_price = value;
				Notify();
			}
		}

		private List<IObserver> _observers = new List<IObserver>();

		public void Attach(IObserver observer)
		{
			this._observers.Add(observer);
		}

		public void Notify()
		{
			foreach (var observer in _observers)
			{
				observer.Update(this);
			}
		}
	}

	public class StockMarket : IObserver
	{
		public void Update(ISubject subject)
		{
			Console.WriteLine($"Price has changed -- listend into stock market..");
		}
	}

	public class TvNewsMarket : IObserver
	{
		public void Update(ISubject subject)
		{
			Console.WriteLine($"Price has changed.. listend into news");
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var topic = new Subject();
			topic.Attach(new TvNewsMarket());
			topic.Attach(new StockMarket());

			topic.Price = 39;

			Console.ReadKey();
		}
	}
}
