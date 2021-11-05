using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;
using BlueBridge.SeaQuollMonitor.Utilities;

namespace BlueBridge.SeaQuollMonitor.Monitoring
{
    public class ErrorHandlingMonitoredServersRepository : DisposableBase, IMonitoredServersRepository
    {
        private readonly IMonitoredServersRepository _proxy;
        private readonly SemaphoreLock _lock = new();
        private IDictionary<string, Server> _cache =
            new SortedDictionary<string, Server>(StringComparer.OrdinalIgnoreCase);

        public event Action? OnLicensingRequirementsChanged;

        public ErrorHandlingMonitoredServersRepository(IMonitoredServersRepository proxy)
        {
            _proxy = proxy;
            proxy.OnLicensingRequirementsChanged += HandleOnLicensingRequirementsChanged;
        }

        private void HandleOnLicensingRequirementsChanged() => OnLicensingRequirementsChanged?.Invoke();

        private async Task<IDictionary<string, Server>> RefreshCache()
        {
            try
            {
                var servers = await _proxy.GetAllServers();
                _cache = servers.ToSortedDictionary(
                    x => x.Name,
                    x => x,
                    StringComparer.OrdinalIgnoreCase);
            }
            catch
            {
                // Ignore any error.
            }

            return _cache;
        }


        public Task<IEnumerable<Server>> GetAllServers() =>
            _lock.ExecuteAsync(async () => (await RefreshCache()).Values.Buffered());

        public Task<Server?> GetServer(string name) =>
            _lock.ExecuteAsync(async () => (await RefreshCache()).TryGetValue(name, out var server) ? server : null);

        public Task CreateServer(Server server) =>
            _lock.ExecuteAsync(async () =>
            {
                try
                {
                    await _proxy.CreateServer(server);
                    await RefreshCache();
                }
                catch
                {
                    // Ignore any error.
                }
            });

        public Task RemoveServer(string name) =>
            _lock.ExecuteAsync(async () =>
            {
                try
                {
                    await _proxy.RemoveServer(name);
                    await RefreshCache();
                }
                catch
                {
                    // Ignore any error.
                }
            });

        public Task UpdateServer(Server server) =>
            _lock.ExecuteAsync(async () =>
            {
                try
                {
                    await _proxy.UpdateServer(server);
                    await RefreshCache();
                }
                catch
                {
                    // Ignore any error.
                }
            });

        protected override void OnDispose()
        {
            _proxy.OnLicensingRequirementsChanged -= HandleOnLicensingRequirementsChanged;
            _lock.Dispose();
        }
    }
}
