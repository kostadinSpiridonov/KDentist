using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KDentist.Converters
{
    public class FourChars: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null )
            {
               if(value.ToString().Count()>4)
               {
                   return value.ToString().Substring(0, 4);
               }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value.ToString().Count() > 4)
                {
                    return value.ToString().Substring(0, 4);
                }
            }
            return value;
        }
    }
}
