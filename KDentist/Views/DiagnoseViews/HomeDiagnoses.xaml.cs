using KDentist.Model;
using KDentist.ViewModels.DiagnosesViewModels;
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

namespace KDentist.Views.DiagnoseViews
{
    /// <summary>
    /// Interaction logic for HomeDiagnoses.xaml
    /// </summary>
    public partial class HomeDiagnoses : Page
    {
        public HomeDiagnoses()
        {
            InitializeComponent();
        }

        private void ProductsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.40;
            var col2 = 0.40;
            var col3 = 0.20;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;

        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var context = (this.DataContext as DiagnosesHomeViewModel);
            context.GetDiagnosesFromDB();

            var word = (sender as TextBox).Text.ToString().Trim().ToLower().Replace(" ", "");

            var searchedBy = this.SearchBy.Text.ToString();

            var items = context.Diagnoses;


            for (int i = 0; i < items.Count; i++)
            {
                if (searchedBy == "Име")
                {
                    if (!items[i].Name.ToLower().Contains(word))
                    {
                        context.Diagnoses.Remove(items[i]);
                        i--;
                    }
                }

                if (searchedBy == "Code")
                {
                    if (!items[i].Code.ToLower().Contains(word))
                    {
                        context.Diagnoses.Remove(items[i]);
                        i--;
                    }
                }
            }

            context.NotifyDiagnosesChanged();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as DiagnosesHomeViewModel);

            var isValid = viewmodel.AddDiagnoseViewModel.IsModelValid();

            if (isValid)
            {
                viewmodel.AddDiagnoseViewModel.Add.Execute(null);
                NavigationService.Navigate(new HomeDiagnoses());
            }
            else
            {
                if (this.Name.Text == null || this.Name.Text == "")
                {
                    this.Name.Text = "";
                }
                if (this.Code.Text == null || this.Code.Text == "")
                {
                    this.Code.Text = "";
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void RedirectToMDT(object sender, SelectionChangedEventArgs e)
        {
            var id = ((sender as ListView).SelectedItem as Diagnose).Id;

            var page = new EditDiagnose();

            (page.DataContext as EditDiagnoseViewModel).LoadData(id);

            NavigationService.Navigate(page);
        }

        private void RemoveDiagnose_Click(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse((sender as Button).CommandParameter.ToString());
            (this.DataContext as DiagnosesHomeViewModel).Delete.Execute(Id);

        }
    }
}
