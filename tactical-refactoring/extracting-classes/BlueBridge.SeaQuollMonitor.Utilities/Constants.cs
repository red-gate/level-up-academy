using System;
using System.IO;

namespace BlueBridge.SeaQuollMonitor.Utilities
{
    public static class Constants
    {
        public static string ApplicationDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "Blue Bridge",
            "Sea Quoll Monitor");

    }
}
