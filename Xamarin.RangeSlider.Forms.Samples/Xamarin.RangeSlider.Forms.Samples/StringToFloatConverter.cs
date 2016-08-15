using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xamarin.RangeSlider.Forms.Samples
{
    public class StringToFloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float result;
            if (float.TryParse(value.ToString(), out result))
                return result;
            return 0;
        }
    }
}