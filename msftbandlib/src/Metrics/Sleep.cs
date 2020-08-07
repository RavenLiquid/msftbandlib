using MSFTBandLib.Command;
using MSFTBandLib.Includes;
using MSFTBandLib.Helpers;
using System;
using MSFTBandLib.Contracts.Command;
using MSFTBandLib.Contracts.Metrics;

namespace MSFTBandLib.Metrics
{
    /// <summary>
    /// Sleep metric
    /// </summary>
    public class Sleep : ISleep
    {
        /// <summary>
        /// Calories burned
        /// </summary>
        public uint Calories { get; protected set; }

        /// <summary>
        /// Duration of sleep (s)
        /// </summary>
        public uint Duration { get; protected set; }

        /// <summary>
        /// Feeling (TODO: What is this?)
        /// </summary>
        public uint Feeling { get; protected set; }

        /// <summary>
        /// Resting heart rate
        /// </summary>
        public uint RestingHR { get; protected set; }

        /// <summary>
        /// Time asleep (s)
        /// </summary>
        public uint TimeAsleep { get; protected set; }

        /// <summary>
        /// Time awake (s)
        /// </summary>
        public uint TimeAwake { get; protected set; }

        /// <summary>
        /// Time taken to fall asleep (s)
        /// </summary>
        public uint TimeToSleep { get; protected set; }

        /// <summary>
        /// Number of times swoke during sleep
        /// </summary>
        public uint TimesAwoke { get; protected set; }

        /// <summary>
        /// Timestamp at which sleep object was retrieved
        /// </summary>
        public DateTime Timestamp { get; protected set; }

        /// <summary>
        /// Time of wakeup
        /// </summary>
        public DateTime WokeUp { get; protected set; }

        /// <summary>
        /// Metric version
        /// </summary>
        public ushort Version { get; protected set; }

        /// <summary>
        /// Create a Sleep instance.
        /// 
        /// TODO: May be removed.
        /// </summary>
        public Sleep()
        {
        }

        /// <summary>
        /// Create a new Sleep instance from a Band last sleep response.
        /// 
        /// Does not in any way validate the given response is a valid sleep.
        /// </summary>
        /// <param name="response">response</param>
        public Sleep(ICommandResponse response)
        {
            var bytes = response.GetByteStream();
            Calories = bytes.GetUint32(26);
            Duration = (bytes.GetUint32(10) / 1000);
            Feeling = bytes.GetUint32(50);
            RestingHR = bytes.GetUint32(30);
            TimeAsleep = (bytes.GetUint32(22) / 1000);
            TimeAwake = (bytes.GetUint32(18) / 1000);
            TimeToSleep = (bytes.GetUint32(46) / 1000);
            TimesAwoke = bytes.GetUint32(14);
            Timestamp = DateTime.FromFileTime(((long) bytes.GetUint64(0)));
            WokeUp = DateTime.FromFileTime(((long) bytes.GetUint64(38)));
            Version = bytes.GetUshort(8);
        }
    }
}