using DataAccessLayer;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementations
{
    public class PravnoLiceBusinessLayerImplementation : IBusinessLayerFacade<PravnoLice>
    {

        public void Create(PravnoLice objToCreate)
        {
            if (objToCreate != null)
            {
                IGenericDataAccessLayer<Osoba> dal = new GenericDataAccessLayer<Osoba>();
                objToCreate.Id = dal.GetAll(null).Max(f => f.Id) + 1;
                dal.Create(objToCreate);
            }
        }

        public void Update(PravnoLice objToUpdate)
        {
            if (objToUpdate != null)
            {
                IGenericDataAccessLayer<PravnoLice> dal = new GenericDataAccessLayer<PravnoLice>();
                PravnoLice dbPl = dal.GetEntity(pl => pl.Id == objToUpdate.Id);
                if (dbPl != null)
                    dal.Update(EntityState.Modified, objToUpdate);
            }
        }

        public void Delete(PravnoLice objToDelete)
        {
            if (objToDelete != null)
            {
                IGenericDataAccessLayer<PravnoLice> dal = new GenericDataAccessLayer<PravnoLice>();
                PravnoLice dbPl = dal.GetEntity(pl => pl.Id == objToDelete.Id);
                if (dbPl != null)
                    dal.Delete(dbPl);
            }
        }
    }
}
