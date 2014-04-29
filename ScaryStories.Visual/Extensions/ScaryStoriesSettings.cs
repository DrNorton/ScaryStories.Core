using System.Windows.Media;

namespace ScaryStories.Visual.Extensions
{
    public class ScaryStorySettings
    {
        public delegate void OnAction();
        public event OnAction OnSettingsItemChange;

        private AccentColor _backgroundColor;
        private AccentColor _tileColor;
        private AccentColor _fontColor;
        private bool _isDynamicBackground;
        private FontFamily _fontFamily;

        public ScaryStorySettings() {
            BackgroundColor=new AccentColor();
            TileColor=new AccentColor();
            FontColor=new AccentColor();
        }

        public AccentColor BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set {
                var temp = _backgroundColor;
                _backgroundColor = value;
                if (temp != null) {
                    if (OnSettingsItemChange != null) {
                        OnSettingsItemChange();
                    }
                }

            }
        }

        public AccentColor TileColor
        {
            get
            {
                return _tileColor;
            }
            set {
                var temp = _tileColor;
                _tileColor = value;
                if (temp != null)
                {
                    if (OnSettingsItemChange != null)
                    {
                        OnSettingsItemChange();
                    }
                }
            }
        }

        public bool IsDynamicBackground
        {
            get
            {
                return _isDynamicBackground;
            }
            set {
                var temp = _isDynamicBackground;
                _isDynamicBackground = value;
                if (temp != null && temp!=value)
                {
                    if (OnSettingsItemChange != null)
                    {
                        OnSettingsItemChange();
                    }
                }
            }
        }

        public AccentColor FontColor {
            get {
                return _fontColor;
            }
            set {
                var temp = _fontColor;
                _fontColor = value;
                if (temp != null)
                {
                    if (OnSettingsItemChange != null)
                    {
                        OnSettingsItemChange();
                    }
                }
            }
        }

        public FontFamily FontFamily {
            get {
                return _fontFamily;
            }
            set {
                var temp = _fontFamily;
                _fontFamily = value;
                if (temp != null)
                {
                    if (OnSettingsItemChange != null)
                    {
                        OnSettingsItemChange();
                    }
                }
            }
        }
    }
}
