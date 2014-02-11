using Client.Helpers;
using DataAccessLayer;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class OsobaViewModel : SwitchableViewModel, IPageViewModel
    {
        Assembly ass = null;
        public OsobaViewModel()
        {
            PageViewModels.Add(new FizickoLiceViewModel());
            PageViewModels.Add(new PravnoLiceViewModel());
            ass = Assembly.Load("MasterEntities");
            RefreshOsobaTypes();
        }


        private void RefreshOsobaTypes()
        {
            Type[] ttt = ass.GetTypes().Where(t => t.IsSubclassOf(typeof(Osoba))).Select(t => t).ToArray();
            ObservableCollection<string> coll = new ObservableCollection<string>();
            ttt.Select(t => t.Name).Distinct().ToList().ForEach(t => coll.Add(t.ToString()));
            this.OsobaTypes = coll;
        }

        public string Name
        {
            get { return "Unos/Izmena osoba"; }
        }

        private ObservableCollection<string> osobaTypes;
        public ObservableCollection<string> OsobaTypes
        {
            get
            {
                return osobaTypes;
            }

            set
            {
                osobaTypes = value;
                OnPropertyChanged("OsobaTypes");
            }

        }

        private string currentOsobaType;
        public string CurrentOsobaType
        {
            get
            {
                return currentOsobaType;
            }
            set
            {
                currentOsobaType = value;
                OnPropertyChanged("CurrentOsobaType");
                SwitchViewModel(currentOsobaType);
            }
        }

        private ObservableCollection<FizickoLice> GetAllFizickaLica()
        {
            GenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
            List<FizickoLice> collFl = dal.GetAll().ToList();
            ObservableCollection<FizickoLice> oColl = new ObservableCollection<FizickoLice>();
            collFl.ForEach(c => oColl.Add(c));
            return oColl;
        }

        private ObservableCollection<PravnoLice> GetAllPravnaLica()
        {
            GenericDataAccessLayer<PravnoLice> dal = new GenericDataAccessLayer<PravnoLice>();
            List<PravnoLice> collFl = dal.GetAll().ToList();
            ObservableCollection<PravnoLice> oColl = new ObservableCollection<PravnoLice>();
            collFl.ForEach(c => oColl.Add(c));
            return oColl;
        }

        public override void FillChildViews(IPageViewModel CurrentPageViewModel)
        {
            if (CurrentPageViewModel is FizickoLiceViewModel)
                (CurrentPageViewModel as FizickoLiceViewModel).ListOfFizickaLica = GetAllFizickaLica();
            else if (CurrentPageViewModel is PravnoLiceViewModel)
                (CurrentPageViewModel as PravnoLiceViewModel).ListOfPravnaLica = GetAllPravnaLica();
        }
    }
}
