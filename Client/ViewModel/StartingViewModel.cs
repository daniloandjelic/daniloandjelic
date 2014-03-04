using Client.Framework;
using Client.Windows;
using Client.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Helpers;

namespace Client.ViewModel
{
    public class StartingViewModel : SubmitViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Podešavanje konfiguracije aplicacije"; }
        }

        public override void SubmitCommand_Execute(object obj)
        {
            GlobalVariables.Instance.SetVariables(DatabaseConnection, DAL);
            MainWindow mw = new MainWindow();            
            CloseWindow_Execute(obj);
            mw.Show();
        }

        #region DatabaseConnections properties

        private DatabaseConnections databaseConnection;
        public DatabaseConnections DatabaseConnection
        {
            get { return this.databaseConnection; }
            set
            {
                if (this.databaseConnection == value)
                    return;

                this.databaseConnection = value;
                OnPropertyChanged("IsMasterApp1");
                OnPropertyChanged("IsMasterApp2");                
                OnPropertyChanged("IsMasterApp3");
            }
        }

        public bool IsMasterApp1
        {
            get { return DatabaseConnection == DatabaseConnections.MasterApp1; }
            set { DatabaseConnection = value ? DatabaseConnections.MasterApp1 : DatabaseConnection; }
        }

        public bool IsMasterApp2
        {
            get { return DatabaseConnection == DatabaseConnections.MasterApp2; }
            set { DatabaseConnection = value ? DatabaseConnections.MasterApp2 : DatabaseConnection; }
        }

        public bool IsMasterApp3
        {
            get { return DatabaseConnection == DatabaseConnections.MasterApp3; }
            set { DatabaseConnection = value ? DatabaseConnections.MasterApp3 : DatabaseConnection; }
        }
        #endregion


        #region DataAccessLayers properties

        private DataAccess dal;
        public DataAccess DAL
        {
            get { return this.dal; }
            set
            {
                if (this.dal == value)
                    return;

                this.dal = value;
                OnPropertyChanged("IsADO");
                OnPropertyChanged("IsEF");
            }
        }

        public bool IsADO
        {
            get { return dal == DataAccess.ADO; }
            set { DAL = value ? DataAccess.ADO : DAL; }
        }

        public bool IsEF
        {
            get { return dal == DataAccess.EntityFramework; }
            set { DAL = value ? DataAccess.EntityFramework : DAL; }
        }
        #endregion

    }

}
