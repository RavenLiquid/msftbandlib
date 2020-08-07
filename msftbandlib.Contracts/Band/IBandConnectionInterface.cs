using System;
using System.Threading.Tasks;
using MSFTBandLib.Contracts.Command;
using MSFTBandLib.Contracts.Constants;
using MSFTBandLib.Contracts.Types;

namespace MSFTBandLib.Contracts.Band
{
    /// <summary>
    /// Microsoft Band connection interface
    /// </summary>
    public interface IBandConnectionInterface : IDisposable
    {
        /// <summary>
        /// Create a new connection to a given Band.
        /// </summary>
        /// <param name="band">Band to connect to</param>
        /// <returns>Task</returns>
        Task Connect(IBandInterface band);

        /// <summary>
        /// Disconnect from the Band if connected.
        /// </summary>
        Task Disconnect();

        /// <summary>
        /// Send command to the device and get response bytes.
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="arguments">Arguments to send</param>
        /// <param name="data">Data to send</param>
        /// <param name="buffer">Receiving buffer size</param>
        /// <returns>Task<CommandResponse></returns>
        Task<ICommandResponse> Command(
            CommandEnum command,
            byte[] arguments = null, byte[] data = null, uint buffer = Network.BUFFER_SIZE
        );
    }
}