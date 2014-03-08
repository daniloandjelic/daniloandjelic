using Client.Framework;
using Client.ViewModel;
using MasterEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Framework
{
    public class TreeViewItemViewModel : ViewModelBase
    {
        #region Data

        static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();

        readonly ObservableCollection<TreeViewItemViewModel> _children;
        FizickoLice _parent;

        bool _isExpanded;
        bool _isSelected;

        #endregion 

        #region Constructors

        protected TreeViewItemViewModel(FizickoLice parent, bool lazyLoadChildren)
        {
            _parent = parent;

            _children = new ObservableCollection<TreeViewItemViewModel>();

            if (lazyLoadChildren)
                _children.Add(DummyChild);
        }

        private TreeViewItemViewModel()
        {
        }

        #endregion 

        #region Presentation Members

        #region Children


        public ObservableCollection<TreeViewItemViewModel> Children
        {
            get { return _children; }
        }

        #endregion // Children

        #region HasLoadedChildren


        public bool HasDummyChild
        {
            get { return this.Children.Count == 1 && this.Children[0] == DummyChild; }
        }

        #endregion // HasLoadedChildren

        #region IsExpanded

 
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }


                //if (_isExpanded && _parent != null)
                //    _parent.IsExpanded = true;

                if (this.HasDummyChild)
                {
                    this.Children.Remove(DummyChild);
                    this.LoadChildren();
                }
            }
        }

        #region IsSelected

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region LoadChildren

        protected virtual void LoadChildren()
        {
        }

        #endregion 

        #region Parent

        public FizickoLice Parent
        {
            get { return _parent; }

            set 
            { 
                _parent = value;
                OnPropertyChanged("Parent");
            }
        }

        #endregion // Parent
       

        #endregion // Presentation Members

        #endregion

    }
}


