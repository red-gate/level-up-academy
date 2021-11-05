using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace BlueBridge.SeaQuollMonitor.Domain
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IReadOnlyDictionary<string, string> _settings;

        public SettingsRepository(string settingsJsonPath)
        {
            BaseDirectory = Path.GetDirectoryName(settingsJsonPath) ?? Environment.CurrentDirectory;

            var json = File.ReadAllText(settingsJsonPath, Encoding.UTF8);
            _settings = new SortedDictionary<string, string>(
                JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                    ?? throw new ArgumentException("Failed to load settings"),
                StringComparer.OrdinalIgnoreCase);
        }

        public string BaseDirectory { get; }

        public string GetValue(string name) => _settings[name];
    }
}
