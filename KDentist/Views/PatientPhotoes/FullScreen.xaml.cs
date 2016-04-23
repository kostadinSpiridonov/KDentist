using KDentist.ViewModels;
using KDentist.ViewModels.PatientPhotoes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDentist.Views.PatientPhotoes
{
    /// <summary>
    /// Interaction logic for FullScreen.xaml
    /// </summary>
    public partial class FullScreen : Window
    {
        public FullScreen()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Set fulscreen image
        /// </summary>
        /// <param name="image"></param>
        public void SetImage(string image)
        {
            try
            {
                FullscreenImage.Source = new ImageSourceConverter().ConvertFromString(image) as ImageSource;
            }
            catch { }
        }
        private void CloseFullscreen(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
