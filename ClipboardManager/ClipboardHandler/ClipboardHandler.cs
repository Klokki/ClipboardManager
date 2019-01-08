using System;
using System.Windows;
using System.Windows.Interop;

namespace ClipboardManager
{
    public class ClipboardHandler
    {
        public event EventHandler ClipboardChanged;

        public ClipboardHandler(Window windowSource)
        {
            HwndSource src = PresentationSource.FromVisual(windowSource) as HwndSource;

            src.AddHook(this.WndProc);

            IntPtr windowHandle = new WindowInteropHelper(windowSource).Handle;
            NativeMethods.AddClipboardFormatListener(windowHandle);
        }

        private void OnClipboardChanged()
        {
            ClipboardChanged?.Invoke(this, EventArgs.Empty);
        }

        private static readonly IntPtr WndProcSuccess = IntPtr.Zero;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == NativeMethods.WM_CLIPBOARDUPDATE)
            {
                this.OnClipboardChanged();
                handled = true;
            }

            return WndProcSuccess;
        }
    }
}
