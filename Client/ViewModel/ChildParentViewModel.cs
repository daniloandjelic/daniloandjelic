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
    public class ChildParentViewModel : SwitchableViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Child ---> Parent View model"; }
        }

        public ChildParentViewModel()
        {
        }


        readonly ReadOnlyCollection<ChildsViewModel> fizickoLice;
        private string CurrentRecursiveType;
        private List<MasterEntities.FizickoLice> flList;
        private object p;

        public ChildParentViewModel(List<FizickoLice> flList)
        {
            fizickoLice = new ReadOnlyCollection<ChildsViewModel>(
                (from fl in flList
                 select new ChildsViewModel(fl, CurrentRecursiveType))
                .ToList());
        }

        public ChildParentViewModel(string CurrentRecursiveType)
        {
            // TODO: Complete member initialization
            this.CurrentRecursiveType = CurrentRecursiveType;
        }

        public ChildParentViewModel(List<MasterEntities.FizickoLice> flList, SwitchableViewModel p)
        {
            fizickoLice = new ReadOnlyCollection<ChildsViewModel>(
                (from fl in flList
                 select new ChildsViewModel(fl, (p as ChildParentViewModel).CurrentRecursiveType))
                .ToList());
        }


        public ChildParentViewModel(List<MasterEntities.FizickoLice> flList, string p)
        {
            fizickoLice = new ReadOnlyCollection<ChildsViewModel>(
                (from fl in flList
                 select new ChildsViewModel(fl, p))
                .ToList());
        }

        public ReadOnlyCollection<ChildsViewModel> FizickoLice
        {
            get { return fizickoLice; }
        }

        public override void RefreshCollectionOnPage()
        {
            GenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
            List<FizickoLice> flList = dal.GetAll().ToList();
            ChildParentViewModel testViewModel = new ChildParentViewModel(flList);
        }

    }

}
