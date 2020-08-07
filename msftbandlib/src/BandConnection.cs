using MSFTBandLib.Command;
using MSFTBandLib.Exceptions;
using System;
using System.Threading.Tasks;
using MSFTBandLib.Contracts.Band;
using MSFTBandLib.Contracts.Command;
using MSFTBandLib.Contracts.Constants;
using MSFTBandLib.Contracts.Types;

namespace MSFTBandLib
{
    /// <summary>
    /// Microsoft Band connection class
    /// </summary>
    public class BandConnection<T> : IBandConnectionInterface, IDisposable
        where T : class, IBandSocketInterface
    {
        /// <summary>
        /// Band instance
        /// </summary>
        protected IBandInterface Band;

        /// <summary>
        /// Currently connected
        /// </summary>
        public bool Connected { get; protected set; }

        ///	<summary>
        /// Disposed
        /// </summary>
        public bool Disposed { get; protected set; }

        /// <summary>
        /// Band main service socket
        /// </summary>
        protected readonly IBandSocketInterface Cargo;

        /// <summary>
        /// Band push service socket
        /// </summary>
        protected readonly IBandSocketInterface Push;

        /// <summary>
        /// Create a new connection instance.
        /// 
        /// Socket instances are created for Cargo and Push using the 
        /// given socket type for the connection type, which must 
        /// implement `BandSocket`.
        /// </summary>
        /// <param name="Band">Band to connect to</param>
        public BandConnection()
        {
            Cargo = Activator.CreateInstance(
                typeof(T), new object[] { }
            ) as T;
            Push = Activator.CreateInstance(
                typeof(T), new object[] { }
            ) as T;
        }

        /// <summary>
        /// Create a new connection instance with a given Band.
        /// </summary>
        /// <param name="band">Band to connect to</param>
        public BandConnection(IBandInterface band) : this()
        {
            Band = band;
        }

        /// <summary>
        /// Dispose of the connection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of the connection.
        /// </summary>
        /// <param name="disposing">Disposing (not used)</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;

            Cargo.Dispose();
            Push.Dispose();
            Disposed = true;
        }

        /// <summary>
        /// Connect to the currently active Band instance.
        /// 
        /// Throws if already connected.
        /// </summary>
        /// <returns>Task</returns>
        /// <exception cref="BandConnectionConnected"></exception>
        public async Task Connect()
        {
            if (!Connected)
            {
                Connected = true;
                var mac = Band.DeviceInfo.GetMac();
                await Cargo.Connect(mac, Guid.Parse(BluetoothServices.CARGO));
                await Push.Connect(mac, Guid.Parse(BluetoothServices.PUSH));
            }
            else throw new BandConnectionConnected();
        }

        /// <summary>
        /// Connect to a given Band, replacing any existing Band instance.
        /// 
        /// Throws if already connected.
        /// </summary>
        /// <param name="band">Band to connect to</param>
        /// <returns>Task</returns>
        /// <exception cref="BandConnectionConnected"></exception>
        public async Task Connect(IBandInterface band)
        {
            if (!Connected)
            {
                Band = band;
                await Connect();
            }
            else throw new BandConnectionConnected();
        }

        /// <summary>
        /// Disconnect all open Band sockets.
        /// 
        /// Throws if not connected.
        /// </summary>
        /// <returns>Task</returns>
        /// <exception cref="BandConnectionConnectedNot"></exception>
        public async Task Disconnect()
        {
            if (Connected)
            {
                await Cargo.Disconnect();
                await Push.Disconnect();
                Connected = false;
            }
            else throw new BandConnectionConnectedNot();
        }

        /// <summary>
        /// Send command to the device and get a response.
        /// 
        /// Throws if not connected.
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="arguments">Arguments to send</param>
        /// <param name="data">Data</param>
        /// <param name="buffer">Receiving buffer size</param>
        /// <returns>Task<ICommandResponse></returns>
        /// <exception cref="BandConnectionConnectedNot"></exception>
        public async Task<ICommandResponse> Command(
            CommandEnum command,
            byte[] arguments = null, byte[] data = null, uint buffer = Network.BUFFER_SIZE)
        {
            if (!Connected) throw new BandConnectionConnectedNot();
            var packet = new CommandPacket(command, arguments, data);
            return await Cargo.Request(packet, buffer);
        }
    }
}