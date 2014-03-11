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
        IGenericDataAccessLayer<FizickoLice> flDal = null;
        IGenericDataAccessLayer<IdentifikacioniDokument> dal = null;

        public override void CheckBusinessRuleToDelete(FizickoLice objToDelete)
        {
            if (objToDelete != null)
            {
                if (objToDelete.IdentifikacioniDokument != null)
                {
                    objToDelete.IdentifikacioniDokument.Mesto = null;
                    dal = new GenericDataAccessLayer<IdentifikacioniDokument>();
                    IdentifikacioniDokument ident = dal.GetEntity(fl => fl.Id == objToDelete.IdentifikacioniDokumentId);
                    if (ident != null)
                        dal.Delete(objToDelete.IdentifikacioniDokument);
                }
                flDal = new GenericDataAccessLayer<FizickoLice>();
                FizickoLice dbFl = flDal.GetEntity(fl => fl.Id == objToDelete.Id);
                if (dbFl != null)
                    flDal.Delete(objToDelete);
            }
        }

        public override void CheckBusinessRuleToUpdate(FizickoLice objToUpdate)
        {
            if (objToUpdate != null)
            {
                if (objToUpdate.IdentifikacioniDokument != null)
                {
                    if(objToUpdate.IdentifikacioniDokument.Mesto != null && !string.IsNullOrEmpty(objToUpdate.IdentifikacioniDokument.Mesto.Naziv))
                    {
                        IGenericDataAccessLayer<Mesto> mDal = new GenericDataAccessLayer<Mesto>();
                        Mesto mesto = mDal.GetEntity(m => m.Naziv == objToUpdate.IdentifikacioniDokument.Mesto.Naziv, null);
                        if(mesto != null)
                            objToUpdate.IdentifikacioniDokument.MestoIzdavanjaId = mesto.Id;

                        objToUpdate.IdentifikacioniDokument.Mesto = null;
                    }
                    dal = new GenericDataAccessLayer<IdentifikacioniDokument>();
                    IdentifikacioniDokument identDok = dal.GetEntity(fl => fl.Id == objToUpdate.IdentifikacioniDokument.Id);
                    long identifId = objToUpdate.IdentifikacioniDokument.Id;

                    objToUpdate.IdentifikacioniDokument.Osoba = null;
                    objToUpdate.IdentifikacioniDokument.Mesto = null;

                    if (identDok != null)
                        dal.Update(objToUpdate.IdentifikacioniDokument);
                    else // ovo treba izbrisati kada se obezbede podaci
                    {
                        identifId = GenerateKey(objToUpdate.IdentifikacioniDokument, dal);
                        objToUpdate.IdentifikacioniDokumentId = identifId;
                        dal.Create(objToUpdate.IdentifikacioniDokument);
                    }

                    objToUpdate.IdentifikacioniDokumentId = identifId;
                    objToUpdate.IdentifikacioniDokument = null;
                }

                flDal = new GenericDataAccessLayer<FizickoLice>();
                flDal.Update(objToUpdate);


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

        public override void CheckBusinessRuleToCreate(FizickoLice objToCreate)
        {
            if (objToCreate != null)
            {
                IGenericDataAccessLayer<Osoba> dalOsoba = new GenericDataAccessLayer<Osoba>();
                List<Osoba> returnList = dalOsoba.GetAll(null).ToList();
                long id = 1;

                if (returnList != null && returnList.Count() > 0)
                    id = returnList.Max(f => f.Id) + 1;

                dal = new GenericDataAccessLayer<IdentifikacioniDokument>();

                long identifId = GenerateKey(objToCreate.IdentifikacioniDokument, dal);
                objToCreate.IdentifikacioniDokumentId = identifId;
                objToCreate.IdentifikacioniDokument.Id = identifId;

                if (objToCreate.Otac != null)
                    objToCreate.OtacId = objToCreate.Otac.Id;

                if (objToCreate.Majka != null)
                    objToCreate.MajkaId = objToCreate.Majka.Id;

                objToCreate.Majka = null;
                objToCreate.Otac = null;

                if (objToCreate.IdentifikacioniDokument.Mesto != null)
                {
                    IGenericDataAccessLayer<Mesto> mDal = new GenericDataAccessLayer<Mesto>();
                    Mesto mesto = mDal.GetEntity(m => m.Naziv == objToCreate.IdentifikacioniDokument.Mesto.Naziv, null);
                    if (mesto != null)
                        objToCreate.IdentifikacioniDokument.MestoIzdavanjaId = mesto.Id;

                    objToCreate.IdentifikacioniDokument.Mesto = null;
                    objToCreate.IdentifikacioniDokument.Osoba = null;
                }

                dal.Create(objToCreate.IdentifikacioniDokument);
                objToCreate.IdentifikacioniDokument = null;

                objToCreate.Id = id;
                dalOsoba.Create(objToCreate);
            }
        }
    }
}
