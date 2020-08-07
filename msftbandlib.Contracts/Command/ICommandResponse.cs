using MSFTBandLib.Contracts.Includes;

namespace MSFTBandLib.Contracts.Command
{
    public interface ICommandResponse
    {
        /// <summary>
        /// Add a new response bytes sequence to the response.
        /// 
        /// Automatically detects the presence of the Band status byte 
        /// sequence at the start of end of the bytes array and handles it 
        /// accordingly, assigning it to `Status` (overwriting any previous 
        /// `Status` found in a previous byte sequence added to this response 
        /// instance) and using the rest of the bytes as data bytes.
        /// 	
        /// The data bytes are appended as a new item in the data list.
        /// </summary>
        /// <param name="bytes">bytes</param>
        void AddResponse(byte[] bytes);

        /// <summary>
        /// Get the data associated with the response.
        /// 
        /// Returns the data bytes array in the `Data` list 
        /// of received data sequences at the given index.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>byte[]</returns>
        byte[] GetData(int index = 0);

        /// <summary>
        /// Get the data associated with the response as a `ByteStream`.
        /// 
        /// Returns a `ByteStream` for the data bytes array in the `Data` 
        /// list of received data sequences at the given index.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>ByteStream</returns>
        IByteStream GetByteStream(int index = 0);

        /// <summary>
        ///	Get whether the response has status bytes.
        /// </summary>
        /// <returns>bool</returns>
        bool StatusReceived();
    }
}