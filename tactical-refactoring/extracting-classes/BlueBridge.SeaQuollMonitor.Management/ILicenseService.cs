using System;
using System.Threading.Tasks;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface ILicenseService
    {
        event Action OnAvailableLicensesChanged;
        Task<int> GetAvailableLicenseCount();
        Task ReportUsedLicenseCount(int count);
    }
}
