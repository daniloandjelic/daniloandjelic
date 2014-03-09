using BusinessLogicLayer;
using BusinessLogicLayer.Implementations;
using Client.Commands;
using Client.Framework;
using Client.Windows;
using DataAccessLayer;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class FizickoLiceViewModel : SubmitViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Unos novog Fizickog lica"; }
        }

        public override bool SubmitCommand_CanExecute(object obj)
        {
            return true;
        }

        public override void SubmitCommand_Execute(object obj)
        {
            // TODO - Pozvati u drugom thread-u da vrti progress bar
            // Treba dodati u DAL da vraca poruku/bool uspesnosti operacije pa na nju da se bindujem za progress bar
            // kada zavrsi progress bar, onda pozvati CloseWindow
            
            if (IsValid)
            {
                FizickoLice fl = obj as FizickoLice;
                IBusinessLayerFacade<FizickoLice> bl = new FizickoLiceBusinessLayerImplementation();
                if (fl.Id != 0)
                    bl.Update(fl);
                else
                    bl.Create(fl);
            }
            else
            {
                if (!_SubmitAttempted)
                {
                    _SubmitAttempted = true;
                    foreach (string property in this.InvalidFields)
                        OnPropertyChanged(property);
                }
            }
            
        }


        public override void OpenDialog_Execute(object obj)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "Slike (.png)|*.png";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImagePath = dlg.FileName;
                FizickoLice fl = obj as FizickoLice;

                if (fl != null)
                    fl.Slika = System.IO.File.ReadAllBytes(ImagePath);
            }
        }

        protected override string ValidateProperty(string columnName)
        {
            string ret = string.Empty;
            FizickoLice fl = ObjectToPersist as FizickoLice;
            switch (columnName)
            {
                case "Ime":
                    if (fl != null && string.IsNullOrEmpty(fl.Ime) || string.IsNullOrEmpty(Ime))
                        ret = "Unos imena fizičkog lica je obavezan!";
                    break;
                case "Prezime":
                    if (fl != null && string.IsNullOrEmpty(fl.Prezime) || string.IsNullOrEmpty(Prezime))
                        ret = "Unos prezimena fizičkog lica je obavezan!";
                    break;
                case "IdentifikacioniBroj":
                    if (fl != null && string.IsNullOrEmpty(fl.IdentifikacioniBroj) || string.IsNullOrEmpty(IdentifikacioniBroj))
                        ret = "Unos JMBG-a je obavezan!";
                    break;
                case "DatumRodjenja":
                    if (fl != null && fl.DatumRodjenja != null && (DateTime)fl.DatumRodjenja > DateTime.Now)
                        ret = "Datum rodjenja mora biti pre današnjeg datuma!";
                    break;
            }

            return ret;
        }

        #region Binding properties

        private Client.Enums.ClientEnums.Pol pol;
        public Client.Enums.ClientEnums.Pol Pol
        {
            get
            {
                return pol;
            }
            set
            {
                pol = value;
                (ObjectToPersist as FizickoLice).Pol = value.ToString();
                OnPropertyChanged("Pol");
            }
        }

        private ObservableCollection<Client.Enums.ClientEnums.Pol> polovi;
        public ObservableCollection<Client.Enums.ClientEnums.Pol> Polovi
        {
            get
            {
                return new ObservableCollection<Enums.ClientEnums.Pol>(Enum.GetValues(typeof(Client.Enums.ClientEnums.Pol)).Cast<Client.Enums.ClientEnums.Pol>());
            }
            set
            {
                polovi = value;
                OnPropertyChanged("Polovi");
            }

        }

        private int? selIndex = null;
        public int? SelIndex
        {
            get
            {
                return selIndex;
            }
            set
            {
                selIndex = value;
                OnPropertyChanged("SelIndex"); 
            }
        }

        private string ime;
        public string Ime
        {
            get
            {
                if (ObjectToPersist != null)
                    return (ObjectToPersist as FizickoLice).Ime;
                else
                    return string.Empty;
            }
            set
            {
                ime = value;
                (ObjectToPersist as FizickoLice).Ime = value;
                OnPropertyChanged("Ime");
            }
        }


        private string prezime;
        public string Prezime
        {
            get
            {
                if (ObjectToPersist != null)
                    return (ObjectToPersist as FizickoLice).Prezime;
                else
                    return string.Empty;
            }
            set
            {
                prezime = value;
                (ObjectToPersist as FizickoLice).Prezime = value;
                OnPropertyChanged("Prezime");
            }
        }

        private string identifikacioniBroj;
        public string IdentifikacioniBroj
        {
            get
            {
                if (ObjectToPersist != null)
                    return (ObjectToPersist as FizickoLice).IdentifikacioniBroj;
                else
                    return string.Empty;
            }
            set
            {
                identifikacioniBroj = value;
                (ObjectToPersist as FizickoLice).IdentifikacioniBroj = value;
                OnPropertyChanged("IdentifikacioniBroj");
            }
        }

        private DateTime? datumRodjenja;
        public DateTime? DatumRodjenja
        {
            get
            {
                if (ObjectToPersist != null)
                    return (ObjectToPersist as FizickoLice).DatumRodjenja;
                else
                    return null;
            }
            set
            {
                datumRodjenja = value;
                (ObjectToPersist as FizickoLice).DatumRodjenja = value;
                OnPropertyChanged("DatumRodjenja");
            }
        }

        #endregion


        private FizickoLice otac;
        public FizickoLice Otac
        {
            get
            {
                return otac;
            }
            set
            {
                otac = value;
                (ObjectToPersist as FizickoLice).Otac = value;
                OnPropertyChanged("Otac");
            }
        }

        private ObservableCollection<FizickoLice> muskaFizickaLica;
        public ObservableCollection<FizickoLice> MuskaFizickaLica
        {
            get 
            {
                return muskaFizickaLica; 
            }
            set
            {
                muskaFizickaLica = value;
                OnPropertyChanged("MuskaFizickaLica");
            }
        }
        internal void RefreshOceviCollection()
        {
            GenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
            List<FizickoLice> collFl = dal.GetList(fl => fl.Pol == "M" && fl.Id != (ObjectToPersist as FizickoLice).Id).ToList();
            MuskaFizickaLica = new ObservableCollection<FizickoLice>(collFl);
            SetOtac();
        }

        internal void SetOtac()
        {
            Otac = (ObjectToPersist as FizickoLice).Otac;
            if (otac != null)
            {
                SelIndex = MuskaFizickaLica.Select((c, i) => new { FizickoLice = c, Index = i })
                                             .Where(x => x.FizickoLice.Id == (ObjectToPersist as FizickoLice).OtacId)
                                             .Select(x => x.Index as int?).FirstOrDefault();
            }
        }
    }
}
