using KDentist.ViewModels.AppointmentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDentist.Views.AppointmentViews
{
    /// <summary>
    /// Interaction logic for WeekAppointments.xaml
    /// </summary>
    public partial class WeekAppointments : Page
    {
        public WeekAppointments()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void AppontmentsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth; // take into account vertical scrollbar
            var col1 = 0.60;
            var col2 = 0.27;
            var col3 = 0.13;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddDentistAppointment win = new AddDentistAppointment();
            win.Closed += delegate
            {
                //  The user has closed our dialog.
                (this.DataContext as WeekAppointmentsAllViewModel).UpdateDays();
            };
            win.Date.SelectedDate = (this.DataContext as WeekAppointmentsAllViewModel).Days.First().Date;
            win.Show();
        }
    }
}
