using Common.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class GlobalVariables
    {
        public DatabaseConnection dbConnection { get; set; }
        public DataAccess dAccess { get; set; }

        private static GlobalVariables m_Instance;
        public static GlobalVariables Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new GlobalVariables();

                return m_Instance;
            }
        }

        public void SetVariables(DatabaseConnection DbConn, DataAccess dataAccess)
        {
            dbConnection = DbConn;
            dAccess = dataAccess;

            //Handler se poziva kada CLR pokusa da Load-uje .dll MasterEntities, i ne uspeva posto je Copy Local = false.
            //Event se poziva u trenutku pred pad aplikacije - resolve-uju se referencirani .dll-ovi.
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(currentDomain_AssemblyResolve);
        }

        Assembly currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly a = null;
            if (dbConnection == DatabaseConnection.MasterApp1)
                a = Assembly.LoadFrom("..\\..\\TPT\\MasterEntities.dll");
            else if (dbConnection == DatabaseConnection.MasterApp2)
                a = Assembly.LoadFrom("..\\..\\TPC\\MasterEntities.dll");
            else if (dbConnection == DatabaseConnection.MasterApp3)
                a = Assembly.LoadFrom("..\\..\\TPH\\MasterEntities.dll");

            return a;
        }

    }
}
