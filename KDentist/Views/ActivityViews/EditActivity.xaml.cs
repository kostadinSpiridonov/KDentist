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
    /// Interaction logic for EditActivity.xaml
    /// </summary>
    public partial class EditActivity : Page
    {
        public EditActivity()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new ActivitiesHome());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as EditActivityViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Save.Execute(null);
                NavigationService.Navigate(new ActivitiesHome());
            }
            else
            {
                if (this.Name.Text == null || this.Name.Text == "")
                {
                    this.Name.Text = "";
                }

                if (this.Price.Text == null || this.Price.Text == "")
                {
                    this.Price.Text = "";
                }
            }
        }
    }
}
