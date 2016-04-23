using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KDentist.Converters
{
    public class IsEGN : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != "")
            {
               return Regex.Match(value.ToString(), @"\d+").Value;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != "")
            {
                return Regex.Match(value.ToString(), @"\d+").Value;
            }
            return "";
        }
    }
}
