using MSFTBandLib.Contracts.Command;
using MSFTBandLib.Contracts.Types;
using MSFTBandLib.Includes;

namespace MSFTBandLib.Command
{
    /// <summary>
    /// Command packet class
    /// </summary>
    public class CommandPacket : ICommandPacket
    {

        /// <summary>
        /// Command
        /// </summary>
        protected CommandEnum Command;

        /// <summary>
        /// Arguments
        /// </summary>
        protected byte[] Arguments;

        /// <summary>
        /// Data
        /// </summary>
        protected byte[] Data;

        /// <summary>
        /// Create new command packet.
        /// 
        /// When no arguments are given, uses the command's default arguments.
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="arguments">Arguments</param>
        /// <param name="data">Data</param>
        public CommandPacket(CommandEnum command, byte[] arguments = null, byte[] data = null)
        {

            // Command
            Command = command;

            // Use default for command when no arguments given
            Arguments = arguments ?? GetCommandDefaultArgumentsBytes();

            Data = data;
        }

        /// <summary>
        /// Get the expected data/response size for the command.
        /// </summary>
        /// <returns>int</returns>
        public int GetCommandDataSize()
        {
            return CommandHelper.GetCommandDataSize(Command);
        }

        /// <summary>
        /// Get array of bytes defining size of the arguments to send.
        /// 
        /// TODO: Why is `8` required as base?
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] GetArgsSizeBytes() => new[] {(byte) (8 + Arguments.Length)};

        /// <summary>
        /// Get the array of bytes to use as the default arguments for 
        /// the command when no arguments are given.
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] GetCommandDefaultArgumentsBytes() => CommandHelper.GetCommandDefaultArgumentsBytes(Command);

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
        public byte[] GetBytes()
        {
            var bytes = new ByteStream();
            bytes.BinaryWriter.Write(GetArgsSizeBytes());
            bytes.BinaryWriter.Write((ushort) 12025);
            bytes.BinaryWriter.Write((ushort) Command);
            bytes.BinaryWriter.Write(GetCommandDataSize());
            bytes.BinaryWriter.Write(Arguments);

            // Send Data if there is any
            if (Data != null)
                bytes.BinaryWriter.Write(Data);
            return bytes.GetBytes();
        }
    }
}