using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace GoogleDriveTreeView
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class UserToVisibilityConverter : IValueConverter
    {
        public static UserToVisibilityConverter Instance = new UserToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "admin")
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
