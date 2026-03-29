using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ClubManagementSystem.Helpers
{
    public class AttendanceToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // On vérifie si la valeur est un nombre (pour le taux de présence)
            if (value is int rate || (value != null && int.TryParse(value.ToString(), out rate)))
            {
                if (rate >= 80) return new SolidColorBrush(Colors.Green);      // Très présent
                if (rate >= 50) return new SolidColorBrush(Colors.Orange);     // Moyen
                return new SolidColorBrush(Colors.Red);                        // Peu présent
            }
            return new SolidColorBrush(Colors.Gray);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(Colors.Gray);
        }
    }
}