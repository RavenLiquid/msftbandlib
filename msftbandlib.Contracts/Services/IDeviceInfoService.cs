using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSFTBandLib.Contracts.Types;

namespace MSFTBandLib.Contracts.Services
{
    public interface IDeviceInfoService
    {
        /// <summary>
        /// MAC address
        /// </summary>
        string Mac { get; }

        ///	<summary>
        /// Bluetooth name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get MAC address.
        /// </summary>
        /// <returns>string</returns>
        string GetMac();

        /// <summary>
        /// Get Bluetooth name.
        /// </summary>
        /// <returns>string</returns>
        string GetName();


        /// <summary>
        /// Get serial number from the Band.
        /// </summary>
        /// <returns>Task<String></returns>
        Task<string> GetSerialNumber();

        /// <summary>
        /// Get the current device time.
        /// </summary>
        /// <returns>Task<DateTime></returns>
        Task<DateTime> GetDeviceTime();
    }
}
