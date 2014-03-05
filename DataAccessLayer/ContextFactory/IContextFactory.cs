using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Factory
{
    public interface IContextFactory
    {
        DbContext GenerateContext();
    }
}
