using System;
using System.Globalization;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace SchoolTripPlannerUWP.Converters
{
    public class ToTitleCaseConverter : IValueConverter
    {
        private static TextInfo _textInfo = CultureInfo.CurrentCulture.TextInfo;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;

            var stringValue = (string)value;
            return _textInfo.ToTitleCase(stringValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;

            var stringValue = (string)value;
            return _textInfo.ToTitleCase(stringValue);
        }

        public static void StaticConvert(object sender)
        {
            var textBox = (TextBox)sender;
            textBox.Text = _textInfo.ToTitleCase(textBox.Text);
            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}
