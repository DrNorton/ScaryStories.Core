using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ScaryStories.Visual.Converters
{
    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ByteArrayToImage((byte[])value);
        }

        public static BitmapSource ByteArrayToImage(byte[] bytes)
        {
            BitmapImage bitmapImage = null;
            using (MemoryStream stream = new MemoryStream(bytes, 0, bytes.Length))
            {
                bitmapImage = new BitmapImage();
                bitmapImage.SetSource(stream);
            }

            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return null;
        }
    }

}
