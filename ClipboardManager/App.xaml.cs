using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DBHandler;
using Microsoft.EntityFrameworkCore;

namespace ClipboardManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            using (Context dbContext = new Context())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
