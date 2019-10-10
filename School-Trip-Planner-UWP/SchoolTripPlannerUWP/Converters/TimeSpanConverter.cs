using System;
using Windows.UI.Xaml.Data;

namespace SchoolTripPlannerUWP.Converters
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;

            var timeSpan = (TimeSpan)value;

            if (timeSpan.Days > 0)
            {
                return timeSpan.ToString(@"dd") + " dagen " + timeSpan.ToString(@"hh\:mm");
            }
            else
            {
                return timeSpan.ToString(@"hh\:mm");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
