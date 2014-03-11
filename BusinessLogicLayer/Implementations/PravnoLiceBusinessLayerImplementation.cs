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
        IGenericDataAccessLayer<PravnoLice> dal = null;
        IGenericDataAccessLayer<IdentifikacioniDokument> identDal = null;

        public void Create(PravnoLice objToCreate)
        {
            if (objToCreate != null)
            {
                IGenericDataAccessLayer<Osoba> dalOsoba = new GenericDataAccessLayer<Osoba>();
                List<Osoba> returnList = dalOsoba.GetAll(null).ToList();
                long id = 1;

                if (returnList != null && returnList.Count() > 0)
                    id = returnList.Max(f => f.Id) + 1;

                identDal = new GenericDataAccessLayer<IdentifikacioniDokument>();

                long identifId = GenerateKey(objToCreate.IdentifikacioniDokument, identDal);
                objToCreate.IdentifikacioniDokumentId = identifId;
                objToCreate.IdentifikacioniDokument.Id = identifId;

                if (objToCreate.IdentifikacioniDokument.Mesto != null)
                {
                    IGenericDataAccessLayer<Mesto> mDal = new GenericDataAccessLayer<Mesto>();
                    Mesto mesto = mDal.GetEntity(m => m.Naziv == objToCreate.IdentifikacioniDokument.Mesto.Naziv, null);
                    if (mesto != null)
                        objToCreate.IdentifikacioniDokument.MestoIzdavanjaId = mesto.Id;

                    objToCreate.IdentifikacioniDokument.Mesto = null;
                    objToCreate.IdentifikacioniDokument.Osoba = null;
                }

                identDal.Create(objToCreate.IdentifikacioniDokument);
                objToCreate.IdentifikacioniDokument = null;

                objToCreate.Id = id;
                dalOsoba.Create(objToCreate);
            }
        }

        public void Update(PravnoLice objToUpdate)
        {
            if (objToUpdate != null)
            {
                

                if (objToUpdate.IdentifikacioniDokument != null)
                {
                    if (objToUpdate.IdentifikacioniDokument.Mesto != null && !string.IsNullOrEmpty(objToUpdate.IdentifikacioniDokument.Mesto.Naziv))
                    {
                        IGenericDataAccessLayer<Mesto> mDal = new GenericDataAccessLayer<Mesto>();
                        Mesto mesto = mDal.GetEntity(m => m.Naziv == objToUpdate.IdentifikacioniDokument.Mesto.Naziv, null);
                        if (mesto != null)
                            objToUpdate.IdentifikacioniDokument.MestoIzdavanjaId = mesto.Id;                       
                    }

                    objToUpdate.IdentifikacioniDokument.Mesto = null;
                    identDal = new GenericDataAccessLayer<IdentifikacioniDokument>();
                    IdentifikacioniDokument identDok = identDal.GetEntity(fl => fl.Id == objToUpdate.IdentifikacioniDokument.Id);
                    long identifId = objToUpdate.IdentifikacioniDokument.Id;

                    if (identDok != null)
                        identDal.Update(objToUpdate.IdentifikacioniDokument);
                    else // ovo treba izbrisati kada se obezbede podaci
                    {
                        identifId = GenerateKey(objToUpdate.IdentifikacioniDokument, identDal);
                        objToUpdate.IdentifikacioniDokumentId = identifId;
                        identDal.Create(objToUpdate.IdentifikacioniDokument);
                    }

                    objToUpdate.IdentifikacioniDokumentId = identifId;
                    objToUpdate.IdentifikacioniDokument = null;
                }
                dal = new GenericDataAccessLayer<PravnoLice>();
                PravnoLice dbPl = dal.GetEntity(pl => pl.Id == objToUpdate.Id);
                if (dbPl != null)
                    dal.Update(objToUpdate);
            }
        }

        private long GenerateKey(IdentifikacioniDokument identifikacioniDokument, IGenericDataAccessLayer<IdentifikacioniDokument> dal)
        {
            long id = 1;
            List<IdentifikacioniDokument> returnList = dal.GetAll(null).ToList();
            if (returnList != null && returnList.Count() > 0)
                id = returnList.Max(f => f.Id) + 1;
            return id;
        }

        public void Delete(PravnoLice objToDelete)
        {
            if (objToDelete != null)
            {
                if (objToDelete.IdentifikacioniDokument != null)
                {
                    identDal = new GenericDataAccessLayer<IdentifikacioniDokument>();
                    IdentifikacioniDokument ident = identDal.GetEntity(fl => fl.Id == objToDelete.IdentifikacioniDokumentId);
                    if (ident != null)
                        identDal.Delete(objToDelete.IdentifikacioniDokument);
                }
                dal = new GenericDataAccessLayer<PravnoLice>();
                PravnoLice dbPl = dal.GetEntity(pl => pl.Id == objToDelete.Id);
                if (dbPl != null)
                    dal.Delete(objToDelete);
            }
        }
    }
}
