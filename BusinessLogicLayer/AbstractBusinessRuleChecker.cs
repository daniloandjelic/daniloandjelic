using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    /// <summary>
    /// TODO: Add return messages to abstract method so that client app can use them.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractBusinessRuleChecker<T> : IBusinessLayerFacade<T> where T : class
    {

        public void Create(T objToCreate)
        {
            CheckBusinessRuleToCreate(objToCreate);
        }

        public void Update(T objToUpdate)
        {
            CheckBusinessRuleToUpdate(objToUpdate);
        }

        public void Delete(T objToDelete)
        {
            CheckBusinessRuleToDelete(objToDelete);
        }

        public abstract void CheckBusinessRuleToDelete(T objToDelete);

        public abstract void CheckBusinessRuleToUpdate(T objToUpdate);

        public abstract void CheckBusinessRuleToCreate(T objToCreate);
    }
}
