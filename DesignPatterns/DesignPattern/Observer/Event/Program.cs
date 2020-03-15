using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Event
{
	public class Market:INotifyPropertyChanged
	{
		private int _price;
		public int Price
		{	get { return _price; }
			set
			{
				_price = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		
	}

	class Program
	{
		static void Main(string[] args)
		{
			Market market = new Market();
			market.PropertyChanged += StockMarket;
			market.PropertyChanged += TvNews;

			market.Price = 199;

			Console.WriteLine("Hello World!");
		}

		private static void StockMarket(object obj, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			Console.WriteLine($"{propertyChangedEventArgs.PropertyName} has changed..");
		}
		private static void TvNews(object obj, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			Console.WriteLine($"{propertyChangedEventArgs.PropertyName} has changed..");
		}
	}
}
