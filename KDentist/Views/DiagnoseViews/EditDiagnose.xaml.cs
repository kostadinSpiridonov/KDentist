using KDentist.Service;
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
    /// Interaction logic for EditDiagnose.xaml
    /// </summary>
    public partial class EditDiagnose : Page
    {
        public EditDiagnose()
        {
            InitializeComponent();
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as EditDiagnoseViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Save.Execute(null);
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

                var service = new DiagnoseService();

                if (service.ExistUpdate(this.Code.Text,viewmodel.Id))
                {
                    ErrorExist.Text = "Съществува диагноза с този код.";
                }
                else
                {
                    ErrorExist.Text = "";
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

    }
}
