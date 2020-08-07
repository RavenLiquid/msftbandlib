using System;
using System.IO;
using MSFTBandLib.Contracts.Includes;

namespace MSFTBandLib.Includes
{
    /// <summary>
    /// Byte stream class
    /// 
    /// This is a thin wrapper around `MemoryStream`, `BinaryReader` 
    /// and `BinaryWriter` to simplify writing/reading byte arrays.
    /// </summary>
    public class ByteStream : IDisposable, IByteStream
    {
        ///	<summary>
        /// Disposed
        /// </summary>
        public bool Disposed { get; protected set; }

        /// <summary>
        /// Memory stream
        /// </summary>
        public MemoryStream MemoryStream;

        /// <summary>
        /// Binary reader
        /// </summary>
        public BinaryReader BinaryReader;

        /// <summary>
        /// Binary writer
        /// </summary>
        public BinaryWriter BinaryWriter;

        /// <summary>
        /// Construct.
        /// </summary>
        public ByteStream()
        {
            MemoryStream = new MemoryStream();
            BinaryReader = new BinaryReader(MemoryStream);
            BinaryWriter = new BinaryWriter(MemoryStream);
        }

        /// <summary>
        /// Construct and write bytes.
        /// </summary>
        /// <param name="bytes">bytes</param>
        public ByteStream(byte[] bytes) : this()
        {
            Write(bytes);
        }

        /// <summary>
        /// Dispose of the resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of the resources.
        /// </summary>
        /// <param name="disposing">Disposing (not used)</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;

            BinaryReader.Dispose();
            BinaryWriter.Dispose();
            MemoryStream.Dispose();
            Disposed = true;
        }

        /// <summary
        /// >Write bytes.
        /// </summary>
        /// <param name="bytes">bytes</param>
        public void Write(byte[] bytes)
        {
            BinaryWriter.Write(bytes);
        }

        /// <summary>
        /// Get the current byte array.
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] GetBytes() => MemoryStream.ToArray();

        /// <summary>
        /// Read an 4-byte unsigned integer from the stream using 
        /// the `BinaryReader`. Sets stream to given position, and 
        /// advances stream position by 4 once done. Little-endian encoding.
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>uint</returns>
        public uint GetUint32(int position = 0)
        {
            BinaryReader.BaseStream.Position = position;
            return BinaryReader.ReadUInt32();
        }

        /// <summary>
        /// Read an 8-byte unsigned integer from the stream using 
        /// the `BinaryReader`. Sets stream to given position, and 
        /// advances stream position by 8 once done. Little-endian encoding.
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>ulong</returns>
        public ulong GetUint64(int position = 0)
        {
            BinaryReader.BaseStream.Position = position;
            return BinaryReader.ReadUInt64();
        }

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
        public string GetString(int position = 0, long chars = 0)
        {
            if (chars == 0) chars = BinaryReader.BaseStream.Length;
            BinaryReader.BaseStream.Position = position;
            return new string(BinaryReader.ReadChars((int) chars));
        }

        /// <summary>
        /// Read a 2-byte unsigned integer from the stream using 
        /// the `BinaryReader`. Sets stream to given position, and 
        /// advances stream position by 2 once done. Little-endian encoding.
        /// </summary>
        /// <param name="position">Position to read from</position>
        /// <returns>ushort</returns>
        public ushort GetUshort(int position = 0)
        {
            BinaryReader.BaseStream.Position = position;
            return BinaryReader.ReadUInt16();
        }

        /// <summary>
        /// Get an array of `ushort` from the stream using 
        /// the `BinaryReader`, reading sequentially from the 
        /// given start position. Does not verify the specified 
        /// number of `ushort` is available.
        /// </summary>
        /// <param name="count">Number of `ushort` to read</param>
        /// <param name="pos">Position to read from</param>
        /// <returns>ushort[]</returns>
        public ushort[] GetUshorts(int count, int pos = 0)
        {
            var ushorts = new ushort[count];
            for (var i = 0; i < count; i++)
            {
                pos = (i == 0) ? pos : (pos + 2);
                ushorts[i] = GetUshort(pos);
            }

            return ushorts;
        }
    }
}