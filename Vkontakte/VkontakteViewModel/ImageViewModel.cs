using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VkontakteViewModel
{
    public class ImageViewModel : BaseViewModel
    {
        private static BitmapImage BitmapPreview { get; set; }
        
        public ImageViewModel(string url)
        {
            if (BitmapPreview == null)
            {
                BitmapPreview=new BitmapImage(new Uri("/Imagees/PreloadImage.png",UriKind.Relative));
            }
            Image = BitmapPreview;
            if (DesignerProperties.IsInDesignTool)
            {
                return;
            }
            
            var client = new WebClient();
            client.OpenReadCompleted += ClientOpenReadCompleted;
            client.OpenReadAsync(new Uri(url));
        }


        void ClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            //Dispatcher.BeginInvoke(() => {
            var img = new BitmapImage();
            img.SetSource(e.Result);
            Image = img;
            //});
        }

        private BitmapImage bitmapImage;
        public BitmapImage Image
        {
            get { return bitmapImage; }
            set
            {
                Dispatcher.BeginInvoke(() =>
                {
                    bitmapImage = value;
                    OnPropertyChange("Image");
                });
            }
        }
    }
}
