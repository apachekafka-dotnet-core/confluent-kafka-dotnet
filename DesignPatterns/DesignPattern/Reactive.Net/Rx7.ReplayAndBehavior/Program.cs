using System;
using System.Reactive.Subjects;

namespace Rx7.ReplayAndBehavior
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

    public static class RxExtension
    {
        public static IDisposable InterceptTopPrice<T>(this IObservable<T> observable, string name)
        {
            return observable.Subscribe
                (
                onNext: (market) => { Console.WriteLine($"Market price updated with {market}"); },
                onError: (exception) => { Console.WriteLine($"Error..{exception.Message}"); },
                onCompleted: () => { Console.WriteLine("Completed"); }
                );
        }

        public static void OnNext<T>(this IObserver<T> observer, params T[] prices)
        {
            foreach (var a in prices)
                observer.OnNext(a);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var subject = new ReplaySubject<Market>();
            var bvsubject = new BehaviorSubject<Market>(new Market { Price = 0 });


            //subject.OnNext(new Market[]
            //    {
            //        new Market() { Price = 1991 },
            //        new Market() { Price = 2222 }
            //    });

            bvsubject.Subscribe(new StockMarket());

            Console.ReadKey();
        }
    }
}
