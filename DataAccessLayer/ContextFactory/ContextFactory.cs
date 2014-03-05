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

        public DbContext GenerateContext()
        {
            if (m_DbConnection == DatabaseConnection.MasterApp1)
                return new MasterApp1Entities();
            else if (m_DbConnection == DatabaseConnection.MasterApp2)
                return new MasterApp2Entities();
            else if (m_DbConnection == DatabaseConnection.MasterApp3)
                return new MasterApp3Entities();

            return null;
        }
    }
}
