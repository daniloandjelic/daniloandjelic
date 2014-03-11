using Common.Helpers.Enums;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Factory
{
    public class ContextFactory : IContextFactory
    {
        DatabaseConnection m_DbConnection;

        public ContextFactory(DatabaseConnection dbConnection)
        {
            m_DbConnection = dbConnection;
        }

        DbContext result = null;

        public DbContext GenerateContext()
        {
            if (m_DbConnection == DatabaseConnection.MasterApp1)
                result =  new MasterApp1Entities();
            else if (m_DbConnection == DatabaseConnection.MasterApp2)
                result = new MasterApp2Entities();
            else if (m_DbConnection == DatabaseConnection.MasterApp3)
                result = new MasterApp3Entities();

            result.Configuration.LazyLoadingEnabled = false;

            return result;
        }
    }
}
