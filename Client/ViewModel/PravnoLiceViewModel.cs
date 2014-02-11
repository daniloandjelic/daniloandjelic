using MasterEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class PravnoLiceViewModel : ViewModelBase, IPageViewModel
    {
        private ObservableCollection<PravnoLice> listOfPravnaLica;
        public ObservableCollection<PravnoLice> ListOfPravnaLica
        {
            get
            {
                return listOfPravnaLica;
            }
            set
            {
                listOfPravnaLica = value;
                this.OnPropertyChanged("ListOfPravnaLica");
            }
 
        }

        public string Name
        {
            get { return "Pravna lica"; }
        }
    }
}
