using Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class RecursiveViewModel : SwitchableViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Rekurzivne veze"; }
        }

        public string Icon
        {
            get
            {
                return "Images/circle.png";
            }
        }

        public RecursiveViewModel()
        {
            RefreshRecursiveDirectionsTypes();
        }

        private void RefreshRecursiveDirectionsTypes()
        {
            ObservableCollection<string> ls = new ObservableCollection<string>();
            ls.Add("ChildParent");
            ls.Add("ParentChild");
            RecursiveDirectionTypes = ls;
        }


        private ObservableCollection<string> recursiveDirectionTypes;
        public ObservableCollection<string> RecursiveDirectionTypes
        {
            get
            {
                return recursiveDirectionTypes;
            }

            set
            {
                recursiveDirectionTypes = value;
                OnPropertyChanged("RecursiveDirectionTypes");
            }

        }

        private string currentRecurisveType;
        public string CurrentRecursiveType
        {
            get
            {
                return currentRecurisveType;
            }

            set
            {
                currentRecurisveType = value;
                OnPropertyChanged("CurrentRecursiveType");
                AddPageViewModel(new ChildParentViewModel(CurrentRecursiveType));
                SwitchViewModel("ChildParent");
                
            }
        }

        private void AddPageViewModel(ChildParentViewModel childParentViewModel)
        {
            PageViewModels.RemoveAll(f => f is ChildParentViewModel);
            if (!PageViewModels.Contains(childParentViewModel))
            {
                DataAccessLayer.GenericDataAccessLayer<MasterEntities.FizickoLice> dal = new DataAccessLayer.GenericDataAccessLayer<MasterEntities.FizickoLice>();
                List<MasterEntities.FizickoLice> flList = dal.GetAll().ToList();
                PageViewModels.Add(new ChildParentViewModel(flList, this.CurrentRecursiveType));
            }
        }        

    }
}
