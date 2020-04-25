using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace GoogleDriveTreeView
{
    [ValueConversion(typeof(DirectoryItemActionType), typeof(Visibility))]
    class ActionTypeToVisibilityConverter : IValueConverter
    {
        public static ActionTypeToVisibilityConverter Instance = new ActionTypeToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Console.WriteLine($"CONVERTER {(DirectoryItemActionType)value}");
            if ((DirectoryItemActionType)value != DirectoryItemActionType.None)
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
