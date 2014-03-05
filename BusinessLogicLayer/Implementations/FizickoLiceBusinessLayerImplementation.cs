using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer.Implementations
{
    public class FizickoLiceBusinessLayerImplementation : AbstractBusinessRuleChecker<FizickoLice>
    {
        public override void CheckBusinessRuleToDelete(FizickoLice objToDelete)
        {
            if (objToDelete != null)
            {
                IGenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
                FizickoLice dbFl = dal.GetEntity(fl => fl.Id == objToDelete.Id);
                if (dbFl != null)
                    dal.Delete(objToDelete);
            }
        }

        public override void CheckBusinessRuleToUpdate(FizickoLice objToUpdate)
        {
            if (objToUpdate != null)
            {
                IGenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
                FizickoLice dbFl = dal.GetEntity(fl => fl.Id == objToUpdate.Id);
                if (dbFl != null)
                    dal.Update(EntityState.Modified, objToUpdate);
            }
        }

        public override void CheckBusinessRuleToCreate(FizickoLice objToCreate)
        {
            if (objToCreate != null)
            {
                IGenericDataAccessLayer<Osoba> dal = new GenericDataAccessLayer<Osoba>();
                List<Osoba> returnList = dal.GetAll(null).ToList();
                long id = 1;

                if(returnList != null && returnList.Count() > 0)
                    id = returnList.Max(f => f.Id) + 1;

                objToCreate.Id = id;
                dal.Create(objToCreate);
            }
        }
    }
}
