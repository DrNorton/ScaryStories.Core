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


namespace VkontakteViewModel.Helper
{
    /// <summary>
    /// Helper class relation with bug in Mango toolkit
    /// </summary>
    public class FlickerArg
    {
        public double HorizontalVelocity { get; set; }
        public double VerticalVelocity { get; set; }
        public double Angle { get; set; }
        public Orientation Direction { get; set; }
    }
}
