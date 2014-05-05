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

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                base.NotifyOfPropertyChange(()=>IsLoading);
            }
        }

        public void Wait(bool wait)
        {
            IsLoading = wait;
        }
    }
}
