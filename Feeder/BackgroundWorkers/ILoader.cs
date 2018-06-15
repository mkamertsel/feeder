using System;

namespace Feeder.BackgroundWorkers
{
    public interface ILoader
    {
        void RegisterObserver(IObserver<long> observer);
        void Dispose();
    }
}