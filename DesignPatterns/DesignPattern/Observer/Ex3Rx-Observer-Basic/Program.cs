using System;
using System.Collections.Generic;

namespace Ex3Rx_Observer_Basic
{
	public interface ISubject
	{
		void Attach(IObserver<int> observer);

		void Notify();

		int Price { get; set; }
	}

	
	public class Subject 
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

		private List<IObserver<int>> _observers = new List<IObserver<int>>();

		public void Attach(IObserver<int> observer)
		{
			this._observers.Add(observer);
		}

		public void Notify()
		{
			foreach (var observer in _observers)
			{
				observer.OnNext(this.Price);
			}
		}
	}

	public class StockMarket : IObserver<int>
	{
		public void OnCompleted()
		{
			Console.WriteLine($"Price change observer completed in stock market");
		}

		public void OnError(Exception error)
		{
			Console.WriteLine($"{error.Message}");
		}

		public void OnNext(int value)
		{
			Console.WriteLine($"Price has changed with {value}-- listend into stock market..");
		}
	}

	public class TvNewsMarket : IObserver<int>
	{
		public void OnCompleted()
		{
			Console.WriteLine($"Price change observer completed in News");
		}

		public void OnError(Exception error)
		{
			Console.WriteLine($"{error.Message}");
		}

		public void OnNext(int value)
		{
			Console.WriteLine($"Price has changed with {value}-- listend into news..");
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
