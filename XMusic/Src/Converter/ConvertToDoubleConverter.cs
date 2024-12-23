using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XMusic.Src.Converter
{
    internal class ConvertToDoubleConverter : IValueConverter
    {
        private double increment;

        public double Increment
        {
            get => increment;
            set => increment = value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return increment + (double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
