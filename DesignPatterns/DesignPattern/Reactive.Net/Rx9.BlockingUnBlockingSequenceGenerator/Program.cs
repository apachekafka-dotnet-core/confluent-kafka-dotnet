using System;
using System.Reactive.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Reactive.Disposables;

namespace Rx9.BlockingUnBlockingSequenceGenerator
{
    public static class RxExtension
    {
        public static IDisposable Intercept<T>(this IObservable<T> observable, string name)
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
        public static IObservable<int> BlockingSequence()
        {
            var subject = new ReplaySubject<int>();
            subject.OnNext(100);
            subject.OnNext(200);
            subject.OnNext(300);
            subject.OnCompleted();
            Thread.Sleep(3000);

            return subject;
        }

        public static IObservable<int> UnBlockingSequence()
        {
            var subject = Observable.Create<int>(observer => {
                observer.OnNext(100);
                observer.OnNext(200);
                observer.OnNext(300);
                Thread.Sleep(3000);
                observer.OnCompleted();
                return Disposable.Empty;
            });
            return subject;
        }

        public static IObservable<string> TimerSequence()
        {
            var subject = Observable.Create<string>(observer => {
                var timer = new System.Timers.Timer(1000);
                timer.Elapsed += (a,e)=> { observer.OnNext(e.SignalTime.ToString());};
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
                return ()=> {
                    timer.Elapsed -= Timer_Elapsed;
                    timer.Stop();
                };
            });
            return subject;
        }

		private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
            Console.WriteLine($"{e.SignalTime } elapsed");
		}

		static void Main(string[] args)
        {
            //BlockingSequence().Intercept("Intercept value..bloking sequence");
            //UnBlockingSequence().Intercept("Intercept value..unbloking sequence");

            var subject = TimerSequence();
            var firstSubcriber = subject.Subscribe((a) => { Console.WriteLine($"{a} first subscriber....."); }) ;
            Console.ReadKey();
            firstSubcriber.Dispose();

            var intercept = subject.Intercept<string>("Intercept value..Timer sequence");
            Console.ReadKey();
            intercept.Dispose();

            Console.ReadKey();
        }
    }
}
