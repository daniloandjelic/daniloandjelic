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
    public class PravnoLiceViewModel : ViewModelBase, IPageViewModel
    {

        public PravnoLiceViewModel()
        {
            RefreshPravnaLica();
        }

        private void RefreshPravnaLica()
        {
            ListOfPravnaLica = GetAllPravnaLica();
        }
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


        private ObservableCollection<PravnoLice> GetAllPravnaLica()
        {
            GenericDataAccessLayer<PravnoLice> dal = new GenericDataAccessLayer<PravnoLice>();
            List<PravnoLice> collFl = dal.GetAll().ToList();
            ObservableCollection<PravnoLice> oColl = new ObservableCollection<PravnoLice>();
            collFl.ForEach(c => oColl.Add(c));
            return oColl;
        }
    }
}
