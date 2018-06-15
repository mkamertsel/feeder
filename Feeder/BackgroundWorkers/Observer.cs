using System;
using System.Threading.Tasks;

namespace Feeder.BackgroundWorkers
{
    public class Observer<T> : IObserver<long>
    {
        private readonly Func<Task<T>> loadAction;
        private readonly Action<T> setAction;

        public Observer(Func<Task<T>> loadAction, Action<T> setAction)
        {
            this.loadAction = loadAction;
            this.setAction = setAction;
        }

        public async void OnNext(long value)
        {
            try
            {
                var data = await loadAction.Invoke();

                setAction.Invoke(data);
            }
            catch (Exception)
            {
                setAction.Invoke(default(T));
            }
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}