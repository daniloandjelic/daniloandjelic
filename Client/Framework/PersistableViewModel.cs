using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Framework
{
    public class PersistableViewModel : SwitchableViewModel
    {
        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }
        private object objectToPersist;
        public object ObjectToPersist
        {
            get
            {
                return objectToPersist;
            }
            set
            {
                objectToPersist = value;
                this.OnPropertyChanged("ObjectToPersist");
                if (objectToPersist != null)
                    this.SwitchViewModel(objectToPersist.GetType().Name.Contains("_") ? objectToPersist.GetType().Name.Substring(0,objectToPersist.GetType().Name.IndexOf("_")) : objectToPersist.GetType().Name);
            }
        }
    }
}
