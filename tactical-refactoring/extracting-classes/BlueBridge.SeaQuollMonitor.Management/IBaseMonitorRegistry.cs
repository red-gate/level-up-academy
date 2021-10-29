using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface IBaseMonitorRegistry
    {
        event Action OnLicensingRequirementsChanged;
        void AddBaseMonitor(IBaseMonitor baseMonitor);
        Task<IDictionary<string, T>> ExecuteOnAllBaseMonitorsAsync<T>(Func<IBaseMonitor, Task<T>> func);
        Task ExecuteOnAllBaseMonitorsAsync(Func<IBaseMonitor, Task> action);
    }
}
