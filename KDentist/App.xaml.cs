using KDentist.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KDentist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //AppDomain.CurrentDomain.UnhandledException += ProcessUnhandledException;
            var dri = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/KDentist").FullName;
            var backup = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/KDentist/Backup").FullName;
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/KDentist/Documents");

            if (File.Exists(dri + "\\KDentistDB.mdf"))
            {
                File.Copy(dri + "\\KDentistDB.mdf", backup + "\\KDentistDB.mdf", true);
            }


            AppDomain.CurrentDomain.SetData("DataDirectory", dri);
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/KDentist/Photoes");
            Database.SetInitializer(
                     new MigrateDatabaseToLatestVersion<ApplicationDbContext, KDentist.Data.Migrations.Configuration>()
                     );
        }
        //private void ProcessUnhandledException(object o, UnhandledExceptionEventArgs e)
        //{
        //    MessageBox.Show("An unhandled exception has been thrown\n" + (e.ExceptionObject as Exception).ToString(),( e.ExceptionObject as Exception).ToString());
        //    Application.Current.Shutdown();
        //}
    }
}
