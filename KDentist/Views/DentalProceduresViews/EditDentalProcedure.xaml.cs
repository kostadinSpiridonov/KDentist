using KDentist.ViewModels;
using KDentist.ViewModels.DentalProcedureViewModels;
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

namespace KDentist.Views.DentalProceduresViews
{
    /// <summary>
    /// Interaction logic for EditDentalProcedure.xaml
    /// </summary>
    public partial class EditDentalProcedure : Page
    {
        public EditDentalProcedure()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
              var viewmodel = (this.DataContext as  EditDentalProcedureViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Save.Execute(null);

                var page = new PatientProfile();
                var id = (this.DataContext as EditDentalProcedureViewModel).PatientId;
                (page.DataContext as PatientProfileViewModel).LoadData(id);
                this.NavigationService.Navigate(page);
            }
            else
            {
                if (this.Tooth.Text == null || this.Tooth.Text == "")
                {
                    this.Tooth.Text = "";
                }
                if (this.Diagnosis.Text == null || this.Diagnosis.Text == "")
                {
                    this.Diagnosis.Text = "";
                }
                if (this.Date.SelectedDate == null)
                {
                    this.Date.SelectedDate = DateTime.Now;
                }

                if (this.DoctorComboBox.SelectedValue.ToString() == "0")
                {
                    this.DoctorBorderError.BorderBrush = Brushes.Red;
                    this.DoctorBorderError.BorderThickness = new Thickness(2);
                    this.DoctorErrorText.Visibility = Visibility.Visible;
                }
                else
                {
                    this.DoctorBorderError.BorderBrush = Brushes.Transparent;
                    this.DoctorBorderError.BorderThickness = new Thickness(0);
                    this.DoctorErrorText.Visibility = Visibility.Collapsed;
                }

                if (this.ActivitiesComboBox.SelectedValue.ToString() == "0")
                {
                    this.ActivityBorderError.BorderBrush = Brushes.Red;
                    this.ActivityBorderError.BorderThickness = new Thickness(2);
                    this.ActivityErrorText.Visibility = Visibility.Visible;
                }
                else
                {
                    this.ActivityBorderError.BorderBrush = Brushes.Transparent;
                    this.ActivityBorderError.BorderThickness = new Thickness(0);
                    this.ActivityErrorText.Visibility = Visibility.Collapsed;
                }

                if (this.Date.SelectedDate.Value.ToString() == null || this.Date.SelectedDate.Value.ToString() == "")
                {
                    this.Date.SelectedDate = DateTime.Now;
                }

                decimal vald = 0;
                if (!decimal.TryParse(this.PaidSum.Text, out vald))
                {
                    this.PaidSum.Text = "";
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var page = new PatientProfile();
            var id = (this.DataContext as EditDentalProcedureViewModel).PatientId;
            (page.DataContext as PatientProfileViewModel).LoadData(id);
            this.NavigationService.Navigate(page);
        }
    }
}
