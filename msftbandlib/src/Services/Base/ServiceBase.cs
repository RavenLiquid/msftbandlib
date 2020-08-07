using System;
using System.Threading.Tasks;
using MSFTBandLib.Command;
using MSFTBandLib.Contracts.Band;
using MSFTBandLib.Contracts.Command;
using MSFTBandLib.Contracts.Types;
using MSFTBandLib.Exceptions;

namespace MSFTBandLib.Services.Base
{
    /// <summary>
    /// Base class for services
    /// </summary>
    public abstract class ServiceBase<T> where T : class, IBandSocketInterface
    {
        protected ServiceBase(BandConnection<T> connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Band Bluetooth connection
        /// </summary>
        protected BandConnection<T> Connection { get; }

        /// <summary>
        /// Get currently connected
        /// </summary>
        protected bool Connected
        {
            get => Connection.Connected;
            set => throw new Exception("Can't change connection directly!");
        }

        /// <summary>
        /// Run a command using the Band's `BandConnection`.
        /// </summary>
        /// <param name="command">Command to run</param>
        /// <returns>Task<CommandResponse></returns>
        /// <exception cref="BandConnectedNot">Band not connected.</exception>
        protected async Task<ICommandResponse> Command(CommandEnum command)
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
        protected async Task<ICommandResponse> Command(CommandEnum command, byte[] arguments, byte[] data = null)
        {
            if (!Connected) throw new BandConnectedNot();

            return await Connection.Command(command, arguments, data);
        }
    }
}
