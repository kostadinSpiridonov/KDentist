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
    /// Interaction logic for PatientPhotoesHome.xaml
    /// </summary>
    public partial class PatientPhotoesHome : Page
    {
        public PatientPhotoesHome()
        {
            InitializeComponent();
            var mainWindow = Application.Current.MainWindow;
            FrameworkElementFactory factoryPanel = new FrameworkElementFactory(typeof(WrapPanel));
            factoryPanel.SetValue(WrapPanel.WidthProperty, (mainWindow.ActualWidth-30));

            ItemsPanelTemplate template = new ItemsPanelTemplate();
            template.VisualTree = factoryPanel;

            ListViewPhotoes.ItemsPanel = template;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as PatientPhotoesHomeViewModel).Clear();
            var page = new PatientProfile();
            var id = (this.DataContext as PatientPhotoesHomeViewModel).Id;
            (page.DataContext as PatientProfileViewModel).LoadData(id);
            NavigationService.Navigate(page);
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseImageDlg = new OpenFileDialog();
            chooseImageDlg.Title = "Select an image file.";
            chooseImageDlg.Filter = "Image Files (*.gif,*.jpg,*.jpeg,*.bmp,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.png";
            chooseImageDlg.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            var result = chooseImageDlg.ShowDialog();

            if (result.HasValue)
            {
                if (result.Value)
                {
                    var imagePath = chooseImageDlg.FileName;
                    if (!(this.DataContext as PatientPhotoesHomeViewModel).AddImage(imagePath))
                    {
                        MessageBox.Show("Имаше проблем при качването на снимката. Опитайте отново.", "Проблем", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Имаше проблем при качването на снимката. Опитайте отново.", "Проблем", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Имаше проблем при качването на снимката. Опитайте отново.", "Проблем", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void DeleteImage_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult  question = MessageBox.Show("Искате ли да изтриете тази снимка?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if(question==MessageBoxResult.Yes)
            {
                var imagePath = (sender as Button).Tag.ToString();
                if(!(this.DataContext as PatientPhotoesHomeViewModel).Delete(imagePath))
                {
                    MessageBox.Show("Възникна проблем при изтриването. Опитайте отново.", "Проблем", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ViewFullScreen_Click(object sender, MouseButtonEventArgs e)
        {
            var full = new FullScreen();
            full.SetImage((sender as Image).Source.ToString());
            full.Show();
        }
    }
}
