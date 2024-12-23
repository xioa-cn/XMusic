using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
namespace XMusic.Src.MainView.Converters;

public class BooleanToColorConverter : IValueConverter
{
    public Color TrueValue { get; set; }
    public Color FalseValue { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return new SolidColorBrush(boolValue ? TrueValue : FalseValue);
        }
        return new SolidColorBrush(FalseValue);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}