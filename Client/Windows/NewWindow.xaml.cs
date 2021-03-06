﻿using Client.Framework;
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
        }

        public NewWindow(object item, Window parent)
        {
            InitializeComponent();
            parent.IsEnabled = false;
            this.Loaded += (s, e) =>
                {
                    this.DataContext = view;
                    if (item != null)
                    {
                        if (item is FizickoLice)
                            (view.CurrentPageViewModel as NewUpdateViewModel).PageViewModels.Add(new FizickoLiceViewModel());
                        else if (item is PravnoLice)
                            (view.CurrentPageViewModel as NewUpdateViewModel).PageViewModels.Add(new PravnoLiceViewModel());

                        (view.CurrentPageViewModel as NewUpdateViewModel).PageViewModels.Add(new IdentifikacioniDokumentViewModel());

                        Osoba os = item as Osoba;
                        if (os != null && os.IdentifikacioniDokument == null)
                            (item as Osoba).IdentifikacioniDokument = new IdentifikacioniDokument();

                        if (os.IdentifikacioniDokument.Mesto == null)
                            (item as Osoba).IdentifikacioniDokument.Mesto = new Mesto();

                        (view.CurrentPageViewModel as PersistableViewModel).ObjectToPersist = item;
                        foreach (var p in (view.CurrentPageViewModel as PersistableViewModel).PageViewModels)
                        {
                            PersistableViewModel pers = p as PersistableViewModel;
                            if (pers != null)
                                pers.ObjectToPersist = item;
                            
                        }
                        //((view.CurrentPageViewModel as PersistableViewModel).CurrentPageViewModel as PersistableViewModel).ObjectToPersist = item;
                        if (item is FizickoLice)
                        {
                            
                            FizickoLiceViewModel flmv = (view.CurrentPageViewModel as PersistableViewModel).CurrentPageViewModel as FizickoLiceViewModel;
                            if (flmv != null)
                            {
                                flmv.RefreshOceviCollection();
                                flmv.RefreshMajkeCollection();
                            }
                        }
                    }
                };
        }
    }
}
