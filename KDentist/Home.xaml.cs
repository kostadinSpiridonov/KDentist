using KDentist.Data;
using KDentist.ViewModels;
using KDentist.Views.ActivityViews;
using KDentist.Views.AppointmentViews;
using KDentist.Views.CalculatorViews;
using KDentist.Views.DoctorViews;
using KDentist.Views.RemindersViews;
using KDentist.Views.ReportViews;
using KDentist.Views.VideosViews;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using KDentist.Views.DocumentsView;
using KDentist.ViewModels.DocumentsViewModel;
using KDentist.Views.DiagnoseViews;

namespace KDentist
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void NewPatientWindow(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatient());
        }

        private void ProductsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            System.Windows.Controls.ListView listView = sender as System.Windows.Controls.ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.225;
            var col2 = 0.225;
            var col3 = 0.225;
            var col4 = 0.225;
            var col5 = 0.10;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
            gView.Columns[4].Width = workingWidth * col5;

        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var context = (this.DataContext as HomeViewModel);
            context.GetPatientsFromDB();

            var word = (sender as System.Windows.Controls.TextBox).Text.ToString().Trim().ToLower().Replace(" ", "");

            var searchedBy = this.SearchBy.Text.ToString();

            var items = context.Patients;


            for (int i = 0; i < items.Count; i++)
            {
                if (searchedBy == "ЕГН")
                {
                    if (!items[i].EGN.ToLower().Contains(word))
                    {
                        context.Patients.Remove(items[i]);
                        i--;
                    }
                }
                if (searchedBy == "Име")
                {
                    var fullName = items[i].FirstName + items[i].SecondName + items[i].LastName;
                    fullName = fullName.Trim().ToLower().Replace(" ", "");
                    if (!fullName.Contains(word))
                    {
                        context.Patients.Remove(items[i]);
                        i--;
                    }
                }
            }

            context.NotifyPatientsChanged();
        }

        private void RedirectToPatient(object sender, SelectionChangedEventArgs e)
        {
            var egn = ((sender as System.Windows.Controls.ListView).SelectedItem as PatientViewModel).EGN;

            var page = new PatientProfile();

            (page.DataContext as PatientProfileViewModel).LoadData(egn);

            NavigationService.Navigate(page);
        }

        private void MDTsWindow(object sender, RoutedEventArgs e)
        {
            var page = new AllMDT();
            NavigationService.Navigate(page);
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            AddDentistAppointment win = new AddDentistAppointment();
            win.Closed += delegate
            {
                //  The user has closed our dialog.
                (this.DataContext as HomeViewModel).NotifyAppointmentsChanged();
            };
            win.Date.SelectedDate = (this.DataContext as HomeViewModel).ShowAppointmentDate;
            win.Show();
        }

        private void AppontmentsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            System.Windows.Controls.ListView listView = sender as System.Windows.Controls.ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.70;
            var col2 = 0.18;
            var col3 = 0.12;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;

        }

        private void DoctorHome(object sender, RoutedEventArgs e)
        {
            var page = new DoctorsHome();
            NavigationService.Navigate(page);
        }

        private void ActivitesHome(object sender, RoutedEventArgs e)
        {

            var page = new ActivitiesHome();
            NavigationService.Navigate(page);
        }

        private void RemindersHome(object sender, RoutedEventArgs e)
        {

            var page = new HomeReminders();
            NavigationService.Navigate(page);
        }
        public static bool HasConnection()
        {
            try
            {
                System.Net.IPHostEntry i = System.Net.Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void VideosHome(object sender, RoutedEventArgs e)
        {
            if (HasConnection())
            {
                var page = new VideosHome();
                NavigationService.Navigate(page);
            }
            else
            {
                System.Windows.MessageBox.Show("За тази функция е необходима интернет връзка !");
            }
        }

        private void Calculator(object sender, RoutedEventArgs e)
        {
            var page = new Calculator();
            NavigationService.Navigate(page);
        }

        private void WeekAppointments_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new WeekAppointments());
        }

        private void Report(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DPForYear());

        }

        private void Archive()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var from = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/KDentist/Backup";

                var backup = Directory.CreateDirectory(dialog.SelectedPath + "/KDentistBackup/" + DateTime.Now.ToShortDateString()).FullName; 
                var photoes = Directory.CreateDirectory(dialog.SelectedPath + "/KDentistBackup/" + DateTime.Now.ToShortDateString()+"/Photoes").FullName;

                if (File.Exists(from + "\\KDentistDB.mdf"))
                {
                    File.Copy(from + "\\KDentistDB.mdf", backup + "\\KDentistDB.mdf", true);
                }
                else
                {
                    MessageBox.Show("Трябва да рестартирате програмата.");
                }

                var fromPhotoes = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/KDentist/Photoes";
                copyDirectory(fromPhotoes, photoes);
                var fromDocuments = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/KDentist/Documents";
                var toDocuments = Directory.CreateDirectory(dialog.SelectedPath + "/KDentistBackup/" + DateTime.Now.ToShortDateString() + "/Documents").FullName;
                copyDirectory(fromDocuments, toDocuments); 
            }
        }

        private void copyDirectory(string strSource, string strDestination)
        {
            if (!Directory.Exists(strDestination))
            {
                Directory.CreateDirectory(strDestination);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(strSource);
            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo tempfile in files)
            {
                tempfile.CopyTo(System.IO.Path.Combine(strDestination, tempfile.Name));
            }

            DirectoryInfo[] directories = dirInfo.GetDirectories();
            foreach (DirectoryInfo tempdir in directories)
            {
                copyDirectory(System.IO.Path.Combine(strSource, tempdir.Name), System.IO.Path.Combine(strDestination, tempdir.Name));
            }

        }

        private void Documents(object sender, RoutedEventArgs e)
        {
            var page = new DocumentsHome();
            (page.DataContext as DocumentsHomeViewModel).GetDocuments();
            NavigationService.Navigate(page);
        }

        private void Archive(object sender, RoutedEventArgs e)
        {


            var backup = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/KDentist/Backup").FullName + "/KDentistDB.mdf";

            FileInfo info = new FileInfo(backup);
            var modified = info.LastWriteTime;
            if (modified.ToShortDateString() == DateTime.Now.ToShortDateString() &&
                (modified.Hour == DateTime.Now.Hour))
            {
                if (DateTime.Now.Minute - modified.Minute < 2)
                {
                    Archive();
                }
                else
                {
                    MessageBoxResult question = System.Windows.MessageBox.Show("За да архивирате всичките записи е препорачително рестартиране на програма, ако вече не сте го направили. Искате ли да затворите програмата?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (question == MessageBoxResult.Yes)
                    {
                        System.Windows.Application.Current.Shutdown();
                    }
                    else if (question == MessageBoxResult.No)
                    {
                        Archive();
                    }
                }
            }
            else
            {
                MessageBoxResult question = System.Windows.MessageBox.Show("За да архивирате всичките записи е препорачително рестартиране на програма, ако вече не сте го направили. Искате ли да затворите програмата?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    System.Windows.Application.Current.Shutdown();
                }
                else if (question == MessageBoxResult.No)
                {
                    Archive();
                }
            }
        }

        private void Diagnoses(object sender, RoutedEventArgs e)
        {

            var page = new HomeDiagnoses();
            NavigationService.Navigate(page);
        }
    }
}
