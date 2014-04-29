using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaryStories.Helpers;

namespace ScaryStories.Visual.Models
{
    public class MenuItem
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public Uri ImagePath { get; set; }
        public ActionCommand NavigateMenuCommand { get; set; }
    }
}
