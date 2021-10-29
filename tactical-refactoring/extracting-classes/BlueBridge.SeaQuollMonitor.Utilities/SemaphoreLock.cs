using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlueBridge.SeaQuollMonitor.Utilities
{
    /// <summary>
    /// Convenience wrapper around a semaphore to be used like a monitor lock object, but it's async-friendly.
    /// </summary>
    public sealed class SemaphoreLock : DisposableBase
    {
        private readonly SemaphoreSlim _semaphore = new (initialCount: 1);

        public void Execute(Action action)
        {
            _semaphore.Wait();
            try
            {
                action();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            await _semaphore.WaitAsync();
            try
            {
                await action();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public T Execute<T>(Func<T> func)
        {
            _semaphore.Wait();
            try
            {
                return func();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> func)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await func();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        protected override void OnDispose() => _semaphore.Dispose();
    }
}
