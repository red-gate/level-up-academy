using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;
using BlueBridge.SeaQuollMonitor.Utilities;

namespace BlueBridge.SeaQuollMonitor.Monitoring
{
    public class MonitoredServersRepository : DisposableBase, IMonitoredServersRepository
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new ()
        {
            WriteIndented = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        private static readonly IComparer<string> NameComparer = StringComparer.OrdinalIgnoreCase;

        private readonly SemaphoreLock _lock = new ();
        private readonly string _monitoredServersJsonPath;
        private readonly FileWatcher _watcher;

        public event Action? OnLicensingRequirementsChanged;

        public MonitoredServersRepository(ISettingsRepository settingsRepository)
        {
            _monitoredServersJsonPath = Path.Combine(
                settingsRepository.BaseDirectory,
                settingsRepository.GetValue("monitoredServersPath"));

            _watcher = new FileWatcher(_monitoredServersJsonPath);
            _watcher.OnChanged += OnFileChanged;
        }

        private void OnFileChanged() => OnLicensingRequirementsChanged?.Invoke();

        public Task<IEnumerable<Server>> GetAllServers() =>
            _lock.Execute(async () => (await LoadServers()).Values.Buffered());

        public Task<Server?> GetServer(string name) =>
            _lock.Execute(async () => (await LoadServers()).TryGetValue(name, out var server) ? server : null);

        public async Task CreateServer(Server server)
        {
            await _lock.Execute(async () =>
            {
                var servers = await LoadServers();
                servers[server.Name] = server;
                await SaveServers(servers.Values);
            });
            OnLicensingRequirementsChanged?.Invoke();
        }

        public async Task RemoveServer(string name)
        {
            await _lock.Execute(async () =>
            {
                var servers = await LoadServers();
                servers.Remove(name);
                await SaveServers(servers.Values);
            });
            OnLicensingRequirementsChanged?.Invoke();
        }

        public Task UpdateServer(Server server) =>
            _lock.Execute(async () =>
            {
                var servers = await LoadServers();
                servers[server.Name] = server;
                await SaveServers(servers.Values);
            });

        private async Task<IDictionary<string, Server>> LoadServers()
        {
            var servers = new SortedDictionary<string, Server>(NameComparer);

            if (File.Exists(_monitoredServersJsonPath))
            {
                var json = await File.ReadAllTextAsync(_monitoredServersJsonPath, Encoding.UTF8);
                var array = JsonSerializer.Deserialize<Server[]>(json, JsonSerializerOptions);

                if (array != null)
                {
                    foreach (var server in array)
                    {
                        servers[server.Name] = server;
                    }
                }
            }

            return servers;
        }

        private async Task SaveServers(IEnumerable<Server> servers)
        {
            var json = JsonSerializer.Serialize(servers.ToArray(), JsonSerializerOptions);
            await File.WriteAllTextAsync(_monitoredServersJsonPath, json, Encoding.UTF8);
        }

        protected override void OnDispose()
        {
            _watcher.OnChanged -= OnFileChanged;
            _watcher.Dispose();
            _lock.Dispose();
        }
    }
}
