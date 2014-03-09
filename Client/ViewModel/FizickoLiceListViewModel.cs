using BusinessLogicLayer;
using BusinessLogicLayer.Implementations;
using Client.Commands;
using Client.Framework;
using Client.Windows;
using DataAccessLayer;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    public class FizickoLiceListViewModel : CollectionViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Fizicko lice"; }
        }

        public FizickoLiceListViewModel()
        {
            RefreshCollectionOnPage();            
        }

        public override void NewCommand_Execute(object obj)
        {
            NewWindow nv = new NewWindow(new FizickoLice(), obj as Window);
            nv.Show();
            AfterWindowClosing(nv, obj as Window);
        }

        public override void DeleteCommand_Execute(object obj)
        {
            MessageBoxResult result = MessageBox.Show(deleteMessage, "Potvrdite svoju akciju", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                IBusinessLayerFacade<FizickoLice> flDelete = new FizickoLiceBusinessLayerImplementation();
                flDelete.Delete(obj as FizickoLice);
            }
            this.RefreshCollectionOnPage();
        }

        public override void UpdateCommand_Execute(object obj)
        {
            OsobaViewModel ovm = obj as OsobaViewModel;
            if (ovm != null && SelectedItem != null && ovm._mainWindow != null)
            {
                NewWindow nv = new NewWindow(SelectedItem, ovm._mainWindow);
                nv.Show();
                AfterWindowClosing(nv, ovm._mainWindow as Window);
            }
        }

        public override void RefreshCollectionOnPage()
        {
            GenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
            List<FizickoLice> collFl = dal.GetAll(f => f.Majka, f => f.Otac).ToList();//dal.GetAll().ToList();//
            CollectionItems = new ObservableCollection<object>(collFl);
        }

    }
}
