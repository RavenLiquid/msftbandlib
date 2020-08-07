using MSFTBandLib.Command;
using MSFTBandLib.Metrics;
using System;
using System.Threading.Tasks;

namespace MSFTBandLib
{
    /// <summary>
    /// Band interface
    /// </summary>
    public interface IBandInterface
    {


        /// <summary>
        /// Connect to the Band.
        /// </summary>
        /// <returns>Task</returns>
        Task Connect();

        /// <summary>
        /// Disconnect from the Band.
        /// </summary>
        /// <returns>Task</returns>
        Task Disconnect();

        /// <summary>
        /// Run a command.
        /// </summary>
        /// <param name="Command">Command to run</param>
        /// <returns>Task<CommandResponse></returns>
        Task<CommandResponse> Command(CommandEnum Command);

        /// <summary>
        /// Run a command with arguments.
        /// </summary>
        /// <param name="Command">Command to run</param>
        /// <param name="arguments">Arguments</param>
        /// <returns>Task<CommandResponse></returns>
        Task<CommandResponse> Command(CommandEnum Command, byte[] arguments, byte[] data = null);

        /*/// <summary>
        /// Get the current device time.
        /// </summary>
        /// <returns>Task<DateTime></returns>
        Task<DateTime> GetDeviceTime();

        /// <summary>
        /// Get last sleep.
        /// </summary>
        /// <returns>Task<Sleep></returns>
        Task<Sleep> GetLastSleep();

        /// <summary>
        /// Get serial number from the Band.
        /// </summary>
        /// <returns>Task<string></returns>
        Task<string> GetSerialNumber();*/

        /*/// <summary>
        /// Get MAC address.
        /// </summary>
        /// <returns>string</returns>
        string GetMac();

        /// <summary>
        /// Get Bluetooth name.
        /// </summary>
        /// <returns>string</returns>
        string GetName();*/
    }
}