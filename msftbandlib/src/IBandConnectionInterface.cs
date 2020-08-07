using MSFTBandLib.Command;
using MSFTBandLib.Libs;
using System;
using System.Threading.Tasks;

namespace MSFTBandLib
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
        Task<CommandResponse> Command(
            CommandEnum command,
            byte[] arguments = null, byte[] data = null, uint buffer = Network.BUFFER_SIZE
        );
    }
}