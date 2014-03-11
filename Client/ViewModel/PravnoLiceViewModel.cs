using BusinessLogicLayer;
using BusinessLogicLayer.Implementations;
using Client.Commands;
using Client.Framework;
using Client.Windows;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class PravnoLiceViewModel : SubmitViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Unos/izmena pravnog lica"; }
        }

        public override bool SubmitCommand_CanExecute(object obj)
        {
            return true;
        }

        public override void SubmitCommand_Execute(object obj)
        {
            if (IsValid)
            {
                PravnoLice pl = obj as PravnoLice;
                IBusinessLayerFacade<PravnoLice> bl = new PravnoLiceBusinessLayerImplementation();
                if (pl.Id != 0)
                    bl.Update(pl);
                else
                    bl.Create(pl);
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
                PravnoLice fl = obj as PravnoLice;

                if (fl != null)
                    fl.Slika = System.IO.File.ReadAllBytes(ImagePath);
            }
        }

        protected override string ValidateProperty(string columnName)
        {
            string ret = string.Empty;
            PravnoLice pl = ObjectToPersist as PravnoLice;
            switch (columnName)
            {
                case "IdentifikacioniBroj":
                    if (pl != null && string.IsNullOrEmpty(pl.IdentifikacioniBroj) || string.IsNullOrEmpty(IdentifikacioniBroj))
                        ret = "Unos identifikacionog broja pravnog lica je obavezan!";
                    break;
                case "Naziv":
                    if (pl != null && string.IsNullOrEmpty(pl.Naziv) || string.IsNullOrEmpty(Naziv))
                        ret = "Unos naziva pravnog lica je obavezan!!";
                    break;
                case "DatumOsnivanja":
                    if (pl != null && pl.DatumOsnivanja != null && (DateTime)pl.DatumOsnivanja > DateTime.Now)
                        ret = "Datum osnivanja mora biti pre današnjeg datuma!";
                    break;
            }

            return ret;
        }

        #region Binding properties

        private string naziv;
        public string Naziv
        {
            get
            {
                if (ObjectToPersist != null)
                    return (ObjectToPersist as PravnoLice).Naziv;
                else
                    return string.Empty;
            }
            set
            {
                naziv = value;
                (ObjectToPersist as PravnoLice).Naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        private string identifikacioniBroj;
        public string IdentifikacioniBroj
        {
            get
            {
                if (ObjectToPersist != null)
                    return (ObjectToPersist as PravnoLice).IdentifikacioniBroj;
                else
                    return string.Empty;
            }
            set
            {
                identifikacioniBroj = value;
                (ObjectToPersist as PravnoLice).IdentifikacioniBroj = value;
                OnPropertyChanged("IdentifikacioniBroj");
            }
        }

        private DateTime? datumOsnivanja;
        public DateTime? DatumOsnivanja
        {
            get
            {
                if (ObjectToPersist != null)
                    return (ObjectToPersist as PravnoLice).DatumOsnivanja;
                else
                    return null;
            }
            set
            {
                datumOsnivanja = value;
                (ObjectToPersist as PravnoLice).DatumOsnivanja = value;
                OnPropertyChanged("DatumOsnivanja");
                OnPropertyChanged("ObjectToPersist");
            }
        }

        #endregion
    }
}
