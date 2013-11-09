using System.Windows.Media;

using ScaryStories.Helpers;

namespace ScaryStories.ViewModel.DataContext.Base {
    public class BaseTemplate:BasePropertyChanging {
        public delegate void OnSettingChangeHandler(SettingTemplate settings);
        public event OnSettingChangeHandler OnSettingChange;

        public BaseTemplate() {
            _template=new SettingTemplate();
        }

        private SettingTemplate _template;

        public SettingTemplate Template {
            get {
                return _template;
            }
            set {
                _template = value;
            }
        }
    }

    public class SettingTemplate:BasePropertyChanging {
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