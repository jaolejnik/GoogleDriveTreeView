using System;
using System.Globalization;
using System.Windows.Data;

namespace GoogleDriveTreeView
{
    [ValueConversion(typeof(DirectoryItemActionType), typeof(string))]
    public class ActionTypeToStringConverter : IValueConverter
    {
        public static ActionTypeToStringConverter Instance = new ActionTypeToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((DirectoryItemActionType)value)
            {
                case DirectoryItemActionType.None:
                    return "";
                case DirectoryItemActionType.Download:
                    return "Download complete.";
                case DirectoryItemActionType.Delete:
                    return "An item has been deleted.";
                case DirectoryItemActionType.Create:
                    return "A new item has been created.";
                case DirectoryItemActionType.Upload:
                    return "A file has been uploaded.";
                default:
                    return "Loading...";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
