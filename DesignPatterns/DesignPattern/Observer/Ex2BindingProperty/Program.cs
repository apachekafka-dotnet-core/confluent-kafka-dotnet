using System;
using System.ComponentModel;

namespace Ex2BindingProperty
{
	public class Market 
	{
		private int _price;
		public int Price
		{
			get { return _price; }
			set
			{
				_price = value;
			}
		}
	}

	public class MarketIndex
	{
		public BindingList<Market> Prices = new BindingList<Market>();

		public MarketIndex()
		{
			Prices.AllowEdit = true;
		}
		public void AddPrice (Market market)
		{
			Prices.Add(market);
		}

		public void UpdatePrice(Market market)
		{
			Prices[0].Price = market.Price;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var market = new MarketIndex();
			market.Prices.ListChanged += Prices_ListChanged;
			market.AddPrice(new Market { Price = 199 });

			market.UpdatePrice(new Market { Price = 299 });
			Console.ReadKey();
			Console.WriteLine("Hello World!");
		}

		private static void Prices_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemAdded)
			{
				Console.WriteLine($"Price has changed -- listend into stock market..");
			}
		}
	}
}
