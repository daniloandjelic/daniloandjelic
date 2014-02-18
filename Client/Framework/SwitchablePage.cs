using Client.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Framework
{
    public class SwitchablePage : Client.ViewModel.ViewModelBase
    {
        private ICommand commandToSwitch;
        private IPageViewModel currentPageViewModel;
        private List<IPageViewModel> pageViewModels;

        public ICommand CommandToSwitch
        {
            get
            {
                if (commandToSwitch == null)
                    commandToSwitch = new DelegateCommand(page => ChangeViewModel((IPageViewModel)page), page => page is IPageViewModel);

                return commandToSwitch;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (pageViewModels == null)
                    pageViewModels = new List<IPageViewModel>();

                return pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get { return currentPageViewModel; }
            set
            {
                if (currentPageViewModel != value)
                {
                    currentPageViewModel = value;
                    this.OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

    }
}
