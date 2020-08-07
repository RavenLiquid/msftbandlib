using System.Threading.Tasks;
using MSFTBandLib.Contracts.Command;
using MSFTBandLib.Contracts.Services;
using MSFTBandLib.Contracts.Types;

namespace MSFTBandLib.Contracts.Band
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
        Task<ICommandResponse> Command(CommandEnum Command);

        /// <summary>
        /// Run a command with arguments.
        /// </summary>
        /// <param name="Command">Command to run</param>
        /// <param name="arguments">Arguments</param>
        /// <returns>Task<CommandResponse></returns>
        Task<ICommandResponse> Command(CommandEnum Command, byte[] arguments, byte[] data = null);

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
        /// <summary>
        /// Band personalization service
        /// </summary>
        IPersonalizationService Personalization { get; }

        /// <summary>
        /// Band Device Information Service
        /// </summary>
        IDeviceInfoService DeviceInfo { get; }

        /// <summary>
        /// Sleep information service
        /// </summary>
        ISleepService Sleep { get; }
    }
}