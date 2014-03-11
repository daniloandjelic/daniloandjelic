using Client.Framework;
using DataAccessLayer;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class IdentifikacioniDokumentViewModel : PersistableViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Unos/Izmena identifikacionog dokumenta"; }
        }

        public IdentifikacioniDokumentViewModel()
        {
            RefreshMesta();
        }

        private void RefreshMesta()
        {
            IGenericDataAccessLayer<Mesto> dal = new GenericDataAccessLayer<Mesto>();
            MestaIzdavanjaDokumenata = new ObservableCollection<Mesto>(dal.GetAll());
        }

        private ObservableCollection<Mesto> mestaIzdavanjaDokumenata;
        public ObservableCollection<Mesto> MestaIzdavanjaDokumenata
        {
            get
            {
                return mestaIzdavanjaDokumenata;
            }
            set
            {
                mestaIzdavanjaDokumenata = value;
                OnPropertyChanged("MestaIzdavanjaDokumenata");
            }
        }

        private Mesto mestoIzdavanja;
        public Mesto MestoIzdavanja
        {
            get
            {
                if (ObjectToPersist != null && (ObjectToPersist as Osoba).IdentifikacioniDokument != null
                    && (ObjectToPersist as Osoba).IdentifikacioniDokument.Mesto != null
                    && !string.IsNullOrEmpty((ObjectToPersist as Osoba).IdentifikacioniDokument.Mesto.Naziv))
                {
                    mestoIzdavanja = (ObjectToPersist as Osoba).IdentifikacioniDokument.Mesto;
                }

                if (MestaIzdavanjaDokumenata != null && mestoIzdavanja != null)
                    SelIndexMesto = MestaIzdavanjaDokumenata.Select(m => m.Id).ToList().IndexOf(mestoIzdavanja.Id);

                return mestoIzdavanja;
            }
            set
            {
                mestoIzdavanja = value;
                if((ObjectToPersist as Osoba).IdentifikacioniDokument.Mesto == null)
                    (ObjectToPersist as Osoba).IdentifikacioniDokument.Mesto = new Mesto();

                (ObjectToPersist as Osoba).IdentifikacioniDokument.Mesto.Naziv = (value as Mesto).Naziv;
                OnPropertyChanged("MestoIzdavanja");
            }
        }
        private Client.Enums.ClientEnums.TipIdentifikacionogDokumenta naziv;
        public Client.Enums.ClientEnums.TipIdentifikacionogDokumenta Naziv
        {
            get
            {
                if (ObjectToPersist != null && (ObjectToPersist as Osoba).IdentifikacioniDokument != null && !string.IsNullOrEmpty((ObjectToPersist as Osoba).IdentifikacioniDokument.Naziv))
                    SelIndexTip = TipoviDokumenata.IndexOf((Client.Enums.ClientEnums.TipIdentifikacionogDokumenta)Enum.Parse(typeof(Client.Enums.ClientEnums.TipIdentifikacionogDokumenta), (ObjectToPersist as Osoba).IdentifikacioniDokument.Naziv));

                return naziv;
            }
            set
            {
                naziv = value;
                (ObjectToPersist as Osoba).IdentifikacioniDokument.Naziv = value.ToString();
                OnPropertyChanged("Naziv");
                OnPropertyChanged("ObjectToPersist");
            }
        }

        private ObservableCollection<Client.Enums.ClientEnums.TipIdentifikacionogDokumenta> tipoviDokumenata;
        public ObservableCollection<Client.Enums.ClientEnums.TipIdentifikacionogDokumenta> TipoviDokumenata
        {
            get
            {
                return new ObservableCollection<Client.Enums.ClientEnums.TipIdentifikacionogDokumenta>(Enum.GetValues(typeof(Client.Enums.ClientEnums.TipIdentifikacionogDokumenta)).Cast<Client.Enums.ClientEnums.TipIdentifikacionogDokumenta>());
            }
            set
            {
                tipoviDokumenata = value;
                OnPropertyChanged("TipoviDokumenata");
            }

        }

        private int? selIndexMesto = null;
        public int? SelIndexMesto
        {
            get
            {
                return selIndexMesto;
            }
            set
            {
                selIndexMesto = value;
                OnPropertyChanged("SelIndexMesto");
            }
        }


        private int? selIndexTip = null;
        public int? SelIndexTip
        {
            get
            {
                return selIndexTip;
            }
            set
            {
                selIndexTip = value;
                OnPropertyChanged("SelIndexTip");
            }
        }
    }
}
