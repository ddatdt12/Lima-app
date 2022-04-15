using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LibraryManagement.Views.Converter
{
    public class ChangeIconConverter : IValueConverter
    {
        // This converts the result object to the foreground.
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo language)
        {
            // Retrieve the format string and use it to format the value.
            bool Param = (bool)value;

            if (Param == true)
                return PackIconKind.LeadPencil;
            return PackIconKind.Information;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
