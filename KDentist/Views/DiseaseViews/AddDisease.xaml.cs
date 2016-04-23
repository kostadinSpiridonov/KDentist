using KDentist.ViewModels.DiseasesViewModels;
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

namespace KDentist.Views.DiseaseViews
{
    /// <summary>
    /// Interaction logic for AddDisease.xaml
    /// </summary>
    public partial class AddDisease : Window
    {
        public AddDisease()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = (this.DataContext as AddDiseaseViewModel);

            var isValid = viewmodel.IsModelValid();

            if (isValid)
            {
                viewmodel.Add.Execute(null);

                this.Close();

            }
            else
            {
                if (this.Name.Text == "")
                {
                    this.Name.Text = "";
                }
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
