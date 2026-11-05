using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HiresEpuletek;

// Saját értékátalakító: ha a kötött érték null (nincs kiválasztott épület), elrejti az elemet.
public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null ? Visibility.Hidden : Visibility.Visible;
    }

    // Visszafelé (cél → forrás) nem használjuk.
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
