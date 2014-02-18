using DataAccessLayer;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Helpers
{
    public class EventFactory
    {

        public EventFactory()
        {
            InitializeCallbacks();
        }

        private void InitializeCallbacks()
        {
            GetAllOsobeCompletedCallback = new SendOrPostCallback(InternalGetAllOsobeCompleted);
        }

        private delegate void GetAllOsobeCallback(AsyncOperation asyncOperation);

        public event EventHandler<GetAllOsobeEventArgs> GetAllOsobeCompleted;

        private SendOrPostCallback GetAllOsobeCompletedCallback;
 
        protected virtual void OnGetAllOsobeCompleted(GetAllOsobeEventArgs e)
        {
            EventHandler<GetAllOsobeEventArgs> handler = GetAllOsobeCompleted;

            if(handler != null)
                handler(this, e);
        }

        private void InternalGetAllOsobeCompleted(object state)
        {
            GetAllOsobeEventArgs e = (GetAllOsobeEventArgs)state;
            OnGetAllOsobeCompleted(e);
        }

        public void GetAllOsobeAsync()
        {
            AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(null);
            GetAllOsobeCallback callback = new GetAllOsobeCallback(InternalGetAllOsobeAsync);
            callback.BeginInvoke(asyncOperation, null, null); 
        }

        private void InternalGetAllOsobeAsync(AsyncOperation asyncOperation)
        {
            List<Osoba> lista = InternalGetAllOsobe();

            GetAllOsobeEventArgs e = new GetAllOsobeEventArgs(lista, null, false);
            asyncOperation.PostOperationCompleted(GetAllOsobeCompletedCallback, e);
        }

        private List<Osoba> InternalGetAllOsobe()
        {
            GenericDataAccessLayer<Osoba> dal = new GenericDataAccessLayer<Osoba>();
            return dal.GetAll(null).ToList();
        }
    }
}
