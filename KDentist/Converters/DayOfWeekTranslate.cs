using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KDentist.Converters
{
    public class DayOfWeekTranslate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = value.ToString();
            switch (date)
            {
                case "Friday":
                    {
                        return "Петък";
                    }
                case "Monday":
                    {
                        return "Понеделник";
                    }
                case "Tuesday":
                    {
                        return "Вторник";
                    }
                case "Wednesday":
                    {
                        return "Сряда";
                    }
                case "Thursday":
                    {
                        return "Четвъртък";
                    }
                case "Sunday":
                    {
                        return "Неделя";
                    }
                case "Saturday":
                    {
                        return "Събота";
                    }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return "";
        }
    }

    public class ToShortDS : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;
                return date.ToShortDateString();
            }
            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return "";
        }
    }
}
