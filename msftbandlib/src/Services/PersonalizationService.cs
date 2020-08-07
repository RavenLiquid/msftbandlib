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

        public async Task<bool> SetMeTileBackground(byte[] data, uint imageId)
        {
            // TODO figure out how to get image
            // Set device in start strip sync mode
            var syncStartResult = await Command(CommandEnum.StartStripSyncStart);

            // Send theme to device
            // TODO make image id byte[] (4 bytes uint?)
            var setThemeResult = await Command(CommandEnum.SetMeTileImage, BitConverter.GetBytes(imageId), data);

            // Complete start strip sync mode
            var completeSyncResult = await Command(CommandEnum.StartStripSyncEnd);

            return await Task.FromResult(true);
        }

        public async Task<uint> GetMeTileImageId()
        {
            var result = await Command(CommandEnum.GetMeTileImageId);

            return result.GetByteStream().GetUint32();
        }

        /*         
         def get_me_tile_image_id(self):
        result, data = self.cargo.cargo_read(GET_ME_TILE_IMAGE_ID, 4)
        if data:
            return data[0]
        return 0

    def get_me_tile_image(self):
        """
        Sends READ_ME_TILE_IMAGE command to device and returns a bgr565
        byte array with Me tile image
        """
        # calculate byte count based on device type
        if self.band_type == BandType.Cargo:
            byte_count = 310 * 102 * 2
        elif self.band_type == BandType.Envoy:
            byte_count = 310 * 128 * 2
        else:
            byte_count = 0

        # read Me Tile image
        result, data = self.cargo.cargo_read(READ_ME_TILE_IMAGE, byte_count)
        pixel_data = b''.join(data)
        return pixel_data

    def set_me_tile_image(self, pixel_data, image_id):
        result, data = self.cargo.cargo_write_with_data(
            WRITE_ME_TILE_IMAGE_WITH_ID,
            pixel_data,
            struct.pack("<I", image_id))
        return result, data
         */
    }
}
