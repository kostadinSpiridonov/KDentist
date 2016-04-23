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
    public class IsInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null&& value!="")
            {
                int intValue;
                if (int.TryParse(value.ToString(), out intValue))
                {
                    return intValue;
                }
                else
                {
                    var resultString = Regex.Match(value.ToString(), @"\d+").Value;
                    return int.Parse(resultString);
                }
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null&&value!="")
            {
                int intValue;
                if (int.TryParse(value.ToString(), out intValue))
                {
                    return intValue;
                }
                else
                {
                    var resultString = Regex.Match(value.ToString(), @"\d+").Value;
                    if (resultString != "")
                    {
                        return int.Parse(resultString);
                    }
                    return 1;
                }
            }
            return 1;
        }
    }
}
