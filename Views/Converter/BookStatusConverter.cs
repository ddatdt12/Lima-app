using LibraryManagement.Utils;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LibraryManagement.Views.Converter
{
    public class BookStatusConverter : IValueConverter
    {
        // This converts the result object to the foreground.
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo language)
        {
            // Retrieve the format string and use it to format the value.
            int Param = (int)value;

            if (Param == (int)BookInfoStatus.AVAILABLE)
                return "Khả dụng";
            else if(Param == (int)BookInfoStatus.BORROWING)
                return "Đang mượn";
            return "Đã hỏng";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
