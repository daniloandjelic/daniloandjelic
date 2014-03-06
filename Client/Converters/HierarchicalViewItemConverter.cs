using Client.Framework;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Client.Converters
{
    class HierarchicalViewItemConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = string.Empty;
            string basic = string.Empty;
            string data = string.Empty;
            string dateOfBirth = string.Empty;
            if (value != null)
            {
                ChildsViewModel cvm = value as ChildsViewModel;

                basic = string.IsNullOrEmpty(cvm.Fl.Naziv) ? cvm.Fl.Ime + " " + cvm.Fl.Prezime : cvm.Fl.Naziv;
                data = string.IsNullOrEmpty(cvm.Fl.Pol) ? "---" : cvm.Fl.Pol;
                dateOfBirth = cvm.Fl.DatumRodjenja == null ? "---" : cvm.Fl.DatumRodjenja.Value.Year.ToString();
            }

            return string.Format("{0} | Pol: {1} | Godina rodjenja: {2}", basic, data, dateOfBirth);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #region TODO
    //public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        ChildsViewModel cvm = values[0] as ChildsViewModel;
    //        bool isExpanded = (bool)values[1];
    //        string itemText = string.Empty;
    //        string basic = string.Empty;
    //        string relationshipType = string.Empty;
    //        string pol = string.Empty;

    //        basic = string.IsNullOrEmpty(cvm.Fl.Naziv) ? cvm.Fl.Ime + " " + cvm.Fl.Prezime : cvm.Fl.Naziv;

    //        if (isExpanded)
    //        {

    //            if (cvm.CurrRecursiveType == "ChildParent")
    //            {
    //                if (cvm.Children != null && cvm.Children.Count() > 0)
    //                {
    //                    pol = cvm.Fl.Pol;

    //                    switch (pol)
    //                    {
    //                        case "M": relationshipType = "Sin";
    //                            break;
    //                        case "Z": relationshipType = "Ćerka";
    //                            break;
    //                        default: relationshipType = "---";
    //                            break;
    //                    }

    //                    //if (cvm.Fl.Pol == "M")

    //                    //    relationshipType = "Otac" + cvm.Fl.Pol;
    //                }
    //            }
    //            else
    //            {


    //            }


    //        }
    //        else// if (cvm.Children != null && cvm.Children.Count() > 0)
    //        {
    //            pol = pol = cvm.Fl.Pol;

    //            switch (pol)
    //            {
    //                case "M": relationshipType = "Otac";
    //                    break;
    //                case "Z": relationshipType = "Majka";
    //                    break;
    //                default: relationshipType = "---";
    //                    break;
    //            }
    //        }

    //        itemText = string.Format("{0} | {1}", basic, relationshipType);

    //        return itemText;
    //    }

    //    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    #endregion
}
