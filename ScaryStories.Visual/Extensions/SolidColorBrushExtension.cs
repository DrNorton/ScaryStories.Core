using System;
using System.Windows.Media;

namespace ScaryStories.Visual.Extensions
{
    public static class SolidColorBrushExtension
    {
        /// <summary>
        /// Convert the Brush to a ARGB - Color. 
        /// </summary>
        /// <param name="brush">your object</param>
        /// <returns>
        /// White = #ffffffff
        /// Green = #ff00ff00
        /// </returns>
        public static string ToARGB(this SolidColorBrush brush,string name)
        {
            if (brush == null) throw new ArgumentNullException();
            var c = brush.Color;
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}|{4}", c.A, c.R, c.G, c.B,name);
        }

        /// <summary>
        /// set the current brush to a new color based on the #argb string
        /// </summary>
        /// <param name="brush">your object</param>
        /// <param name="argb">The #ARGB Color</param>
        /// <returns>the same object as you run the function</returns>
        public static Tuple<string, SolidColorBrush> FromARGB(string argb) {
           
            SolidColorBrush brush=new SolidColorBrush();
            byte a = Convert.ToByte(int.Parse(argb.Substring(1, 2), System.Globalization.NumberStyles.HexNumber));
            byte r = Convert.ToByte(int.Parse(argb.Substring(3, 2), System.Globalization.NumberStyles.HexNumber));
            byte g = Convert.ToByte(int.Parse(argb.Substring(5, 2), System.Globalization.NumberStyles.HexNumber));
            byte b = Convert.ToByte(int.Parse(argb.Substring(7, 2), System.Globalization.NumberStyles.HexNumber));
            var c = Color.FromArgb(a, r, g, b);
            var split = argb.Split(new char[] { '|' });

            brush.Color = c;
            return new Tuple<string, SolidColorBrush>(split[1],brush);
        }

        public class Tuple<T, U>
        {
            public T Item1 { get; private set; }
            public U Item2 { get; private set; }

            public Tuple(T item1, U item2)
            {
                Item1 = item1;
                Item2 = item2;
            }
        }
    }
}
