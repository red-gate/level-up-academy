using System.IO;
using System.Reflection;

namespace BlueBridge.SeaQuollMonitor.Utilities
{
    public static class Constants
    {
        public static string ApplicationDataFolder;

        static Constants()
        {
            for (var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                 dir != null;
                 dir = Path.GetDirectoryName(dir))
            {
                var dataDir = Path.Combine(dir, "Data");
                if (Directory.Exists(dataDir))
                {
                    ApplicationDataFolder = dataDir;
                    break;
                }
            }
        }
    }
}
