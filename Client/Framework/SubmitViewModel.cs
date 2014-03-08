using Client.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Framework
{
    public abstract class SubmitViewModel : PersistableViewModel, IDataErrorInfo
    {
        protected bool _SubmitAttempted = false;
        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand CloseWindowCommand { get; private set; }
        public DelegateCommand OpenFileDialogCommand { get; private set; }

        public SubmitViewModel()
        {
            SubmitCommand = new DelegateCommand(SubmitCommand_Execute, SubmitCommand_CanExecute);
            CloseWindowCommand = new DelegateCommand(CloseWindow_Execute, CloseWindow_CanExecute);
            OpenFileDialogCommand = new DelegateCommand(OpenDialog_Execute, OpenDialog_CanExecute);
        }

        private bool OpenDialog_CanExecute(object obj)
        {
            return obj != null;
        }

        public abstract void OpenDialog_Execute(object obj);

        [System.Diagnostics.DebuggerStepThrough]
        public virtual bool CloseWindow_CanExecute(object obj)
        {
            var element = System.Windows.Media.VisualTreeHelper.GetParent(obj as System.Windows.Window) as System.Windows.UIElement; 
            return obj != null && obj is Window && IsValid;
        }

        public virtual bool SubmitCommand_CanExecute(object obj)
        {
            return _InvalidFields != null && _InvalidFields.Count() == 0;
        }

        public virtual void CloseWindow_Execute(object obj)
        {
            Window w = obj as Window;
            if (w != null)
                w.Close();
        }

        public abstract void SubmitCommand_Execute(object obj);

        ObservableCollection<string> _InvalidFields = new ObservableCollection<string>();

        public ObservableCollection<string> InvalidFields
        {
            get { return _InvalidFields; }
        }

        public bool IsValid
        {
            get { return (_InvalidFields.Count == 0); }
        }

        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string ret = ValidateProperty(columnName);

                if (ret != string.Empty)
                {
                    if (!InvalidFields.Contains(columnName))
                        InvalidFields.Add(columnName);
                }
                else
                {
                    if (InvalidFields.Contains(columnName))
                        InvalidFields.Remove(columnName);
                }

                if (!_SubmitAttempted)
                    ret = string.Empty;

                return ret;
            }
        }

        protected virtual string ValidateProperty(string columnName)
        {
            return string.Empty;
        }
    }
}
