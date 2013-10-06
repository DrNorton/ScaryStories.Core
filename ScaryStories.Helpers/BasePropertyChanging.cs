using System.ComponentModel;

namespace ScaryStories.Helpers
{
		public class BasePropertyChanging : INotifyPropertyChanged, INotifyPropertyChanging
		{
				public event PropertyChangingEventHandler PropertyChanging;

				public event PropertyChangedEventHandler PropertyChanged;

				protected void NotifyPropertyChanged(string propertyName)
				{
						if (PropertyChanged != null)
						{
								PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
						}
				}

				protected void NotifyPropertyChanging(string propertyName)
				{
						if (PropertyChanging != null)
						{
								PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
						}
				}

		}
}
