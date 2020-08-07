using MSFTBandLib.Contracts.Band;
using MSFTBandLib.Contracts.Services;
using MSFTBandLib.Services.Base;

namespace MSFTBandLib.Services
{
    public class PersonalizationService<T> : ServiceBase<T>, IPersonalizationService where T : class, IBandSocketInterface
    {
        public PersonalizationService(BandConnection<T> connection) : base(connection)
        {

        }
    }
}
