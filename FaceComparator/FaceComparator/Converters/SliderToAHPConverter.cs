using System;
using System.Globalization;
using System.Windows.Data;

namespace FaceComparator.Converters
{
    class SliderToAHPConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (double) value;

            if (Math.Abs(val - 1d/9d) < 0.00001) return -4;
            if (Math.Abs(val - 1d/7d) < 0.00001) return -3;
            if (Math.Abs(val - 1d/5d) < 0.00001) return -2;
            if (Math.Abs(val - 1d/3d) < 0.00001) return -1;
            if (Math.Abs(val - 1) < 0.00001) return 0;
            if (Math.Abs(val - 3) < 0.00001) return 1;
            if (Math.Abs(val - 5) < 0.00001) return 2;
            if (Math.Abs(val - 7) < 0.00001) return 3;
            if (Math.Abs(val - 9) < 0.00001) return 4;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (double)value;

            if (Math.Abs(val - -4) < 0.00001) return 1d/9d;
            if (Math.Abs(val - -3) < 0.00001) return 1d/7d;
            if (Math.Abs(val - -2) < 0.00001) return 1d/5d;
            if (Math.Abs(val - -1) < 0.00001) return 1d/3d;
            if (Math.Abs(val - 0) < 0.00001) return 1;
            if (Math.Abs(val - 1) < 0.00001) return 3;
            if (Math.Abs(val - 2) < 0.00001) return 5;
            if (Math.Abs(val - 3) < 0.00001) return 7;
            if (Math.Abs(val - 4) < 0.00001) return 9;

            return 1;
        }
    }
}
