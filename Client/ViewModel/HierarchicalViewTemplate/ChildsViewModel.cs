using Client.Framework;
using DataAccessLayer;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class ChildsViewModel : TreeViewItemViewModel
    {
        public FizickoLice Fl;
        public string CurrRecursiveType;
        private bool _childFirst;

        public ChildsViewModel(FizickoLice fl, string CurrentRecursiveType)
            : base(fl, true)
        {
            Fl = fl;
            CurrRecursiveType = CurrentRecursiveType;
        }

        public string ChildName
        {
            get { return Fl.Naziv; } 
        }

        protected override void LoadChildren()
        {
            GenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
            _childFirst = CurrRecursiveType == "ChildParent" ? true : false;

            if (_childFirst)
            {
                FizickoLice fll = dal.GetEntity(f => f.Id == Fl.Id, f => f.Otac, f => f.Majka);

                if (fll.Majka != null)
                    base.Children.Add(new ChildsViewModel(fll.Majka, CurrRecursiveType));

                if (fll.Otac != null)
                    base.Children.Add(new ChildsViewModel(fll.Otac, CurrRecursiveType));

            }
            else
            {
                List<FizickoLice> fllColl = dal.GetList(f => f.OtacId == Fl.Id || f.MajkaId == Fl.Id, null).ToList();

                if (fllColl != null && fllColl.Count() > 0)
                    foreach (FizickoLice item in fllColl)
                        base.Children.Add(new ChildsViewModel(item, CurrRecursiveType));
 
            }            

        }
    }
}
