using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BlueBridge.SeaQuollMonitor.Utilities
{
    public class FileWatcher : DisposableBase
    {
        private static readonly TimeSpan PollingInterval = TimeSpan.FromSeconds(0.5);

        private readonly string _filePath;
        private readonly Task _pollingTask;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private DateTime? _lastWriteTime;

        public event Action? OnChanged;

        public FileWatcher(string filePath)
        {
            _filePath = filePath;
            _cancellationTokenSource = new CancellationTokenSource();
            _pollingTask = PollFile(_cancellationTokenSource.Token);
        }

        private async Task PollFile(CancellationToken cancellationToken)
        {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (CheckLastWriteTime())
                    {
                        try
                        {
                            OnChanged?.Invoke();
                        }
                        catch
                        {
                        }
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    try
                    {
                        await Task.Delay(PollingInterval, cancellationToken);
                    }
                    catch (TaskCanceledException)
                    {
                    }
                }
        }

        private bool CheckLastWriteTime()
        {
            try
            {
                var lastWriteTime = File.GetLastWriteTimeUtc(_filePath);
                var hasChanged = _lastWriteTime.HasValue && _lastWriteTime.Value < lastWriteTime;
                _lastWriteTime = lastWriteTime;
                return hasChanged;
            }
            catch
            {
                return false;
            }
        }

        protected override void OnDispose()
        {
            _cancellationTokenSource.Cancel();
            _pollingTask.Wait(PollingInterval);
        }
    }
}
