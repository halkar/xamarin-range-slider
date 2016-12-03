using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Xamarin.RangeSlider.Forms.Samples
{
    [Preserve(AllMembers = true)]
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result;
            if (int.TryParse(value.ToString(), out result))
                return result;
            return 0;
        }
    }
}