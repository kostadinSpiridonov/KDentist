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
using System.Windows.Shapes;

namespace KDentist
{
    /// <summary>
    /// Interaction logic for AddDentistAppointment.xaml
    /// </summary>
    public partial class AddDentistAppointment : Window
    {
        public AddDentistAppointment()
        {
            InitializeComponent();
            Hour.Text = DateTime.Now.Hour.ToString();
            Minutes.Text = DateTime.Now.Minute.ToString();

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as AddDentistAppointmentViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Add.Execute(null);

                this.Close();
              
            }
            else
            {
                if (this.Hour.Text == "")
                {
                    this.Hour.Text = "";
                }
                if (this.Minutes.Text == "")
                {
                    this.Minutes.Text = "";
                }
                if (this.Date.SelectedDate == null)
                {
                    this.Date.SelectedDate = DateTime.Now;
                }
                if (this.Date.SelectedDate.Value.ToString() == null || this.Date.SelectedDate.Value.ToString() == "")
                {
                    this.Date.SelectedDate = DateTime.Now;
                }
                if(viewmodel.PatientId==0)
                {
                    if (viewmodel.UnknownPatient == null)
                    {
                        ComboPatientsErroBorder.BorderThickness = new Thickness(1);
                        ComboPatientsErroBorder.BorderBrush = Brushes.Red;
                        ComboPatientsError.Text = "Моля, изберете пациент !";

                        UnknownPatientErroBorder.BorderThickness = new Thickness(1);
                        UnknownPatientErroBorder.BorderBrush = Brushes.Red;
                    }
                }
                else
                {

                    ComboPatientsErroBorder.BorderThickness = new Thickness(0);
                    ComboPatientsErroBorder.BorderBrush = Brushes.Transparent;
                    ComboPatientsError.Text = "";


                    UnknownPatientErroBorder.BorderThickness = new Thickness(0);
                    UnknownPatientErroBorder.BorderBrush = Brushes.Transparent;
                }


            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MarkAll_Click(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).SelectAll();
            (sender as TextBox).Focus();
            e.Handled = true;
        }
    }
}
