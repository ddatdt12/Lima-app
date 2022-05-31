using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LibraryManagement.Views.Converter
{
    public class VnFormatConverter : IValueConverter
    {
        // This converts the result object to the foreground.
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo language)
        {
            // Retrieve the format string and use it to format the value.
            if (value == null) return null;
            int p = (int)value;

            if (p == 0)
            {
                return "0 ₫";
            }
            return String.Format(CultureInfo.InvariantCulture,
                                "{0:#,#} ₫", p);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
