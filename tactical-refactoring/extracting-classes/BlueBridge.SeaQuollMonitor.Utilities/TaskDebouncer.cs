using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlueBridge.SeaQuollMonitor.Utilities
{
    /// <summary>
    /// <para>
    /// Utility class that wraps a <c>Func&lt;Task&gt;</c> with two guarantees:
    /// </para>
    /// <para>
    /// 1. Repeated overlapping execution of the func will not occur, because if a call to <see cref="Execute"/> is
    /// received whilst it's already executing, the new execution will be queued until the current one completes.
    /// </para>
    /// <para>
    /// 2. If a call to <see cref="Execute"/> is pending the completion of a current execution, and one or more
    /// subsequent calls to <see cref="Execute"/> are additionally received, they will all be merged into a single
    /// pending execution.
    /// </para>
    /// </summary>
    public class TaskDebouncer : DisposableBase
    {
        private readonly object _taskLock = new();
        private Task? _pendingTask;
        private Task? _currentTask;
        private readonly Func<Task> _func;

        private readonly SemaphoreSlim _semaphore = new (initialCount: 1);

        public TaskDebouncer(Func<Task> func)
        {
            _func = func;
        }

        public Task Execute()
        {
            lock (_taskLock)
            {
                if (_currentTask == null)
                {
                    _currentTask = DoWork();
                    return _currentTask;
                }

                if (_pendingTask == null)
                {
                    _pendingTask = DoWork();
                }

                return _pendingTask;
            }
        }

        private async Task DoWork()
        {
            await _semaphore.WaitAsync();

            try
            {
                await _func();
            }
            finally
            {
                lock (_taskLock)
                {
                    _currentTask = _pendingTask;
                    _pendingTask = null;
                    _semaphore.Release();
                }
            }
        }

        protected override void OnDispose() => _semaphore.Dispose();
    }
}
