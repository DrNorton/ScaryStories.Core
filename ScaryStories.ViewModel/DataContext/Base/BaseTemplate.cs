using System.Windows.Media;

using ScaryStories.Helpers;
using ScaryStories.ViewModel.Extensions;

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

   

  
}