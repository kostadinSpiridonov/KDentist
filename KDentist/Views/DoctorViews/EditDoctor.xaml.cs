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
    /// Interaction logic for EditDoctor.xaml
    /// </summary>
    public partial class EditDoctor : Page
    {
        public EditDoctor()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DoctorsHome());
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as EditDoctorViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Save.Execute(null);
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
    }
}
