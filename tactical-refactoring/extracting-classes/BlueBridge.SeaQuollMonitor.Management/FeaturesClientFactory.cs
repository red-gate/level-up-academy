using System;
using System.Collections.Generic;
using System.IO;
using BlueBridge.SeaQuollMonitor.Utilities;
using RedGate.Licensing.Permits.Core;
using RedGate.Licensing.Permits.Core.Logging;
using RedGate.Licensing.Permits.Windows;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class FeaturesClientFactory : IFeaturesClientFactory
    {
        public IFeaturesClient Create(Action<IReadOnlyCollection<Feature>> onFeaturesChanged)
        {
            var persistenceDir = Path.Combine(Constants.ApplicationDataFolder, "Permits");

            // the features client (via .WithWindowsDefaults -> .WithEnvironmentVariableServiceLocation)
            // checks the REDGATEPERMITS environment variable to see where to send permits requests.
            // If we just set that environment variable for our own process, then we don't need to worry
            // about setting it on the machine or user profile.
            Environment.SetEnvironmentVariable(
                "REDGATEPERMITS",
                "https://permits.coredev-uat-1.testnet.red-gate.com",
                EnvironmentVariableTarget.Process);

            return new FeaturesClientBuilder(new FeaturesClientLog())
                .WithWindowsDefaults()
                .WithPolling(onFeaturesChanged)
                .WithDirectoryBasedPersistence(persistenceDir)
                .Build();
        }

        private class FeaturesClientLog : ILog
        {
            public void Log(LogLevel level, string message, Exception exception)
            {
            }
        }
    }
}
