namespace MSFTBandLib.Contracts.Command
{
    /// <summary>
    /// Command packet interface
    /// </summary>
    public interface ICommandPacket
    {
        /// <summary>
        /// Get the expected data/response size for the command.
        /// </summary>
        /// <returns>int</returns>
        int GetCommandDataSize();

        /// <summary>
        /// Get array of bytes defining size of the arguments to send.
        /// 
        /// TODO: Why is `8` required as base?
        /// </summary>
        /// <returns>byte[]</returns>
        byte[] GetArgsSizeBytes();

        /// <summary>
        /// Get the array of bytes to use as the default arguments for 
        /// the command when no arguments are given.
        /// </summary>
        /// <returns>byte[]</returns>
        byte[] GetCommandDefaultArgumentsBytes();

        /// <summary>
        /// Get command packet bytes to send.
        /// 
        /// Packet is structured as:
        /// 	- Expected data/response size bytes
        /// 	- `12025` `ushort` constant (TODO: what is this for?)
        /// 	- Command `ushort`
        /// 	- Expected data/response size as integer
        /// 	- Arguments (when none given explicitly, is data size again)
        /// 	
        /// Returns the array of bytes to send to the Band.
        /// </summary>
        /// <returns>byte[]</returns>
        byte[] GetBytes();
    }
}