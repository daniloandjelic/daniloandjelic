using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.ViewModel
{
    public class HomeViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Početna strana";
            }
        }
    }
}
