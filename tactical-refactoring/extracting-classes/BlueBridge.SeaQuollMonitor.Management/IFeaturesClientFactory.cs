using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RedGate.Licensing.Permits.Core;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface IFeaturesClientFactory
    {
        /// <summary>
        /// Creates a new features client. Callers are responsible for disposing of the client.
        /// </summary>
        /// <param name="onFeaturesChanged">A callback "event handler" invoked when the available features changes.
        /// </param>
        IFeaturesClient Create(Action<IReadOnlyCollection<Feature>> onFeaturesChanged);
    }
}
