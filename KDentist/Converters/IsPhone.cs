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
    public class IsPhone:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != "")
            {
                if (!Regex.Match(value.ToString(), "^[0-9]*$").Success)
                {
                    var valToString = value.ToString();
                    return valToString.Remove(valToString.Count() - 1, 1);
                }

                return value.ToString();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value != "")
            {
                if(!Regex.Match(value.ToString(), "^[0-9]*$").Success)
                {
                    var valToString=value.ToString();
                    return valToString.Remove(valToString.Count() - 1, 1);
                }

                return value.ToString();
            }
            return "";
        }
    }
}
