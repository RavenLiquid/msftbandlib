using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSFTBandLib.Contracts.Metrics;

namespace MSFTBandLib.Contracts.Services
{
    /// <summary>
    /// Sleep service Interface
    /// </summary>
    public interface ISleepService
    {
        /// <summary>
        /// Get last sleep.
        /// </summary>
        /// <returns>Task<Sleep></returns>
        Task<ISleep> GetLastSleep();
    }
}
