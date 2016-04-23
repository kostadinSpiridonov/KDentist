using KDentist.Model;
using KDentist.ViewModels.ActivityViewModels;
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

namespace KDentist.Views.ActivityViews
{
    /// <summary>
    /// Interaction logic for ActivitiesHome.xaml
    /// </summary>
    public partial class ActivitiesHome : Page
    {
        public ActivitiesHome()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as ActivitiesHomeViewModel);

            var isValid = viewmodel.AddActivityViewModel.IsModelValid();

            if (isValid)
            {
                viewmodel.AddActivityViewModel.Add.Execute(null);
                NavigationService.Navigate(new ActivitiesHome());
            }
            else
            {
                if (this.Name.Text == null || this.Name.Text == "")
                {
                    this.Name.Text = "";
                }
                decimal vald = 0;
                if (!decimal.TryParse(this.Price.Text, out vald))
                {
                    this.Price.Text = "";
                }
            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var context = (this.DataContext as ActivitiesHomeViewModel);
            context.GetActivitiesFromDb();

            var word = (sender as TextBox).Text.ToString().Trim().ToLower().Replace(" ", "");

            var searchedBy = this.SearchBy.Text.ToString();

            var items = context.Activities;


            for (int i = 0; i < items.Count; i++)
            {
                if (searchedBy == "Име")
                {
                    if (!items[i].Name.ToLower().Contains(word))
                    {
                        context.Activities.Remove(items[i]);
                        i--;
                    }
                }
                else if (searchedBy == "Описание")
                {
                    if (!items[i].Description.ToLower().Contains(word))
                    {
                        context.Activities.Remove(items[i]);
                        i--;
                    }
                }
                else if (searchedBy == "Цена")
                {
                    if (!items[i].Price.ToString().ToLower().Contains(word))
                    {
                        context.Activities.Remove(items[i]);
                        i--;
                    }
                }
            }

            context.NotifyActivitiesChanged();
        }

        private void RemoveActivity_Click(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse((sender as Button).CommandParameter.ToString());
            (this.DataContext as ActivitiesHomeViewModel).Delete.Execute(Id);
        }

        private void ActivitiesListView_SizeChanged(object sender, SizeChangedEventArgs e)
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

        private void RedirectToActivity(object sender, SelectionChangedEventArgs e)
        {
            var id = ((sender as ListView).SelectedItem as Activity).Id;

            var page = new EditActivity();

            (page.DataContext as EditActivityViewModel).LoadData(id);

            NavigationService.Navigate(page);
        }
    }
}
