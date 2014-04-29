using System.Windows;
using Caliburn.Micro;
using ScaryStories.Visual.Extensions;

namespace ScaryStories.Visual
{
		public partial class App : Application
		{
				public App()
				{
						InitializeComponent();
                        var settingsManager = IoC.Get<ISettingsManager>();
                        App.Current.Resources.Remove("Template");
                        App.Current.Resources.Add("Template", settingsManager.Listener);
				}
		}
}