using Client.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand commandToSwitch;
        private IPageViewModel currentPageViewModel;
        private List<IPageViewModel> pageViewModels;

        public MainWindowViewModel()
        {
            PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new OsobaViewModel());
            CurrentPageViewModel = pageViewModels[0];
        }


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
