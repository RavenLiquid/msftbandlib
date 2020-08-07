using MSFTBandLib.Command;
using MSFTBandLib.Exceptions;
using MSFTBandLib.Helpers;
using MSFTBandLib.Metrics;
using System;
using System.Threading.Tasks;
using MSFTBandLib.Contracts.Band;
using MSFTBandLib.Contracts.Command;
using MSFTBandLib.Contracts.Services;
using MSFTBandLib.Contracts.Types;
using MSFTBandLib.Services;

namespace MSFTBandLib
{
    /// <summary>
    /// Microsoft Band device class
    /// 
    /// Most methods will throw `BandConnectedNot` from the `Command` 
    /// method when trying to access Bluetooth endpoints when not connected.
    /// </summary>
    public class Band<T> : IBandInterface
        where T : class, IBandSocketInterface
    {
        #region Services
        /// <summary>
        /// Band personalization service
        /// </summary>
        public IPersonalizationService Personalization { get; }

        /// <summary>
        /// Band Device Information Service
        /// </summary>
        public IDeviceInfoService DeviceInfo { get; }

        /// <summary>
        /// Sleep information service
        /// </summary>
        public ISleepService Sleep { get; }
        #endregion

        /// <summary>
        /// Get currently connected
        /// </summary>
        public bool Connected
        {
            get => Connection.Connected;
            set => throw new Exception("Can't change connection directly!");
        }

        /// <summary>
        /// Band Bluetooth connection
        /// </summary>
        public BandConnection<T> Connection { get; protected set; }

        /// <summary
        /// >Construct a new device instance.
        /// </summary>
        /// <param name="mac">MAC address</param>
        /// <param name="name">Bluetooth name</param>
        public Band(string mac, string name)
        {
            Connection = new BandConnection<T>(this);

            Sleep = new SleepService<T>(Connection);
            DeviceInfo = new DeviceInfoService<T>(mac, name, Connection);
            Personalization = new PersonalizationService<T>(Connection);
        }

        /// <summary>
        /// Connect to the Band.
        /// </summary>
        /// <returns>Task</returns>
        /// <exception cref="BandConnected">Band is connected.</exception>
        public async Task Connect()
        {
            if (!Connected)
            {
                await Connection.Connect();
            }
            else throw new BandConnected();
        }

        /// <summary>
        /// Disconnect from the Band.
        /// </summary>
        /// <returns>Task</returns>
        /// <exception cref="BandConnectedNot">Band not connected.</exception>
        public async Task Disconnect()
        {
            if (Connected)
            {
                await Connection.Disconnect();
            }
            else throw new BandConnectedNot();
        }

        /// <summary>
        /// Run a command using the Band's `BandConnection`.
        /// </summary>
        /// <param name="command">Command to run</param>
        /// <returns>Task<CommandResponse></returns>
        /// <exception cref="BandConnectedNot">Band not connected.</exception>
        public async Task<ICommandResponse> Command(CommandEnum command)
        {
            if (!Connected) throw new BandConnectedNot();
            
            return await Connection.Command(command);
        }

        /// <summary>
        /// Run a command using the Band's `BandConnection`.
        /// </summary>
        /// <param name="command">Command to run</param>
        /// <param name="arguments">Arguments</param>
        /// <param name="data">Data</param>
        /// <returns>Task<CommandResponse></returns>
        /// <exception cref="BandConnectedNot">Band not connected.</exception>
        public async Task<ICommandResponse> Command(CommandEnum command, byte[] arguments, byte[] data = null)
        {
            if (!Connected) throw new BandConnectedNot();
            
            return await Connection.Command(command, arguments, data);
        }
    }
}