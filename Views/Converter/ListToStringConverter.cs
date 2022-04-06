using LibraryManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LibraryManagement.Views.Converter
{
    public class ListToStringConverter : IValueConverter
    {
        // This converts the result object to the foreground.
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo language)
        {
            // Retrieve the format string and use it to format the value.
            List<AuthorDTO> Param = value as List<AuthorDTO>;

            if (Param != null)
            {
                string res = "";
                foreach (var item in Param)
                {
                    res += item.name + ", ";
                }
                string tempres = res.Remove(res.Length -2);
                return tempres;
            }
            return "Trống";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
