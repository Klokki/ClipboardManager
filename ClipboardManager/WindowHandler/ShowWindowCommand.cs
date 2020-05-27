using System;
using System.Windows.Input;

namespace ClipboardManager
{
    /// <summary>
    /// broadcast WM_SHOWME to NativeMethods
    /// </summary>
    class ShowWindowCommand : ICommand
    {
        public void Execute(object parameter)
        {

            NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        // add {} remove {} to suppress warning
        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}
