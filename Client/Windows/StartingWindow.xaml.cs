using Client.Framework;
using Client.ViewModel.WindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for StartingWindow.xaml
    /// </summary>
    public partial class StartingWindow : Window
    {
        SwitchablePage viewModel = null;

        public StartingWindow()
        {
            InitializeComponent();
            viewModel = new StartingWindowViewModel();
            this.Loaded += (s, e) =>
                {
                    this.DataContext = viewModel;
                };
        }
    }
}
