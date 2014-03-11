using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class NewUpdateViewModel : PersistableViewModel, IPageViewModel
    {

        public NewUpdateViewModel()
        {
            //this.PageViewModels.Add(new FizickoLiceViewModel());
            //this.PageViewModels.Add(new PravnoLiceViewModel());
        }
        public string Name
        {
            get { return string.Empty; }
        }
    }
}
