using System;
using System.Collections.Generic;
using System.Text;
using MSFTBandLib.Contracts;

namespace MSFTBandLib.src
{
    /// <summary>
    /// Theme color information
    /// </summary>
    public class Theme : ITheme
    {
        public uint BaseColor { get; set; }
        public uint Highlight { get; set; }
        public uint Lowlight { get; set; }
        public uint SecondaryText { get; set; }
        public uint HighContrast { get; set; }
        public uint Muted { get; set; }

        /// <inheritdoc />
        public void SetColors(uint[] colors)
        {
            BaseColor = colors[0];
            HighContrast = colors[1];
            Lowlight = colors[2];
            SecondaryText = colors[3];
            HighContrast = colors[4];
            Muted = colors[5];
        }

        /// <inheritdoc />
        public uint[] GetColors()
        {
            return new[] {BaseColor, Highlight, Lowlight, SecondaryText, HighContrast, Muted};
        }
    }
}
