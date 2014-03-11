using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IGenericDataAccessLayer<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetEntity(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Create(T objectToCreate);
        void Update(T objectToUpdate);
        void Delete(T objectToDelete);
    }
}
