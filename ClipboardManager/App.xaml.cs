using DBHandler;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace ClipboardManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            using (Context dbContext = new Context())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
