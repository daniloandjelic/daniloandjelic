using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBusinessLayerFacade<T> where T : class
    {
        void Create(T objToCreate);
        void Update(T objToUpdate);
        void Delete(T objToDelete);
    }
}
