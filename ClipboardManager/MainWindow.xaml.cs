using DBHandler;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace ClipboardManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // hook for maximizing window from tray
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);

            ClipboardHandler clipHandler = new ClipboardHandler(this);
            clipHandler.ClipboardChanged += ClipboardUpdate;

            NotifyIcon.Icon = Properties.Resources.Clip;

            RenderClips();
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
        /// adds clipboard content to sqlite database
        /// move elsewhere?
        /// </summary>
        private void ClipboardUpdate(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                // make a separate handler for executing SQL commands later?
                using (Context dbContext = new Context())
                {
                    var query = "INSERT INTO Clip (content) VALUES (@content)";
                    var content = new SqliteParameter("@content", Clipboard.GetText());
                    dbContext.Database.ExecuteSqlCommand(query, content);
                    //clipContent.Text += Clipboard.GetText() + "\n";
                }
            }
        }

        /// <summary>
        /// renders clip database content on UI
        /// </summary>
        private void RenderClips()
        {
            using (Context dbContext = new Context())
            {
                var clips = dbContext.Clip.FromSql("SELECT * From Clip").ToList();
                ClipGrid.ItemsSource = clips;
            }
        }

        /// <summary>
        /// maximize window
        /// </summary>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == NativeMethods.WM_SHOWME)
            {
                Show();
                WindowState = WindowState.Normal;
            }
            return IntPtr.Zero;
        }
    }
}
