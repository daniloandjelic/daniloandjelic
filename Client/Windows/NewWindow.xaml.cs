using Client.Framework;
using Client.ViewModel;
using MasterEntities;
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
    /// Interaction logic for NewWindow.xaml
    /// </summary>
    public partial class NewWindow : Window
    {
        NewWindowViewModel view = new NewWindowViewModel();
        public NewWindow(object item)
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
                {
                    this.DataContext = view;
                    if (item != null)
                    {
                        (view.CurrentPageViewModel as PersistableViewModel).ObjectToPersist = item;

                        ////Uncomment for explicit setter of view's ObjectToPersist
                        //((view.CurrentPageViewModel as SwitchableViewModel).CurrentPageViewModel as PersistableViewModel).ObjectToPersist = item;
                    }
                };
            this.Unloaded += (s, e) =>
            {
                //this.DataContext = null;
                //Window.GetWindow(new MainWindow()).IsEnabled = true;
            };

        }
        //private Window _parent { get; set; }

        public NewWindow(object item, Window parent)
        {
            InitializeComponent();
            //_parent = parent;
            parent.IsEnabled = false;
            this.Loaded += (s, e) =>
                {
                    this.DataContext = view;
                    if (item != null)
                    {
                        (view.CurrentPageViewModel as PersistableViewModel).ObjectToPersist = item;
                        ((view.CurrentPageViewModel as PersistableViewModel).CurrentPageViewModel as PersistableViewModel).ObjectToPersist = item;

                        ////Uncomment for explicit setter of view's ObjectToPersist
                        //((view.CurrentPageViewModel as SwitchableViewModel).CurrentPageViewModel as PersistableViewModel).ObjectToPersist = item;
                    }
                };
            //this.Unloaded += (s, e) =>
            //    {
            //        parent.IsEnabled = true;
            //    };

        }
    }
}
