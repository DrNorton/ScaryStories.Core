using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using VkontakteViewModel.Helper;

namespace VkontakteMangoClient.Helpers
{
    public static class FlickerExtension
    {
        public static FlickerArg GetFlickerArg(FlickGestureEventArgs arg)
        {
            return new FlickerArg()
            {
                Angle = arg.Angle,
                Direction = arg.Direction,
                HorizontalVelocity = arg.HorizontalVelocity,
                VerticalVelocity = arg.VerticalVelocity
            };
        }
    }
}
