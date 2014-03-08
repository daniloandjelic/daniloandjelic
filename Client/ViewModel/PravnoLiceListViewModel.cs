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
    public class PravnoLiceListViewModel : CollectionViewModel, IPageViewModel
    {
        public PravnoLiceListViewModel()
        {
            RefreshCollectionOnPage();
        }

        public override void NewCommand_Execute(object obj)
        {
            NewWindow nv = new NewWindow(new PravnoLice(), obj as Window);
            nv.Show();
            AfterWindowClosing(nv, obj as Window);
        }

        public override void DeleteCommand_Execute(object obj)
        {
            MessageBoxResult result = MessageBox.Show(deleteMessage, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                IBusinessLayerFacade<PravnoLice> flDelete = new PravnoLiceBusinessLayerImplementation();
                flDelete.Delete(obj as PravnoLice);
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

        public string Name
        {
            get { return "Pravna lica"; }
        }

        public override void RefreshCollectionOnPage()
        {
            GenericDataAccessLayer<PravnoLice> dal = new GenericDataAccessLayer<PravnoLice>();
            List<PravnoLice> collFl = dal.GetAll().ToList();
            ObservableCollection<object> oColl = new ObservableCollection<object>();
            collFl.ForEach(c => oColl.Add(c));
            CollectionItems = oColl;
        }
    }
}
