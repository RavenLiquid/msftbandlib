using System;
using System.Collections.Generic;
using System.Text;

namespace MSFTBandLib.Contracts
{
    /// <summary>
    /// Theme interface
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// Base color
        /// </summary>
        uint BaseColor { get; set; }
        /// <summary>
        /// HighlightColor
        /// </summary>
        uint Highlight { get; set; }
        /// <summary>
        /// Lowlight color
        /// </summary>
        uint Lowlight { get; set; }
        /// <summary>
        /// Secondary text color
        /// </summary>
        uint SecondaryText { get; set; }
        /// <summary>
        /// High Contrast color
        /// </summary>
        uint HighContrast { get; set; }
        /// <summary>
        /// Muted
        /// </summary>
        uint Muted { get; set; }

        /// <summary>
        /// Sets the colors from an uint array
        /// Expected order:
        /// BaseColor, Highlight, Lowlight, SecondaryText, HighContrast, Muted
        /// </summary>
        /// <param name="colors"></param>
        void SetColors(uint[] colors);

        /// <summary>
        /// Gets the set colors as an uint array
        /// </summary>
        /// <returns></returns>
        uint[] GetColors();
    }
}
