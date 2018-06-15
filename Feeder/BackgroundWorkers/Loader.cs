using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Feeder.BackgroundWorkers
{
    public class Loader : ILoader
    {
        private const int defaultDelay = 10;
        private readonly IObservable<long> delay;
        private readonly IList<IDisposable> observersList;

        public Loader()
        {
            observersList = new List<IDisposable>();

            delay =
                Observable.Interval(
                    TimeSpan.FromSeconds(ConfigurationManager.AppSettings["backgroundDelayInSeconds"] != null
                        ? int.Parse(ConfigurationManager.AppSettings["backgroundDelayInSeconds"])
                        : defaultDelay),
                    DispatcherScheduler.Current);
        }

        public void RegisterObserver(IObserver<long> observer)
        {
            var createdObserver = delay.Subscribe(observer);

            observersList.Add(createdObserver);
        }

        public void Dispose()
        {
            foreach (var disposable in observersList)
            {
                disposable.Dispose();
            }
        }
    }
}