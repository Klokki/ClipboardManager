using System;
using System.Text;
using System.Windows;

namespace ClipboardManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder sbClip = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var clipHandler = new ClipboardHandler(this);
            clipHandler.ClipboardChanged += ClipboardUpdate;

            // minimizing the window using winforms NotifyIcon
            // maybe switch to WPF NotifyIcon later
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = Properties.Resources.Clip;
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    Show();
                    WindowState = WindowState.Normal;
                };
        }

        /// <summary>
        /// minimizing WPF form using winforms
        /// </summary>
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(e);
        }

        /// <summary>
        /// adds clipboard content to textblock via string builder
        /// </summary>
        private void ClipboardUpdate(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
                sbClip.AppendLine(Clipboard.GetText());

            clipContent.Text = sbClip.ToString();
        }
    }
}
