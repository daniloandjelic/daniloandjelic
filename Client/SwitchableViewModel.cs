using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public abstract class SwitchableViewModel : ViewModelBase
    {
        public void SwitchViewModel(string viewName)
        {
            CurrentPageViewModel = null;

            if (!string.IsNullOrEmpty(viewName))
            {
                IPageViewModel pvm = PageViewModels.Where(page => page.GetType().Name == viewName + "ViewModel").Select(page => page).FirstOrDefault() as IPageViewModel;

                if (pvm != null)
                    ChangeViewModel((IPageViewModel)pvm);
            }
        }

        private IPageViewModel currentPageViewModel;
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

        public List<IPageViewModel> pageViewModels;
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (pageViewModels == null)
                    pageViewModels = new List<IPageViewModel>();

                return pageViewModels;
            }
        }

    }
}
