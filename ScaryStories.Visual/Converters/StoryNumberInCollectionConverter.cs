using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

using ScaryStories.Entities.Dto;

namespace ScaryStories.Visual.Converters
{
    public class StoryNumberInCollectionConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var val = (StoryDto)value;
            var collection = (List<StoryDto>)parameter;
            return collection.IndexOf(val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
