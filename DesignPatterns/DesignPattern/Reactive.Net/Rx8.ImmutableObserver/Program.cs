using System;
using System.Collections.Immutable;
using System.Reactive.Disposables;
namespace Rx8.ImmutableObserver
{
	public class Market : IObservable<int>
	{
		private int _price;
		private ImmutableHashSet<IObserver<int>> _observers =  ImmutableHashSet<IObserver<int>>.Empty;

		public IDisposable Subscribe(IObserver<int> observer)
		{
			_observers = _observers.Add(observer);
			return Disposable.Create(()=> {
				_observers = _observers.Remove(observer);
			});
		}

		public void Publish(int price)
		{
			_price = price;
			foreach (var o in _observers)
				o.OnNext(price);
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

	public class NewsMarket : IObserver<int>
	{
		public void OnCompleted()
		{
			Console.WriteLine($"Price change observer completed in news market");
		}

		public void OnError(Exception error)
		{
			Console.WriteLine($"{error.Message}");
		}

		public void OnNext(int value)
		{
			Console.WriteLine($"Price has changed with {value}-- listend into news market..");
		}
	}

	class Program
    {
        static void Main(string[] args)
        {
			var marketIndex = new Market();
			marketIndex.Subscribe(new StockMarket());
			marketIndex.Subscribe(new NewsMarket());
			marketIndex.Publish(12222);
			Console.ReadKey();
		}
    }
}
