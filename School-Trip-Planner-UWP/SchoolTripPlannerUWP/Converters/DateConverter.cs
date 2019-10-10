using System;
using Windows.UI.Xaml.Data;

namespace SchoolTripPlannerUWP.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;

            return ((DateTime)value).ToLocalTime().ToString("dd/MM/yyyy HH:MM");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
