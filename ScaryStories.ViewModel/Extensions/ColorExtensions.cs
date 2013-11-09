using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ScaryStories.ViewModel.Extensions
{
    public static class ColorExtensions
    {
       
        /// <summary>
        /// Returns a Color for a hex value.
        /// </summary>
        /// <param name="argb">The hex value</param>
        /// <remarks>Calls to this method should look like 0xFF11FF11.ToColor().</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "argb", Justification = "By design")]
        public static Color ToColor(this uint argb)
        {
            return Color.FromArgb((byte)((argb & 0xff000000) >> 0x18),
                                  (byte)((argb & 0xff0000) >> 0x10),
                                  (byte)((argb & 0xff00) >> 8),
                                  (byte)(argb & 0xff));
        }

        /// <summary>
        /// Returns a SolidColorBrush for a hex value.
        /// </summary>
        /// <param name="argb">The hex value</param>
        /// <remarks>Calls to this method should look like 0xFF11FF11.ToSolidColorBrush().</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "argb", Justification = "By design")]
        public static SolidColorBrush ToSolidColorBrush(this uint argb)
        {
            return new SolidColorBrush(argb.ToColor());
        }
    }
}
