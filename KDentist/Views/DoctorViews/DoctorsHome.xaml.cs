using KDentist.Model;
using KDentist.ViewModels.DoctorViewModels;
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

namespace KDentist.Views.DoctorViews
{
    /// <summary>
    /// Interaction logic for DoctorsHome.xaml
    /// </summary>
    public partial class DoctorsHome : Page
    {
        public DoctorsHome()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as DoctorsHomeViewModel);

            var isValid = viewmodel.AddDoctorViewModel.IsModelValid();

            if (isValid)
            {
                viewmodel.AddDoctorViewModel.Add.Execute(null);
                NavigationService.Navigate(new DoctorsHome());
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
            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var context = (this.DataContext as DoctorsHomeViewModel);
            context.GetDoctorstFromDb();

            var word = (sender as TextBox).Text.ToString().Trim().ToLower().Replace(" ", "");

            var searchedBy = this.SearchBy.Text.ToString();

            var items = context.Doctors;


            for (int i = 0; i < items.Count; i++)
            {
                if (searchedBy == "Име")
                {
                    var name = items[i].FirstName + items[i].SecondName + items[i].LastName;
                    if (!name.ToLower().Contains(word))
                    {
                        context.Doctors.Remove(items[i]);
                        i--;
                    }
                }
            }

            context.NotifyDoctorsChanged();
        }

        private void DoctorsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.30;
            var col2 = 0.30;
            var col3 = 0.30;
            var col4 = 0.10;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
        }

        private void RemoveDoctor_Click(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse((sender as Button).CommandParameter.ToString());
            (this.DataContext as DoctorsHomeViewModel).Delete.Execute(Id);
        }

        private void RedirectToDoctor(object sender, SelectionChangedEventArgs e)
        {
            var id = ((sender as ListView).SelectedItem as Doctor).Id;

            var page = new EditDoctor();

            (page.DataContext as EditDoctorViewModel).LoadData(id);

            NavigationService.Navigate(page);
        }
    }
}
