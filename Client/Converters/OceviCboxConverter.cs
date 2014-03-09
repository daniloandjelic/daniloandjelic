using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Client.Converters
{
    public class OceviCboxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = string.Empty;
            string basic = string.Empty;
            string data = string.Empty;
            string dateOfBirth = string.Empty;

            FizickoLice fl = value as FizickoLice;
            if (fl != null)
            {
                basic = string.IsNullOrEmpty(fl.Naziv) ? fl.Ime + " " + fl.Prezime : fl.Naziv;
                data = string.IsNullOrEmpty(fl.Pol) ? "---" : fl.Pol;
                dateOfBirth = fl.DatumRodjenja == null ? "---" : fl.DatumRodjenja.Value.Year.ToString(); 
            }
            return string.Format("{0} | Pol: {1} | Godina rodjenja: {2}", basic, data, dateOfBirth);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
