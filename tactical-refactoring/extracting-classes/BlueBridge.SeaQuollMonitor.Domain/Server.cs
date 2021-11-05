using System;

namespace BlueBridge.SeaQuollMonitor.Domain
{
    public record Server(string Name, DateTime Added, bool IsSuspended, bool IsLicensed);
}
