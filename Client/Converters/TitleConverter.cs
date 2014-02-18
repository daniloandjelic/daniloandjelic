using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Client.Converters
{
    public class TitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string baseTitle = "Master aplikacija";
            return string.IsNullOrEmpty(value as string) ? string.Format("{0} {1} {2}", baseTitle,"|",string.Empty) : string.Format("{0} {1} {2}", baseTitle,"|", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
