using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Utilities;
using RedGate.Licensing.Permits.Core;
using RedGate.Licensing.Permits.Core.Exceptions;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class LicenseService : DisposableBase, ILicenseService
    {
        private readonly IFeaturesClient _featuresClient;
        private readonly SemaphoreLock _reportUsageLock = new();
        private int? _mostRecentlyReportedUsageCount;

        public event Action? OnAvailableLicensesChanged;

        public LicenseService(IFeaturesClientFactory featuresClientFactory)
        {
            _featuresClient = featuresClientFactory.Create(OnFeaturesChanged);
        }

        private void OnFeaturesChanged(IReadOnlyCollection<Feature> features) => OnAvailableLicensesChanged?.Invoke();

        public async Task<int> GetAvailableLicenseCount()
        {
            try
            {
                var features = (await _featuresClient.GetFeaturesAsync(true))?.ToArray() ?? Array.Empty<Feature>();
                var feature = features.FirstOrDefault(f => f.ProductCode == ProductCodes.SqlMonitor && f.Name == "servers");

                return feature?.Limit ?? 0;
            }
            catch (FeaturesClientException)
            {
                return 0;
            }
        }

        public Task ReportUsedLicenseCount(int count) =>
            _reportUsageLock.ExecuteAsync(async () =>
            {
                if (count != _mostRecentlyReportedUsageCount)
                {
                    try
                    {
                        await _featuresClient.SetFeatureUsageAsync(
                            new[]
                            {
                                new FeatureUsage(
                                    ProductCodes.SqlMonitor,
                                    "servers",
                                    count)
                            });

                        _mostRecentlyReportedUsageCount = count;
                    }
                    catch (FeaturesClientException)
                    {
                    }
                }
            });

        public Task<Uri> GetLicenseManagementUri() =>
            _featuresClient.GetManagementPageAsync(
                ProductCodes.SqlMonitor,
                12,
                false,
                new Uri("https://localhost/seaquollmonitor/"));

        protected override void OnDispose()
        {
            _featuresClient.Dispose();
            _reportUsageLock.Dispose();
        }
    }
}
