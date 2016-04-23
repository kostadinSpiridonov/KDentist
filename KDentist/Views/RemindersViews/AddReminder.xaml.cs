using KDentist.ViewModels.ReminderViewModels;
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

namespace KDentist.Views.RemindersViews
{
    /// <summary>
    /// Interaction logic for AddReminder.xaml
    /// </summary>
    public partial class AddReminder : Window
    {
        public AddReminder()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as AddReminderViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Add.Execute(null);

                this.Close();

            }
            else
            {
                if (this.Content.Text == "")
                {
                    this.Content.Text = "";
                }
                if (this.Date.SelectedDate == null)
                {
                    this.Date.SelectedDate = DateTime.Now;
                }
                if (this.Date.SelectedDate.Value.ToString() == null || this.Date.SelectedDate.Value.ToString() == "")
                {
                    this.Date.SelectedDate = DateTime.Now;
                }
            }
        }
    }
}
