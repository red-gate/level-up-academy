using System;
using System.Threading;

namespace BlueBridge.SeaQuollMonitor.Utilities
{
    public abstract class DisposableBase : IDisposable
    {
        private int _disposalCount;

        public void Dispose()
        {
            if (Interlocked.Increment(ref _disposalCount) == 1)
            {
                OnDispose();
                GC.SuppressFinalize(this);
            }
        }

        ~DisposableBase()
        {
            if (Interlocked.Increment(ref _disposalCount) == 1)
            {
                OnDispose();
            }
        }

        protected abstract void OnDispose();
    }
}
