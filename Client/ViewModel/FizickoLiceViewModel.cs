using MasterEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class FizickoLiceViewModel : ViewModelBase, IPageViewModel
    {
        
        private ObservableCollection<FizickoLice> listOfFizickaLica;
        public ObservableCollection<FizickoLice> ListOfFizickaLica
        {
            get 
            {
                return listOfFizickaLica;
            }
            set
            {
                listOfFizickaLica = value;
                this.OnPropertyChanged("ListOfFizickaLica");
            }
        }
        public string Name
        {
            get { return "Fizicko lice"; }
        }
    }
}
