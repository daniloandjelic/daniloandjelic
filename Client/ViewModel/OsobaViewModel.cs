using Client.Framework;
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
using System.Windows;

namespace Client.ViewModel
{
    public class OsobaViewModel : SwitchableViewModel, IPageViewModel
    {
        Assembly ass = null;

        public Window _mainWindow;

        public bool IsViewSelected = true;

        public OsobaViewModel(Window mainWindow)
        {
            PageViewModels.Add(new FizickoLiceListViewModel());
            PageViewModels.Add(new PravnoLiceListViewModel());
            ass = Assembly.Load("MasterEntities");
            this._mainWindow = mainWindow;
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
                SwitchViewModel(currentOsobaType+"List");
                (this.CurrentPageViewModel as SwitchableViewModel).RefreshCollectionOnPage();
            }
        }
       
    }
}
