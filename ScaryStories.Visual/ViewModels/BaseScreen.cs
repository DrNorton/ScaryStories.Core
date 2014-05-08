using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace ScaryStories.Visual.ViewModels
{
    public class BaseScreen:Screen
    {
        private bool _isLoading;
        private bool _appBarEnabled;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                base.NotifyOfPropertyChange(()=>IsLoading);
            }
        }

        public bool AppBarEnabled
        {
            get { return _appBarEnabled; }
            set
            {
                _appBarEnabled = value;
                base.NotifyOfPropertyChange(()=>AppBarEnabled);
            }
        }

        public void Wait(bool wait)
        {
            IsLoading = wait;
            AppBarEnabled = !wait;
        }
    }
}
