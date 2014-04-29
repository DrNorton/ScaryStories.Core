using System.IO.IsolatedStorage;
using System.Windows.Media;

namespace ScaryStories.Visual.Extensions
{
    public interface ISettingsManager
    {
        ScaryStorySettings Settings { get; }
        SettingTemplate Listener { get; set; }
        void Load();
        void Save();
    }

    public class SettingsManager : ISettingsManager
    {
        private ScaryStorySettings _settings;
        private IsolatedStorageSettings _isolatedStorageSettings;
        private SettingTemplate _listener;

        public SettingsManager(IsolatedStorageSettings isolatedStorageSettings) {
            _isolatedStorageSettings = isolatedStorageSettings;
            if (_isolatedStorageSettings.Count == 0) {
                CreateDefaultSettingsList();
            }
        }

        public ScaryStorySettings Settings {
            get {
                return _settings;
            }
        }

        public SettingTemplate Listener {
            get {
                return _listener;
            }
            set {
                _listener = value;
            }
        }

        public void Load() {
            _settings=new ScaryStorySettings();
            var background =SolidColorBrushExtension.FromARGB((string)LoadFromStorageSettings("BackgroundColor"));
            _settings.BackgroundColor.Name = background.Item1;
            _settings.BackgroundColor.Color = background.Item2;
            var tileColor= SolidColorBrushExtension.FromARGB((string)LoadFromStorageSettings("TileColor"));
            _settings.TileColor.Name = tileColor.Item1;
            _settings.TileColor.Color = tileColor.Item2;
            _settings.IsDynamicBackground = (bool)LoadFromStorageSettings("IsDynamicBackground");
            _settings.OnSettingsItemChange += OnItemChange;
            _settings.FontFamily = new FontFamily((string)LoadFromStorageSettings("FontFamily"));
            var fontColor = SolidColorBrushExtension.FromARGB((string)LoadFromStorageSettings("FontColor"));
            _settings.FontColor.Name = fontColor.Item1;
            _settings.FontColor.Color = fontColor.Item2;
            CreateTemplateSetting(_settings);
        }

        private void CreateTemplateSetting(ScaryStorySettings settings) {
            if (Listener == null) {
                Listener = new SettingTemplate();
            }
            Listener.BackgroundColor = settings.BackgroundColor.Color;
            Listener.FontColor = settings.FontColor.Color;
            Listener.FontFamily = settings.FontFamily;
            Listener.AdditionColor = settings.TileColor.Color;
            

        }

        private void OnItemChange() {
            Save();
        }

        public void Save() {
            SaveToStorageSettings("BackgroundColor", SolidColorBrushExtension.ToARGB(Settings.BackgroundColor.Color,Settings.BackgroundColor.Name));
            SaveToStorageSettings("TileColor", SolidColorBrushExtension.ToARGB(Settings.TileColor.Color,Settings.TileColor.Name));
            SaveToStorageSettings("IsDynamicBackground", Settings.IsDynamicBackground);
            SaveToStorageSettings("FontColor", SolidColorBrushExtension.ToARGB(Settings.FontColor.Color,Settings.FontColor.Name));
            SaveToStorageSettings("FontFamily", Settings.FontFamily.Source);
            _isolatedStorageSettings.Save();
            CreateTemplateSetting(_settings);
        }

        private void CreateDefaultSettingsList() {
            _settings=new ScaryStorySettings();

            Settings.BackgroundColor = new AccentColor() { Color = 0xF0000001.ToSolidColorBrush(), Name = "black" };
            Settings.TileColor = new AccentColor() { Color = 0xFFE51400.ToSolidColorBrush(), Name = "red" };
            Settings.IsDynamicBackground = false;
            _settings.OnSettingsItemChange += OnItemChange;
            _settings.FontFamily = new FontFamily("Times New Roman");
            _settings.FontColor = new AccentColor() { Color = 0xFFFFFFFF.ToSolidColorBrush(), Name = "white" };
            Save();
        }


        private void SaveToStorageSettings(string code,object value) {
            if (!_isolatedStorageSettings.Contains(code))
            {
                _isolatedStorageSettings.Add(code, value);
            }
            else
            {
                _isolatedStorageSettings[code] = value;
            }
        }

        private object LoadFromStorageSettings(string code) {
            if (_isolatedStorageSettings.Contains(code)) {
                return _isolatedStorageSettings[code];
            }
            return null;
        }
    }
}
