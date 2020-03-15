using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Rx.Subject
{
    public class Market
    {
        public int Price { get; set; }
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
            var subject = new Subject<Market>();

            var consoleSubcriber = subject.Subscribe
				(
				onNext: (market) => { Console.WriteLine($"Market price updated with {market.Price}");},
				onError: (exception) => { Console.WriteLine($"Error..{exception.Message}"); },
				onCompleted: () => { Console.WriteLine("Completed"); }
				);

            var stockMarket = subject.Subscribe(new StockMarket());

            subject.OnNext(new Market() { Price = 1991 });

            consoleSubcriber.Dispose();

            subject.OnNext(new Market() { Price = 1331 });

            Console.ReadKey();

        }

    }
}
