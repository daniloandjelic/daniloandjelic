using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Client.Converters
{
    public class FizickoLiceListFatherNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FizickoLice fl = value as FizickoLice;
            string result = string.Empty;
            if (fl != null)
                result = string.IsNullOrEmpty(fl.Naziv) ? fl.Ime + " " + fl.Prezime : fl.Naziv;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class FizickoLiceListMotherNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FizickoLice fl = value as FizickoLice;
            string result = string.Empty;
            if (fl != null)
                result = string.IsNullOrEmpty(fl.Naziv) ? fl.Ime + " " + fl.Prezime : fl.Naziv;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
