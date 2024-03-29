using System;
using System.Threading.Tasks;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface ILicenseAllocator
    {
        Task Refresh();
        event Action OnLicencesAllocated;
    }
}
