using BusinessLogicLayer;
using BusinessLogicLayer.Implementations;
using Client.Commands;
using Client.Framework;
using Client.Windows;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class PravnoLiceViewModel : SubmitViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Unos/izmena pravnog lica"; }
        }

        public override bool SubmitCommand_CanExecute(object obj)
        {
            PravnoLice pl = obj as PravnoLice;
            if (pl != null)
                return true;
            return false;
        }

        public override void SubmitCommand_Execute(object obj)
        {
            PravnoLice pl = obj as PravnoLice;
            IBusinessLayerFacade<PravnoLice> bl = new PravnoLiceBusinessLayerImplementation();
            if (pl.Id != 0)
                bl.Update(pl);
            else
                bl.Create(pl);
        }

        public override void OpenDialog_Execute(object obj)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "Slike (.png)|*.png";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImagePath = dlg.FileName;
                PravnoLice fl = obj as PravnoLice;

                if (fl != null)
                    fl.Slika = System.IO.File.ReadAllBytes(ImagePath);
            }
        }
    }
}
