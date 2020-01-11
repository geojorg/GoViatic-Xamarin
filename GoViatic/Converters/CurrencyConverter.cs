using System;
using System.Globalization;
using Xamarin.Forms;

namespace GoViatic.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dec = Decimal.Parse(value.ToString());
            return dec.ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valString = value.ToString();
            if (valString.Length <= 1)
            {
                return 0m;
            }
            else
            {
                var stringconverted = Decimal.Parse(valString, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol);
                return stringconverted;
            }
        }
    }
}