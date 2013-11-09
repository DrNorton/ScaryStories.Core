using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Animation;

using ScaryStories.Helpers;
using ScaryStories.ViewModel.DataContext.Base;
using ScaryStories.ViewModel.Extensions;

namespace ScaryStories.ViewModel.DataContext.MenuDataContexts
{
    public class SettingsMenuDataContext : BaseTemplate, IMenuItem {
      
        private List<AccentColor> _colors;
        private SettingsManager _settingsManager;
        private List<string> _allFonts;
        
 
        public SettingsMenuDataContext(SettingsManager settingsManager) {
           _colors=new List<AccentColor>();
           _settingsManager = settingsManager;
        }

        public string Header
        {
            get {
              return "Настройки";
            }
        }

        public string Description
        {
            get {
                return "Настройки программы";
            }
        }

        public Uri ImagePath
        {
            get { return new Uri("/Content/settings.png", UriKind.Relative); }
        }

        public string DataContextCode
        {
            get {
                return "SettingsPage";
            }
        }

        public List<AccentColor> Colors {
            get {
                return _colors;
            }
            set {
                _colors = value;
            }
        }

        public AccentColor SelectedBackgroundColor
        {
            get {
                return _colors.FirstOrDefault(x => x.Name == _settingsManager.Settings.BackgroundColor.Name);
            }
            set {
                _settingsManager.Settings.BackgroundColor = value;
            }
        }

        public AccentColor SelectedTileColor {
            get {
                return _colors.FirstOrDefault(x => x.Name == _settingsManager.Settings.TileColor.Name);
            }
            set {
                _settingsManager.Settings.TileColor = value;
            }
        }

        public AccentColor SelectedFontColor
        {
            get
            {
                return _colors.FirstOrDefault(x => x.Name == _settingsManager.Settings.FontColor.Name);
            }
            set
            {
                _settingsManager.Settings.FontColor = value;
            }
        }

        public FontFamily SelectedFamilyFont {
            get {
                return _settingsManager.Settings.FontFamily;
            }
            set {
                _settingsManager.Settings.FontFamily = value;
            }
        }

        public bool IsDynamicBackground {
            get {
                return _settingsManager.Settings.IsDynamicBackground;
            }
            set {
                _settingsManager.Settings.IsDynamicBackground = value;
               
            }
        }

        public void Run() {
           CreateColors();
            GetFonts();

        }

        private void CreateColors() {
            Colors=new List<AccentColor>();
            Colors.Add(new AccentColor() { Color = 0xFFFF0097.ToSolidColorBrush(), Name = "magenta" });
            Colors.Add(new AccentColor() { Color = 0xFFA200FF.ToSolidColorBrush(), Name = "purple" });
            Colors.Add(new AccentColor() { Color = 0xFF00ABA9.ToSolidColorBrush(), Name = "teal" });
            Colors.Add(new AccentColor() { Color = 0xFF8CBF26.ToSolidColorBrush(), Name = "lime" });
            Colors.Add(new AccentColor() { Color = 0xFFA05000.ToSolidColorBrush(), Name = "brown" });
            Colors.Add(new AccentColor() { Color = 0xFFE671B8.ToSolidColorBrush(), Name = "pink" });
            Colors.Add(new AccentColor() { Color = 0xFFF09609.ToSolidColorBrush(), Name = "orange" });
            Colors.Add(new AccentColor() { Color = 0xFF1BA1E2.ToSolidColorBrush(), Name = "blue" });
            Colors.Add(new AccentColor() { Color = 0xFFE51400.ToSolidColorBrush(), Name = "red" });
            Colors.Add(new AccentColor() { Color = 0xFF339933.ToSolidColorBrush(), Name = "green" });
            Colors.Add(new AccentColor() { Color = 0xFFF09609.ToSolidColorBrush(), Name = "mango" });
            Colors.Add(new AccentColor() { Color = 0xF0000001.ToSolidColorBrush(), Name = "black" });
            Colors.Add(new AccentColor() { Color = 0xFFFFFFFF.ToSolidColorBrush(), Name = "white" });
        }

        private void GetFonts() {
            _allFonts=new List<string>();
        }
    }
}
