using System;
using Caliburn.Micro.BindableAppBar;

namespace ScaryStories.Visual.Extensions
{
    public class EnabledBindableAppBar:BindableAppBar
    {
        public EnabledBindableAppBar()
        {
            this.IsEnabledChanged += EnabledBindableAppBar_IsEnabledChanged;
        }

        void EnabledBindableAppBar_IsEnabledChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var res = (Boolean)e.NewValue;
            var bar = sender as BindableAppBar;
            foreach (var button in bar.Buttons)
            {
                if (button is BindableAppBarButton)
                {
                    ((BindableAppBarButton) button).Button.IsEnabled = res;
                }
                else
                {
                    ((BindableAppBarMenuItem)button).IsEnabled = res;
                }
            }
        }
    }
}
