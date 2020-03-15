using System;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Linq;
namespace Rx12Sequence
{
    public static class RxExtension
    {
        public static IDisposable Intercept<T>(this IObservable<T> observable, string name)
        {
            return observable.Subscribe
                (
                onNext: (market) => { Console.WriteLine($"{name} {market}"); },
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

            Console.WriteLine("1. Sequence Combinators");
            Console.WriteLine("2. Zip");
            Console.WriteLine("3. Multiple combination with When");
            Console.WriteLine("4. Amb sequence (listen subject where message comes first");
            Console.WriteLine("5. Merge (merge multiple subject togeather as it comes on subject");


            switch (int.Parse(Console.ReadLine()))
            {
				case 1:
                    {
                        var mechnical = new BehaviorSubject<bool>(true);
                        var electrical = new BehaviorSubject<bool>(true);
                        var electronic = new BehaviorSubject<bool>(true);

                        mechnical.Intercept("mechnical");
                        electrical.Intercept("electrical");
                        electronic.Intercept("electronic");

						//combining all results and putting condition on it
						//here it's check that all system returns true
                        Observable.CombineLatest(electrical, mechnical, electronic)
                            .Select(a => a.All(b => b))
                            .Intercept("system working");

                        electronic.OnNext(false);
                        break;
                    }
                case 2:
                    {
                        var digit = Observable.Range(1, 10);
                        var chars = Observable.Range(1, 10).Select(x=> (char) ('A'+x));

                        chars.Zip(digit, (chars, digit) => $"{chars}-{digit}")
							.Intercept("zip");

                        break;
                    }
                case 3:
                    {
                        var digit = Observable.Range(1, 10);
                        var chars = Observable.Range(1, 10).Select(x => (char)('A' + x));
                        var punctuation = ".@!#$%^&*()".ToArray().ToObservable();

                        Observable.When(
                        chars.And(digit)
                             .And(punctuation)
                             .Then((c, d, p) => $"{c}{d}{p}")).Intercept("multiple combinitation");

                        break;
                    }
                case 4:
                    {
                        var subject1 = new Subject<int>();
                        var subject2 = new Subject<int>();
                        var subject3 = new Subject<int>();

                        subject1.Amb(subject2).Amb(subject3).Intercept("where-message-came-first");

                        subject3.OnNext(15);
                        subject1.OnNext(10);
                        subject2.OnNext(20);
                        subject3.OnNext(30);

                        subject3.OnCompleted();

                        break;
                    }
                case 5:
                    {
                        var subject1 = new Subject<int>();
                        var subject2 = new Subject<int>();
                        var subject3 = new Subject<int>();

                        subject1.Merge(subject2).Merge(subject3).Intercept("merge");

                        subject3.OnNext(15);
                        subject1.OnNext(10);
                        subject2.OnNext(20);
                        subject3.OnNext(30);

                        subject3.OnCompleted();

                        break;
                    }
            }
            Console.ReadKey();
        }
    }
}
