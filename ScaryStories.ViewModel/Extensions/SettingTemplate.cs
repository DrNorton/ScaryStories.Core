using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScaryStories.Helpers;

namespace ScaryStories.ViewModel.Extensions
{
    public class SettingTemplate : BasePropertyChanging
    {
        private SolidColorBrush _backgroundColor;
        private SolidColorBrush _additionColor;
        private SolidColorBrush _fontColor;
        private FontFamily _fontFamily;
        private int _fontSize;

        public SolidColorBrush BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
                base.NotifyPropertyChanged("BackgroundColor");
            }
        }
        public SolidColorBrush AdditionColor
        {
            get
            {
                return _additionColor;
            }
            set
            {
                _additionColor = value;
                base.NotifyPropertyChanged("AdditionColor");
            }
        }
        public FontFamily FontFamily
        {
            get
            {
                return _fontFamily;
            }
            set
            {
                _fontFamily = value;
                base.NotifyPropertyChanged("FontFamily");
            }
        }
        public int FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                base.NotifyPropertyChanged("FontSize");
            }
        }
        public SolidColorBrush FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                _fontColor = value;
                base.NotifyPropertyChanged("FontColor");
            }
        }

    }
}
