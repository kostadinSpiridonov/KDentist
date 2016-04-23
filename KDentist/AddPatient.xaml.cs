using KDentist.ViewModels;
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
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Page
    {
        public AddPatient()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as AddPatientViewModel);

            var isValid = viewmodel.IsModelValid();

            if(isValid)
            {
                viewmodel.Add.Execute(null);
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
    }
}
