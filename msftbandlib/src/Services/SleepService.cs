using System.Threading.Tasks;
using MSFTBandLib.Command;
using MSFTBandLib.Contracts.Band;
using MSFTBandLib.Contracts.Metrics;
using MSFTBandLib.Contracts.Types;
using MSFTBandLib.Metrics;
using MSFTBandLib.Services.Base;
using MSFTBandLib.Contracts.Services;

namespace MSFTBandLib.Services
{
    public class SleepService<T> : ServiceBase<T>, ISleepService where T : class, IBandSocketInterface
    {
        public SleepService(BandConnection<T> connection) : base(connection)
        {
   
        }

        /// <summary>
        /// Get last sleep.
        /// </summary>
        /// <returns>Task<Sleep></returns>
        public async Task<ISleep> GetLastSleep()
        {
            var res = await Command(CommandEnum.GetStatisticsSleep);
            return new Sleep(res);
        }
    }
}