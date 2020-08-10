using System;
using System.Threading.Tasks;
using MSFTBandLib.Contracts;
using MSFTBandLib.Contracts.Band;
using MSFTBandLib.Contracts.Services;
using MSFTBandLib.Contracts.Types;
using MSFTBandLib.Services.Base;

namespace MSFTBandLib.Services
{
    public class PersonalizationService<T> : ServiceBase<T>, IPersonalizationService where T : class, IBandSocketInterface
    {
        public PersonalizationService(BandConnection<T> connection) : base(connection)
        {

        }

        /// <summary>
        /// Sets the device theme colors to the given theme
        /// </summary>
        /// <param name="theme">Color theme</param>
        /// <returns></returns>
        public async Task<bool> SetTheme(ITheme theme)
        {
            // TODO update with status interpretation

            var colors = theme.GetColors();
            var bytes = new byte[colors.Length * sizeof(uint)];
            Buffer.BlockCopy(colors, 0, bytes, 0, bytes.Length);

            // Set device in start strip sync mode
            var syncStartResult = await Command(CommandEnum.StartStripSyncStart);

            // Send theme to device
            var setThemeResult = await Command(CommandEnum.SetThemeColor, null, bytes);

            // Complete start strip sync mode
            var completeSyncResult = await Command(CommandEnum.StartStripSyncEnd);

            return true;
        }      
        
        /// <summary>
        /// Sets a custom theme
        /// </summary>
        /// <param name="theme">Color theme</param>
        /// <param name="themeId">Id of the theme</param>
        /// <returns></returns>
        public async Task<bool> SetCustomTheme(ITheme theme, uint themeId)
        {
            // TODO update with status interpretation

            var colors = theme.GetColors();
            var bytes = new byte[colors.Length * sizeof(uint)];
            Buffer.BlockCopy(colors, 0, bytes, 0, bytes.Length);

            // Set device in start strip sync mode
            var syncStartResult = await Command(CommandEnum.StartStripSyncStart);

            // Send theme to device
            var setThemeResult = await Command(CommandEnum.SetCustomThemeColorByIndex, BitConverter.GetBytes(themeId), bytes);

            // Complete start strip sync mode
            var completeSyncResult = await Command(CommandEnum.StartStripSyncEnd);

            return true;
        }

        /// <summary>
        /// Does not currently work
        /// </summary>
        /// <remarks>WIP</remarks>
        /// <returns></returns>
        public async Task<ITheme> GetTheme()
        {
            // TODO update with status interpretation

            /*var colors = theme.GetColors();
            var bytes = new byte[colors.Length * sizeof(uint)];
            Buffer.BlockCopy(colors, 0, bytes, 0, bytes.Length);*/

            // Set device in start strip sync mode
            var syncStartResult = await Command(CommandEnum.StartStripSyncStart);

            // Send theme to device
            var setThemeResult = await Command(CommandEnum.GetThemeColor);

            // Complete start strip sync mode
            var completeSyncResult = await Command(CommandEnum.StartStripSyncEnd);

            return null;
        }

        /// <summary>
        /// Resets the theme back to factory default (STORM ?)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ResetTheme()
        {
            // TODO update with status interpretation

            // Set device in start strip sync mode
            var syncStartResult = await Command(CommandEnum.StartStripSyncStart);

            // Send theme to device
            var setThemeResult = await Command(CommandEnum.ResetThemeColor);

            // Complete start strip sync mode
            var completeSyncResult = await Command(CommandEnum.StartStripSyncEnd);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">A headerless BGR565 formatted image</param>
        /// <param name="imageId">Id of the image to store on device</param>
        /// <remarks>Currently I have no clue what the image ID actually does, as there seems to be no way to change the image without sending it</remarks>
        /// <returns></returns>
        public async Task<bool> SetMeTileBackground(byte[] data, uint imageId)
        {
            // TODO figure out how to get image
            // Set device in start strip sync mode
            var syncStartResult = await Command(CommandEnum.StartStripSyncStart);

            // Send the background to device
            var setThemeResult = await Command(CommandEnum.SetMeTileImage, BitConverter.GetBytes(imageId), data);

            // Complete start strip sync mode
            var completeSyncResult = await Command(CommandEnum.StartStripSyncEnd);

            return true;
        }

        /// <summary>
        /// Retries the id of the current Me Tile image
        /// </summary>
        /// <remarks>WIP</remarks>
        /// <returns></returns>
        public async Task<uint> GetMeTileImageId()
        {
            var result = await Command(CommandEnum.GetMeTileImageId);

            return result.GetByteStream().GetUint32();
        }

        /// <summary>
        /// Clear the Me tile background image
        /// </summary>
        /// <remarks>WIP</remarks>
        /// <returns></returns>
        public async Task<bool> ClearMeTileBackground()
        {
            // TODO update with status interpretation

            // Set device in start strip sync mode
            var syncStartResult = await Command(CommandEnum.StartStripSyncStart);

            // Clear background on device
            var setThemeResult = await Command(CommandEnum.ClearMeTileImage);

            // Complete start strip sync mode
            var completeSyncResult = await Command(CommandEnum.StartStripSyncEnd);

            return true;
        }
    }
}
