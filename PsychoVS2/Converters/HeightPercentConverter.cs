using System;
using System.Globalization;
using System.Windows.Data;

namespace PsychoVS2.Converters
{
    public class HeightPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double height = (double)value;
            double percent = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            return height * percent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WidthPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = (double)value;
            double percent = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            return width * percent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
