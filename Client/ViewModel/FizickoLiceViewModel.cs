using BusinessLogicLayer;
using Client.Commands;
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
    public class FizickoLiceViewModel : ViewModelBase, IPageViewModel
    {

        public FizickoLiceViewModel()
        {
            RefreshAllFizickaLica();
            UpdateCommand = new DelegateCommand(UpdateCommand_Execute, UpdateCommand_CanExecute);
        }


        public DelegateCommand UpdateCommand { get; private set; }              

        private bool UpdateCommand_CanExecute(object obj)
        {
            return this.SelectedFizickoLice != null;
        }

        private void UpdateCommand_Execute(object obj)
        {
            if (obj != null && obj is FizickoLice)
            {
                IBusinessLayerFacade<FizickoLice> flBusiness = new BusinessLayerImplementation();
                flBusiness.Update(obj as FizickoLice);
            }
        }

        private void RefreshAllFizickaLica()
        {
            ListOfFizickaLica = GetAllFizickaLica();
        }
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

        private FizickoLice selectedFizickoLice;
        public FizickoLice SelectedFizickoLice
        {
            get
            {
                return selectedFizickoLice;
            }
            set
            {
                selectedFizickoLice = value;
                this.OnPropertyChanged("SelectedFizickoLice");
            }
        }

        public string Name
        {
            get { return "Fizicko lice"; }
        }

        private ObservableCollection<FizickoLice> GetAllFizickaLica()
        {
            GenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
            List<FizickoLice> collFl = dal.GetAll().ToList();
            ObservableCollection<FizickoLice> oColl = new ObservableCollection<FizickoLice>();
            collFl.ForEach(c => oColl.Add(c));
            return oColl;
        }
    }
}
