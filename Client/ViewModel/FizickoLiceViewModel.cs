using BusinessLogicLayer;
using BusinessLogicLayer.Implementations;
using Client.Commands;
using Client.Framework;
using Client.Windows;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class FizickoLiceViewModel : SubmitViewModel, IPageViewModel
    {
        public string Name
        {
            get { return "Unos novog Fizickog lica"; }
        }

        public override bool SubmitCommand_CanExecute(object obj)
        {
            FizickoLice fl = obj as FizickoLice;
            if (fl != null)
                return true;
            return false;
        }

        public override void SubmitCommand_Execute(object obj)
        {
            // TODO - Pozvati u drugom thread-u da vrti progress bar
            // Treba dodati u DAL da vraca poruku/bool uspesnosti operacije pa na nju da se bindujem za progress bar
            // kada zavrsi progress bar, onda pozvati CloseWindow
            FizickoLice fl = obj as FizickoLice;
            IBusinessLayerFacade<FizickoLice> bl = new FizickoLiceBusinessLayerImplementation();
            if (fl.Id != 0)
                bl.Update(fl);
            else
                bl.Create(fl);
        }

    }
}
