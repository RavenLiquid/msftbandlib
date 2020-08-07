using System;

namespace MSFTBandLib.Command
{
    /// <summary>
    /// Command data size attribute
    /// </summary>
    public class CommandDataSize : Attribute
    {
        /// <summary>
        /// Assigned data size
        /// </summary>
        public int DataSize { get; protected set; }

        /// <summary>
        /// Command data size constructor.
        /// </summary>
        /// <param name="dataSize">Assigned data size</param>
        public CommandDataSize(int dataSize)
        {
            DataSize = dataSize;
        }
    }
}