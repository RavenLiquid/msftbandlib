using System;

namespace MSFTBandLib.Contracts.Metrics
{
    public interface ISleep
    {
        /// <summary>
        /// Calories burned
        /// </summary>
        uint Calories { get; }

        /// <summary>
        /// Duration of sleep (s)
        /// </summary>
        uint Duration { get; }

        /// <summary>
        /// Feeling (TODO: What is this?)
        /// </summary>
        uint Feeling { get; }

        /// <summary>
        /// Resting heart rate
        /// </summary>
        uint RestingHR { get; }

        /// <summary>
        /// Time asleep (s)
        /// </summary>
        uint TimeAsleep { get; }

        /// <summary>
        /// Time awake (s)
        /// </summary>
        uint TimeAwake { get; }

        /// <summary>
        /// Time taken to fall asleep (s)
        /// </summary>
        uint TimeToSleep { get; }

        /// <summary>
        /// Number of times swoke during sleep
        /// </summary>
        uint TimesAwoke { get; }

        /// <summary>
        /// Timestamp at which sleep object was retrieved
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Time of wakeup
        /// </summary>
        DateTime WokeUp { get; }

        /// <summary>
        /// Metric version
        /// </summary>
        ushort Version { get; }
    }
}