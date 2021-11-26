using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Utilities;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class LicenseService : DisposableBase, ILicenseService
    {
        private record LicenseInfo(int Available, int Used);

        private static readonly JsonSerializerOptions JsonSerializerOptions = new ()
        {
            WriteIndented = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static readonly string
            LicenseFilePath = Path.Combine(Constants.ApplicationDataFolder, "licenses.json");

        private readonly SemaphoreLock _lock = new();
        private readonly FileWatcher _watcher;
        private int? _mostRecentLicensesAvailable;

        public event Action? OnAvailableLicensesChanged;

        public LicenseService()
        {
            _watcher = new FileWatcher(LicenseFilePath);
            _watcher.OnChanged += OnFileChanged;
        }

        public async Task<int> GetAvailableLicenseCount() =>
            await _lock.ExecuteAsync(async () => (await ReadLicenseInfoAsync()).Available);

        public Task ReportUsedLicenseCount(int count) =>
            _lock.ExecuteAsync(async () =>
            {
                var oldLicenseInfo = await ReadLicenseInfoAsync();
                if (count != oldLicenseInfo.Used)
                {
                    await WriteLicenseInfo(new LicenseInfo(oldLicenseInfo.Available, count));
                }
            });

        private void OnFileChanged()
        {
            var shouldFireEvent = false;
            _lock.Execute(() =>
            {
                var licenseInfo = ReadLicenseInfo();
                if (_mostRecentLicensesAvailable != licenseInfo.Available)
                {
                    shouldFireEvent = true;
                    _mostRecentLicensesAvailable = licenseInfo.Available;
                }
            });
            if (shouldFireEvent)
            {
                OnAvailableLicensesChanged?.Invoke();
            }
        }

        private LicenseInfo ReadLicenseInfo()
        {
            try
            {
                var json = File.ReadAllText(LicenseFilePath, Encoding.UTF8);
                var licenseInfo = JsonSerializer.Deserialize<LicenseInfo>(json, JsonSerializerOptions);
                if (licenseInfo != null)
                {
                    return licenseInfo;
                }
            }
            catch
            {
                // Meh!
            }

            return new LicenseInfo(0, 0);
        }

        private async Task<LicenseInfo> ReadLicenseInfoAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(LicenseFilePath, Encoding.UTF8);
                var licenseInfo = JsonSerializer.Deserialize<LicenseInfo>(json, JsonSerializerOptions);
                if (licenseInfo != null)
                {
                    return licenseInfo;
                }
            }
            catch
            {
                // Meh!
            }

            return new LicenseInfo(0, 0);
        }

        private async Task WriteLicenseInfo(LicenseInfo licenseInfo)
        {
            try
            {
                var json = JsonSerializer.Serialize(licenseInfo, JsonSerializerOptions);
                await File.WriteAllTextAsync(LicenseFilePath, json, Encoding.UTF8);
            }
            catch
            {
                // Meh!
            }
        }

        protected override void OnDispose()
        {
            _watcher.Dispose();
            _lock.Dispose();
        }
    }
}
