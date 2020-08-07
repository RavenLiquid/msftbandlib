using MSFTBandLib.Facility;
using MSFTBandLib.Includes;
using System.Reflection;
using MSFTBandLib.Contracts.Types;

namespace MSFTBandLib.Command
{
    /// <summary>
    /// Command helper methods
    /// </summary>
    public static class CommandHelper
    {
        /// <summary>
        /// Create a new command given a Band facility.
        /// 
        /// You must specify the facility index and whether it is a TX bit.
        /// 
        /// This method uses bitwise operations to convert the facility/index 
        /// integers into a `ushort` which is returned as the command.
        /// 
        /// Reminders:
        ///  - `<<` shifts the left operand's value left by 
        /// 	the number of bits specified by the right operand.
        ///  - `|` copies a bit if it exists in either of its operands.
        ///  
        /// Returns a `ushort` for use as the command.
        /// </summary>
        /// <param name="facility">Facility</param>
        /// <param name="tx">TX bit</param>
        /// <param name="index">Index</param>
        /// <returns>ushort</returns>
        public static ushort Create(FacilityEnum facility, bool tx, int index) =>
            (ushort) ((int) facility << 8 | (tx ? 1 : 0) << 7 | index);

        /// <summary>Get the data size associated with a command.</summary>
        /// <param name="command">Command</param>
        /// <returns>int</returns>
        public static int GetCommandDataSize(CommandEnum command)
        {
            var fi = typeof(CommandEnum).GetRuntimeField(command.ToString());
            var attr = fi.GetCustomAttribute(typeof(CommandDataSize));
            return ((CommandDataSize) attr)?.DataSize ?? 0;
        }

        /// <summary>
        /// Get an array of bytes to use as the default arguments for a 
        /// command when no arguments are given; we have to specify the 
        /// expected data/response size as the arguments instead.
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>byte[]</returns>
        public static byte[] GetCommandDefaultArgumentsBytes(CommandEnum command)
        {
            var bytes = new ByteStream();
            bytes.BinaryWriter.Write(GetCommandDataSize(command));
            return bytes.GetBytes();
        }
    }
}