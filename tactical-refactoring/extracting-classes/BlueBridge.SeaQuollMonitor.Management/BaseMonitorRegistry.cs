using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;
using BlueBridge.SeaQuollMonitor.Utilities;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class BaseMonitorRegistry : DisposableBase, IBaseMonitorRegistry
    {
        private readonly SemaphoreLock _lock = new();
        private readonly IDictionary<string, IBaseMonitor> _baseMonitors =
            new SortedDictionary<string, IBaseMonitor>(StringComparer.OrdinalIgnoreCase);

        public event Action? OnLicensingRequirementsChanged;

        public void AddBaseMonitor(IBaseMonitor baseMonitor) =>
            _lock.Execute(() =>
            {
                baseMonitor.MonitoredServersRepository.OnLicensingRequirementsChanged +=
                    HandleOnLicensingRequirementsChanged;
                return _baseMonitors[baseMonitor.Name] = baseMonitor;
            });

        private void HandleOnLicensingRequirementsChanged() => OnLicensingRequirementsChanged?.Invoke();

        public Task ExecuteOnAllBaseMonitorsAsync(Func<IBaseMonitor, Task> action) =>
            _lock.ExecuteAsync(() => Task.WhenAll(_baseMonitors.Values.Select(action).ToArray()));

        public async Task<IDictionary<string, T>> ExecuteOnAllBaseMonitorsAsync<T>(Func<IBaseMonitor, Task<T>> func) =>
            await _lock.ExecuteAsync(async () =>
            {
                var results = new SortedDictionary<string, T>(StringComparer.OrdinalIgnoreCase);
                foreach (var baseMonitor in _baseMonitors.Values)
                {
                    results[baseMonitor.Name] = await func(baseMonitor);
                }

                return results;
            });

        protected override void OnDispose()
        {
            _lock.Execute(() =>
            {
                foreach (var baseMonitor in _baseMonitors.Values)
                {
                    baseMonitor.MonitoredServersRepository.OnLicensingRequirementsChanged -=
                        HandleOnLicensingRequirementsChanged;
                }
            });
            _lock.Dispose();
        }
    }
}
