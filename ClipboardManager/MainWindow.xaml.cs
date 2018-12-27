using System;
using System.Windows;

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

            var clipHandler = new ClipboardHandler(this);
            clipHandler.ClipboardChanged += ClipboardUpdate;
        }

        private void ClipboardUpdate(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                Console.WriteLine(Clipboard.GetText());
            }
        }
    }
}
