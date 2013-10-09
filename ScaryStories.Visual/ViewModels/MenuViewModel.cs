using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Helpers;

namespace ScaryStories.Visual.ViewModels
{
    public class MenuViewModel:BasePropertyChanging {
        private List<MenuItem> _menu;
        
        

        public MenuViewModel() {
              CreateMenu();   
        }

        private void CreateMenu() {
            Menu=new List<MenuItem>();
            Menu.Add(new MenuItem() { Header = "Все истории", Description = "Список всех страшных историй", ImagePath = new Uri("/Content/allStories.png",UriKind.Relative) });
            Menu.Add(new MenuItem() { Header = "Все истории", Description = "Список всех страшных историй", ImagePath = new Uri("/Content/allStories.png", UriKind.Relative) });
      
        }

        public List<MenuItem> Menu {
            get {
                return _menu;
            }
            set {
                _menu = value;
            }
        }
    }

    public class MenuItem {
        public string Header { get; set; }
        public string Description { get; set; }
        public Uri ImagePath { get; set; }
    }

}
