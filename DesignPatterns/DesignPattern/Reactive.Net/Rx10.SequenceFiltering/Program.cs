using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Rx10.SequenceFiltering
{
    public static class RxExtension
    {
        public static IDisposable Intercept<T>(this IObservable<T> observable, string name)
        {
            return observable.Subscribe
                (
                onNext: (market) => { Console.WriteLine($"{name} updated with {market}"); },
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
			//Example1
            var subject = Observable
                            .Range(10, 100)
                            .Where(a => a % 2 == 0)
							.Select(b => b*b)
							.Distinct()
                            .Intercept("intercept even number");

			//Example2 
            var numbers = new List<int> { 1, 312, 312, 31232, 13, 2, 32, 3, 21, 321, 31231 };
            numbers.ToObservable().Distinct().Intercept("distinct nummbers");


            //Example3:skip market price until message on new agency
            var marketPrice = new Subject<int>();
            var newsAgency = new Subject<string>();

            marketPrice
				.SkipUntil(newsAgency) 
				.Intercept("market price");

            marketPrice.OnNext(10, 12, 21, 34);
            newsAgency.OnNext("star news");
            marketPrice.OnNext(50, 42, 51, 44);


            //Example4: filtering based on type
            var subjectType = new Subject<object>();

            subjectType
                .OfType<float>() // skip market price until message on new agency
                .Intercept("float price");

            subjectType
               .OfType<int>() // skip market price until message on new agency
               .Intercept("int price");

            subjectType.OnNext(10.22f, 12, 2.1f, 34);

            //Example 5: interval when was the last time event generated

            var intervalSubject = Observable.Interval(TimeSpan.FromSeconds(10));
            intervalSubject.TimeInterval().Intercept("last time event produced");

            //Example 6: flatning the result using scheduler //remove and see the order

			//without scheduler: random
			//with scheduler: 1 12 123 1234

            var range = Observable.Range(1,4, Scheduler.Immediate)
						.SelectMany(a=> Observable.Range(1,a, Scheduler.Immediate))
						.Intercept("select manay ordering of event");


            //Example 7: accumlating running numbers
            var sumSubject = new Subject<int>();
            sumSubject.Scan(0.0, (a, b) => a + b).Intercept("Sum number");
            sumSubject.OnNext(1, 2, 3, 4);
            Console.ReadKey();
        }
    }
}
