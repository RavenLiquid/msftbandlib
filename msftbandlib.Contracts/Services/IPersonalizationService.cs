using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSFTBandLib.Contracts.Constants;

namespace MSFTBandLib.Contracts.Services
{
    public interface IPersonalizationService
    {
        Task<bool> SetTheme(ITheme theme);
        Task<bool> SetMeTileBackground(byte[] data, uint imageId);
        Task<uint> GetMeTileImageId();
    }
}
