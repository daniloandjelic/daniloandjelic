using Common.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

    }
}
