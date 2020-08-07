namespace MSFTBandLib.Contracts.Includes
{
    public interface IByteStream
    {
        ///	<summary>
        /// Disposed
        /// </summary>
        bool Disposed { get; }

        /// <summary>
        /// Dispose of the resources.
        /// </summary>
        void Dispose();

        /// <summary
        /// >Write bytes.
        /// </summary>
        /// <param name="bytes">bytes</param>
        void Write(byte[] bytes);

        /// <summary>
        /// Get the current byte array.
        /// </summary>
        /// <returns>byte[]</returns>
        byte[] GetBytes();

        /// <summary>
        /// Read an 4-byte unsigned integer from the stream using 
        /// the `BinaryReader`. Sets stream to given position, and 
        /// advances stream position by 4 once done. Little-endian encoding.
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>uint</returns>
        uint GetUint32(int position = 0);

        /// <summary>
        /// Read an 8-byte unsigned integer from the stream using 
        /// the `BinaryReader`. Sets stream to given position, and 
        /// advances stream position by 8 once done. Little-endian encoding.
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>ulong</returns>
        ulong GetUint64(int position = 0);

        /// <summary>
        /// Read a string from the stream as characters from a given 
        /// position in the stream (position is set and will not be restored).
        /// 	
        /// Note: Implemented with `BinaryReader.ReadChars`, instead 
        /// of `BinaryReader.ReadString`, because the latter expects 
        /// strings to be prepended with their length.
        /// 
        /// This implementation works with any string-like data and 
        /// enables the consumer to specify how many characters to get.
        /// </summary>
        /// <param name="position">Position to read from</param>
        /// <param name="chars">Characters to read (0: all)</param>
        /// <returns>string</returns>
        string GetString(int position = 0, long chars = 0);

        /// <summary>
        /// Read a 2-byte unsigned integer from the stream using 
        /// the `BinaryReader`. Sets stream to given position, and 
        /// advances stream position by 2 once done. Little-endian encoding.
        /// </summary>
        /// <param name="position">Position to read from</position>
        /// <returns>ushort</returns>
        ushort GetUshort(int position = 0);

        /// <summary>
        /// Get an array of `ushort` from the stream using 
        /// the `BinaryReader`, reading sequentially from the 
        /// given start position. Does not verify the specified 
        /// number of `ushort` is available.
        /// </summary>
        /// <param name="count">Number of `ushort` to read</param>
        /// <param name="pos">Position to read from</param>
        /// <returns>ushort[]</returns>
        ushort[] GetUshorts(int count, int pos = 0);
    }
}