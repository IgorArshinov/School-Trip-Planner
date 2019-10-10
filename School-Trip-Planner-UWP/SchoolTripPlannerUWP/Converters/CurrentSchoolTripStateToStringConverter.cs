using System;
using Windows.UI.Xaml.Data;
using SchoolTripPlannerUWP.Core.Enumerations;

namespace SchoolTripPlannerUWP.Converters
{
    public class CurrentSchoolTripStateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;

            var schoolTripState = (SchoolTripState)value;

            switch (schoolTripState)
            {
                case SchoolTripState.Ongoing:
                    return "Lopend";
                case SchoolTripState.Over:
                    return "Voorbij";
                case SchoolTripState.Planned:
                    return "Gepland";
                default: return "Onbekend";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
