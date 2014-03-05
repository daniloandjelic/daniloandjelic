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

            using (var contx = new MasterApp1Entities())
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

        public virtual void Create(params T[] objectsToCreate)
        {
            Update(EntityState.Added, objectsToCreate);
        }

        public virtual void Update(EntityState entityState, params T[] objectsToUpdate)
        {
            using (var ctx = contextFactory.GenerateContext())
            {
                DbSet<T> dbSet = ctx.Set<T>();

                foreach (T ob in objectsToUpdate)
                {
                    dbSet.Add(ob);
                    foreach (DbEntityEntry<T> item in ctx.ChangeTracker.Entries<T>())
                    {
                        if (item != null)
                            item.State = entityState;
                    }
                }
                ctx.SaveChanges();
            }
        }

        public virtual void Delete(params T[] objectsToDelete)
        {
            Update(EntityState.Deleted, objectsToDelete);
        }
    }
}
