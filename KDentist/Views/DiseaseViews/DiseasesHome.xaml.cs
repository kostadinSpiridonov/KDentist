using KDentist.Model;
using KDentist.ViewModels;
using KDentist.ViewModels.DiseasesViewModels;
using KDentist.Views.RemindersViews;
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

namespace KDentist.Views.DiseaseViews
{
    /// <summary>
    /// Interaction logic for DiseasesHome.xaml
    /// </summary>
    public partial class DiseasesHome : Page
    {
        public DiseasesHome()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var id = (this.DataContext as HomeDiseasesViewModel).Id;

            var page = new PatientProfile();

            (page.DataContext as PatientProfileViewModel).LoadData(id);

            NavigationService.Navigate(page);
        }

        private void AddDiseaseWin(object sender, RoutedEventArgs e)
        {
            AddDisease win = new AddDisease();
            var id= (this.DataContext as HomeDiseasesViewModel).Id;
            (win.DataContext as AddDiseaseViewModel).Id = id;
            win.Closed += delegate
            {
                //  The user has closed our dialog.
                var context = (this.DataContext as HomeDiseasesViewModel);
                context.GetDiseasesFromDb();
                context.NotifyDiseasesChanged();
            };
            win.Show();

        }

        private void RemoveDisease_Click(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse((sender as Button).CommandParameter.ToString());
            (this.DataContext as HomeDiseasesViewModel).Delete.Execute(Id);
        }

        private void RedirectToDisease(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewDiseases.SelectedItem != null)
            {
                var id = (this.ListViewDiseases.SelectedItem as Disease).Id;
                EditDisease win = new EditDisease();
                win.Topmost = true;
                var windContext = (win.DataContext as EditDiseaseViewModel);

                windContext.LoadData(id);

                windContext.NotifyAllChanged();
                win.Closed += delegate
                {
                    //  The user has closed our dialog.
                    var context = (this.DataContext as HomeDiseasesViewModel);
                    context.UpdateContext();
                    context.GetDiseasesFromDb();
                    context.NotifyDiseasesChanged();
                    this.ListViewDiseases.SelectedItem = null;
                };
                win.Show();
            }
        }

        private void DiseasesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.60;
            var col2 = 0.30;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }
    }
}
