using Client.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Framework
{
    public abstract class SubmitViewModel : PersistableViewModel
    {
        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand CloseWindowCommand { get; private set; }

        public SubmitViewModel()
        {
            SubmitCommand = new DelegateCommand(SubmitCommand_Execute, SubmitCommand_CanExecute);
            CloseWindowCommand = new DelegateCommand(CloseWindow_Execute, CloseWindow_CanExecute);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public virtual bool CloseWindow_CanExecute(object obj)
        {
            return (obj != null && obj is Window);
        }

        public virtual bool SubmitCommand_CanExecute(object obj)
        {
            return obj != null;
        }

        public virtual void CloseWindow_Execute(object obj)
        {
            Window w = obj as Window;
            if(w != null)
                w.Close();
        }

        public abstract void SubmitCommand_Execute(object obj);
    }
}
