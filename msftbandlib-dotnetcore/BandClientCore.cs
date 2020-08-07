using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using MSFTBandLib.Contracts.Band;
using MSFTBandLib.Contracts.Constants;

namespace MSFTBandLib.DotNetCore
{
    /// <summary>
    /// MSFTBandLib UWP implementation
    /// </summary>
    public class BandClientCore : IBandClientInterface
    {
        /// <summary>
        ///	Get list of all available paired Bands.
        /// </summary>
        /// <returns>Task<List<Band>></returns>
        public async Task<List<IBandInterface>> GetPairedBands()
        {
            var bands = new List<IBandInterface>();

            // Get devices
            var cargo = RfcommServiceId.FromUuid(Guid.Parse(BluetoothServices.CARGO));
            var selector = RfcommDeviceService.GetDeviceSelector(cargo);
            var devices = await DeviceInformation.FindAllAsync(selector);

            // Create Band instances
            foreach (var device in devices)
            {
                var bt = await BluetoothDevice.FromIdAsync(device.Id);
                bands.Add(new Band<BandSocketCore>(bt.HostName.ToString(), bt.Name));
            }
            return bands;
        }
    }
}