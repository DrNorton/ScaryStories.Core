using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ScaryStories.Helpers.Converters
{
		public static class ImageToByte
		{
				public static BitmapImage ByteArraytoBitmap(Byte[] byteArray)
				{
						MemoryStream stream = new MemoryStream(byteArray);
						BitmapImage bitmapImage = new BitmapImage();
						bitmapImage.SetSource(stream);
						return bitmapImage;
				}
		}
}
