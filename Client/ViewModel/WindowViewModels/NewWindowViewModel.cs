using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class NewWindowViewModel : SwitchablePage
    {
        public NewWindowViewModel()
        {
            //PageViewModels.Add(new FizickoLiceViewModel());
            //PageViewModels.Add(new PravnoLiceViewModel());
            PageViewModels.Add(new NewUpdateViewModel());
            CurrentPageViewModel = PageViewModels[0];
        }
    }
}
