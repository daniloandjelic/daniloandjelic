using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;

namespace BusinessLogicLayer
{
    public class BusinessLayerImplementation : AbstractBusinessRuleChecker<FizickoLice>
    {
        public override void CheckBusinessRuleToDelete(FizickoLice objToDelete)
        {
            throw new NotImplementedException();
        }

        public override void CheckBusinessRuleToUpdate(FizickoLice objToUpdate)
        {
            if (objToUpdate != null)
            {
                //if exists
                IGenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
                FizickoLice dbFl = dal.GetEntity(fl => fl.Id == objToUpdate.Id);
                if (dbFl != null)
                    dal.Update(EntityState.Modified, objToUpdate);
            }
        }

        public override void CheckBusinessRuleToCreate(FizickoLice objToCreate)
        {
            throw new NotImplementedException();
        }
    }
}
