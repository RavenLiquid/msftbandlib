using System;
using System.Threading.Tasks;
using MSFTBandLib.Contracts.Band;
using MSFTBandLib.Contracts.Services;
using MSFTBandLib.Contracts.Types;
using MSFTBandLib.Helpers;
using MSFTBandLib.Services.Base;

namespace MSFTBandLib.Services
{
    /// <summary>
    /// Device Information
    /// </summary>
    public class DeviceInfoService<T> : ServiceBase<T>, IDeviceInfoService where T: class, IBandSocketInterface
    {
        /// <summary>
        /// MAC address
        /// </summary>
        public string Mac { get; protected set; }

        ///	<summary>
        /// Bluetooth name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Device Information Service
        /// </summary>
        /// <param name="mac">Mac Address</param>
        /// <param name="name">Device name</param>
        /// <param name="connection"></param>
        public DeviceInfoService(string mac, string name, BandConnection<T> connection) : base(connection)
        {
            Mac = mac;
            Name = name;
        }

        /// <summary>
        /// Get MAC address.
        /// </summary>
        /// <returns>string</returns>
        public string GetMac() => Mac;

        /// <summary>
        /// Get Bluetooth name.
        /// </summary>
        /// <returns>string</returns>
        public string GetName() => Name;
        

        /// <summary>
        /// Get serial number from the Band.
        /// </summary>
        /// <returns>Task<String></returns>
        public async Task<string> GetSerialNumber()
        {
            var res = await Command(CommandEnum.GetSerialNumber);
            return res.GetByteStream().GetString();
        }

        /// <summary>
        /// Get the current device time.
        /// </summary>
        /// <returns>Task<DateTime></returns>
        public async Task<DateTime> GetDeviceTime()
        {
            var res = await Command(CommandEnum.GetDeviceTime);
            return TimeHelper.DateTimeResponse(res);
        }
    }
}
