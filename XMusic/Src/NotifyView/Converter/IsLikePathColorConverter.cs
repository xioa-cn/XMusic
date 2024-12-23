using System;
using System.Globalization;
using System.Windows.Data;

namespace XMusic.Src.NotifyView.Converter;

public class IsLikePathColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool islike)
        {
            return islike ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
        }

        return System.Windows.Media.Brushes.White;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}