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
        readonly FizickoLice _fl;
        private bool _childFirst;
        private string CurrentRecursiveType;

        public ChildsViewModel(FizickoLice fl, bool childFirst)
            : base(null, true)
        {
            _fl = fl;
            _childFirst = childFirst;
        }

        public ChildsViewModel(FizickoLice fl, string CurrentRecursiveType)
            : base(null, true)
        {
            _fl = fl;
            this.CurrentRecursiveType = CurrentRecursiveType;
        }

        public string ChildName
        {
            get { return _fl.Naziv; } 
        }

        protected override void LoadChildren()
        {
            GenericDataAccessLayer<FizickoLice> dal = new GenericDataAccessLayer<FizickoLice>();
            _childFirst = CurrentRecursiveType == "ChildParent" ? true : false;

            if (_childFirst)
            {
                FizickoLice fll = dal.GetEntity(f => f.Id == _fl.Id, f => f.Otac, f => f.Majka);

                if (fll.Majka != null)
                    base.Children.Add(new ChildsViewModel(fll.Majka, _childFirst));

                if (fll.Otac != null)
                    base.Children.Add(new ChildsViewModel(fll.Otac, _childFirst));

            }
            else
            {
                List<FizickoLice> fllColl = dal.GetList(f => f.OtacId == _fl.Id || f.MajkaId == _fl.Id, null).ToList();

                if (fllColl != null && fllColl.Count() > 0)
                    foreach (FizickoLice item in fllColl)
                        base.Children.Add(new ChildsViewModel(item, _childFirst));
 
            }            

        }
    }
}
