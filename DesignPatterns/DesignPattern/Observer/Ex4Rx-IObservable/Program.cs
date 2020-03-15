using System;
using System.Collections.Generic;

namespace Ex4Rx_IObservable
{
    public class Market
    {
		public int Price { get; set; }
	}

	public class MarketIndex : IObservable<Market>
	{
		private List<IObserver<Market>> _observers;

		public MarketIndex()
		{
			_observers = new List<IObserver<Market>>();
		}

		public IDisposable Subscribe(IObserver<Market> observer)
		{
			if (!_observers.Contains(observer))
				_observers.Add(observer);

			return new Unsubscriber(_observers, observer);
		}

		public void UpdatePrice(Market loc)
		{
			foreach (var observer in _observers)
			{
				if (loc == null)
					observer.OnError(new Exception("Price can't be null"));
				else
					observer.OnNext(loc);
			}
		}
	}

	public class Unsubscriber : IDisposable
	{
		private List<IObserver<Market>> _observers;
		private IObserver<Market> _observer;

		public Unsubscriber(List<IObserver<Market>> observers, IObserver<Market> observer)
		{
			this._observers = observers;
			this._observer = observer;
		}

		public void Dispose()
		{
			if (_observer != null && _observers.Contains(_observer))
				_observers.Remove(_observer);
		}
	}

	public class TvNewsMarket : IObserver<Market>
	{
		public void OnCompleted()
		{
			Console.WriteLine($"Price change observer completed in News");
		}

		public void OnError(Exception error)
		{
			Console.WriteLine($"{error.Message}");
		}

		public void OnNext(Market value)
		{
			Console.WriteLine($"Price has changed with {value.Price}-- listend into news..");
		}
	}

	public class StockMarket : IObserver<Market>
	{
		public void OnCompleted()
		{
			Console.WriteLine($"Price change observer completed in stock market");
		}

		public void OnError(Exception error)
		{
			Console.WriteLine($"{error.Message}");
		}

		public void OnNext(Market value)
		{
			Console.WriteLine($"Price has changed with {value.Price}-- listend into stock market..");
		}
	}

	class Program
    {
        static void Main(string[] args)
        {
			var marketIndex = new MarketIndex();
			marketIndex.Subscribe(new TvNewsMarket());
			marketIndex.Subscribe(new StockMarket());
			marketIndex.UpdatePrice(new Market() { Price = 312 });
			Console.ReadKey();
        }
    }
}
