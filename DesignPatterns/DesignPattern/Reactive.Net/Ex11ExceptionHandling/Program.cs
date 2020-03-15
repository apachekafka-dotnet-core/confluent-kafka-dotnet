using System;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace Ex11ExceptionHandling
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
            Console.WriteLine("1. Simple Error redirect");
            Console.WriteLine("2. OnError resume next");
            Console.WriteLine("3. Retry/attempts");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1: {
                        var subject = new Subject<int>();
                        //var errorSubject = new Subject<int>();

                        subject
                            .Catch<int,ArgumentNullException>( ex => Observable.Range(20, 20))
                            .Catch(Observable.Range(1, 20))
							.Intercept("Exception 1 Sample");

                        //subject.OnNext(10, 12, 121, 212);
                        subject.OnError(new Exception("This is error"));
                        subject.OnError(new ArgumentNullException("This is error"));
                        break;
					}
                case 2:
                    {
                        var subject1 = new Subject<int>();
                        var subject2 = new Subject<int>();

                        subject1.OnErrorResumeNext(subject2).Intercept("Join two subject and resume even if error in sequence");

                        subject1.OnNext(10, 11, 12);
                        subject1.OnError(new Exception("This is error"));
                        subject2.OnNext(13, 14, 15);

                        break;
                    }
                case 3:
                    {
                        SucceedAfterTry(2)
							.Retry(3)
							.Intercept("retry event");

                        break;
                    }
            }

			
        }

        public static IObservable<int> SucceedAfterTry(int attempts)
        {
            int count = 0;
            return Observable.Create<int>(o => {
                if (count < attempts)
                {
                    Console.WriteLine("Failed retrying");
                    o.OnError(new Exception("Failed"));
                }
                else
                {
                    Console.WriteLine("Succeed");
                    o.OnNext(22);
                }
                count++;
                return Disposable.Empty;
            });
		}
    }
}
