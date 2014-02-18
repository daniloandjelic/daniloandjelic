using Client.Commands;
using Client.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class MainWindowViewModel : SwitchablePage
    {
        public MainWindowViewModel(Window mainWindow)
        {
            PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new OsobaViewModel(mainWindow));
            CurrentPageViewModel = PageViewModels[0];
        }        
    }
}
