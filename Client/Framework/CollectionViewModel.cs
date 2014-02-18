using Client.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Framework
{
    public abstract class CollectionViewModel : SwitchableViewModel
    {
        public const string deleteMessage = "Da li sigurno zelite da obrisete izabranu stavku?";

        #region Commands

        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand NewCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }

        #endregion

        #region Ctor

        public CollectionViewModel()
        {
            UpdateCommand = new DelegateCommand(UpdateCommand_Execute, UpdateCommand_CanExecute);
            DeleteCommand = new DelegateCommand(DeleteCommand_Execute, DeleteCommand_CanExecute);
            NewCommand = new DelegateCommand(NewCommand_Execute, NewCommand_CanExecute);
        }

        #endregion

        #region VirtualMethods - Command enable/disable

        public virtual bool NewCommand_CanExecute(object obj)
        {
            return obj != null;
        }

        public virtual bool UpdateCommand_CanExecute(object obj)
        {
            return this.SelectedItem != null;
        }

        public virtual bool DeleteCommand_CanExecute(object obj)
        {
            return this.SelectedItem != null;
        }

        #endregion

        #region AbstractMethods - Command execution to implement

        public abstract void NewCommand_Execute(object obj);        
        public abstract void UpdateCommand_Execute(object obj);
        public abstract void DeleteCommand_Execute(object obj);

        #endregion

        #region Properties to notify

        private object selectedItem;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                this.OnPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<object> collectionItems;
        public ObservableCollection<object> CollectionItems
        {
            get
            {
                return collectionItems;
            }
            set
            {
                collectionItems = value;
                this.OnPropertyChanged("CollectionItems");
            }
        }

        #endregion

        #region Methods

        public void AfterWindowClosing(Window child, Window parent)
        {
            child.Unloaded += (s, e) =>
            {
                parent.IsEnabled = true;
                child.DataContext = null;
                this.RefreshCollectionOnPage();
            };
        }

        #endregion
    }
}
