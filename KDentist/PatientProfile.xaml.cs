using KDentist.ViewModels;
using KDentist.ViewModels.DentalProcedureViewModels;
using KDentist.ViewModels.DiseasesViewModels;
using KDentist.ViewModels.PatientPhotoes;
using KDentist.Views.DentalProceduresViews;
using KDentist.Views.DiseaseViews;
using KDentist.Views.PatientPhotoes;
using KDentist.Views.PatientToothTable;
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

namespace KDentist
{
    /// <summary>
    /// Interaction logic for PatientProfile.xaml
    /// </summary>
    public partial class PatientProfile : Page
    {
        public PatientProfile()
        {
            InitializeComponent();
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as PatientProfileViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Save.Execute(null);
                this.NavigationService.Navigate(new Home());
            }
            else
            {
                if (this.FirstName.Text == null || this.FirstName.Text == "")
                {
                    this.FirstName.Text = "";
                }
                if (this.SecondName.Text == null || this.SecondName.Text == "")
                {
                    this.SecondName.Text = "";
                }
                if (this.LastName.Text == null || this.LastName.Text == "")
                {
                    this.LastName.Text = "";
                }
                if (this.EGN.Text == null || this.EGN.Text == "")
                {
                    this.EGN.Text = "";
                }
                if (this.Age.Text == null || this.Age.Text == "")
                {
                    this.Age.Text = "";
                }
            }
        }

        private void SelectText(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
            (sender as TextBox).Focus();
            e.Handled = true;
        }

        private void SelectTextBtn(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).SelectAll();
            (sender as TextBox).Focus();
            e.Handled = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Home());
        }

        private void RedirecrNewDentalProcedure_Click(object sender, RoutedEventArgs e)
        {
            var page = new AddDentalProcedure();
            (page.DataContext as AddDentalProcedureViewModel).PatientId = (this.DataContext as PatientProfileViewModel).Id;
            this.NavigationService.Navigate(page);
        }

        private void ProductsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.08;
            var col2 = 0.21;
            var col3 = 0.24;
            var col4 = 0.25;
            var col5 = 0.15;
            var col6 = 0.07;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
            gView.Columns[4].Width = workingWidth * col5;
            gView.Columns[5].Width = workingWidth * col6;

        }

        private void RemoveProcedure(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse((sender as Button).CommandParameter.ToString());
            (this.DataContext as PatientProfileViewModel).Delete.Execute(Id);
        }

        private void RedirectToProcedure(object sender, SelectionChangedEventArgs e)
        {
            var id = ((sender as ListView).SelectedItem as ListDentapProcedureViewModel).Id;

            var page = new EditDentalProcedure();

            (page.DataContext as EditDentalProcedureViewModel).LoadData(id);

            NavigationService.Navigate(page);
        }

        private void PatientPhotoesHome_Click(object sender, RoutedEventArgs e)
        {
            var page = new PatientPhotoesHome();
            var id = (this.DataContext as PatientProfileViewModel).Id;
            (page.DataContext as PatientPhotoesHomeViewModel).GetImages(id);
            this.NavigationService.Navigate(page);
        }

        private void RedirectToDiseases_Click(object sender, RoutedEventArgs e)
        {

            var page = new DiseasesHome();
            var id = (this.DataContext as PatientProfileViewModel).Id;
            (page.DataContext as HomeDiseasesViewModel).Id = id;
            (page.DataContext as HomeDiseasesViewModel).GetDiseasesFromDb();
            (page.DataContext as HomeDiseasesViewModel).NotifyDiseasesChanged();
            this.NavigationService.Navigate(page);
        }


        private void RedirectToToothTable(object sender, RoutedEventArgs e)
        {
            var id = (this.DataContext as PatientProfileViewModel).Id;

            var page = new PatientToothTable();

            (page.DataContext as ProceduresTableViewModel).LoadData(id);

            NavigationService.Navigate(page);
        }

    }
}
