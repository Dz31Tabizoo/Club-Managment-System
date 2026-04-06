using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;


namespace ClubManagementSystem.Helpers
{
    public class EnhanceBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = value is bool && (bool)value;

            // Si on passe "Inverse" en Parameter dans le XAML, on inverse le booléen
            if (parameter as string == "Inverse")
                bValue = !bValue;

            return bValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = value is Visibility v && v == Visibility.Visible;
            if (parameter as string == "Inverse")
                bValue = !bValue;
            return bValue;
        }
    }
}
