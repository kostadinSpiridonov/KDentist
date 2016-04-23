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
    /// Interaction logic for EditMDT.xaml
    /// </summary>
    public partial class EditMDT : Page
    {
        public EditMDT()
        {
            InitializeComponent();
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as EditMDTViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Save.Execute(null);
                NavigationService.Navigate(new AllMDT());
            }
            else
            {
                if (this.Name.Text == null || this.Name.Text == "")
                {
                    this.Name.Text = "";
                }
                if (this.Phone.Text == null || this.Phone.Text == "")
                {
                    this.Phone.Text = "";
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AllMDT());
        }
    }
}
