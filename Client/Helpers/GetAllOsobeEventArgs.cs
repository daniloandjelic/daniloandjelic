using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Helpers
{
    public class GetAllOsobeEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        public GetAllOsobeEventArgs(List<Osoba> lista, Exception error, bool cancelled) : base(error, cancelled, null)
        {
            Lista = lista;
        }

        public List<Osoba> Lista { get; set; }
    }
}
