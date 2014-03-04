using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel.WindowViewModels
{
    class StartingWindowViewModel : SwitchablePage
    {
        public StartingWindowViewModel()
        {
            PageViewModels.Add(new StartingViewModel());
            CurrentPageViewModel = PageViewModels[0];
        }
    }
}
