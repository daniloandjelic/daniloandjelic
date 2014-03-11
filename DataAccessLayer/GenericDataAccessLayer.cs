using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using DataAccessLayer.Factory;
using Common.Helpers;

namespace DataAccessLayer
{
    public class GenericDataAccessLayer<T> : IGenericDataAccessLayer<T> where T : class
    {
        IContextFactory contextFactory = new ContextFactory(GlobalVariables.Instance.dbConnection);

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IList<T> result = null;

            using (var contx = contextFactory.GenerateContext())
            {
                IQueryable<T> dbQuery = contx.Set<T>();

                if (navigationProperties != null)
                    foreach (Expression<Func<T, object>> prop in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(prop);

                result = dbQuery.AsNoTracking().ToList<T>();

            }
            return result;
        }

        public virtual IList<T> GetList(Func<T, bool> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            IList<T> result = null;

            using (var contx = contextFactory.GenerateContext())
            {
                IQueryable<T> dbQuery = contx.Set<T>();

                if (navigationProperties != null)
                    foreach (Expression<Func<T, object>> prop in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(prop);

                result = dbQuery.AsNoTracking().Where(where).ToList<T>();

            }
            return result;
        }


        public virtual T GetEntity(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T result = null;
            using (var contx = contextFactory.GenerateContext())
            {
                IQueryable<T> dbQuery = contx.Set<T>();

                if (navigationProperties != null)
                    foreach (Expression<Func<T, object>> prop in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(prop);

                result = dbQuery.AsNoTracking().FirstOrDefault(where);
            }
            return result;
        }

        public virtual void Create(T item)
        {
            using (var context = contextFactory.GenerateContext())
            {
                context.Entry(item).State = System.Data.EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Update(T item)
        {
            using (var context = contextFactory.GenerateContext())
            {
                context.Entry(item).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public virtual void Delete(T item)
        {
            using (var context = contextFactory.GenerateContext())
            {
                context.Entry(item).State = System.Data.EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
