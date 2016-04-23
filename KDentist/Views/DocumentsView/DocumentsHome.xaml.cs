using KDentist.ViewModels.DocumentsViewModel;
using Microsoft.Win32;
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

namespace KDentist.Views.DocumentsView
{
    /// <summary>
    /// Interaction logic for DocumentsHome.xaml
    /// </summary>
    public partial class DocumentsHome : Page
    {
        public DocumentsHome()
        {
            InitializeComponent();
            var mainWindow = Application.Current.MainWindow;
            FrameworkElementFactory factoryPanel = new FrameworkElementFactory(typeof(WrapPanel));
            factoryPanel.SetValue(WrapPanel.WidthProperty, (mainWindow.ActualWidth-30));

            ItemsPanelTemplate template = new ItemsPanelTemplate();
            template.VisualTree = factoryPanel;

            ListViewAppointments.ItemsPanel = template;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void AddDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseImageDlg = new OpenFileDialog();
            chooseImageDlg.Title = "Select an document file.";

            var result = chooseImageDlg.ShowDialog();

            if (result.HasValue)
            {
                if (result.Value)
                {
                    var path = chooseImageDlg.FileName;
                    if (!(this.DataContext as DocumentsHomeViewModel).AddDocument(path))
                    {
                        MessageBox.Show("Имаше проблем при качването на документа. Опитайте отново.", "Проблем", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Имаше проблем при качването на документа. Опитайте отново.", "Проблем", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Имаше проблем при качването на документа. Опитайте отново.", "Проблем", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }


        private void AppontmentsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            System.Windows.Controls.ListView listView = sender as System.Windows.Controls.ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.88;
            var col3 = 0.12;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col3;
        }

        private void DeleteDocument_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете този документ?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                var imagePath = (sender as Button).Tag.ToString();
                if (!(this.DataContext as DocumentsHomeViewModel).Delete(imagePath))
                {
                    MessageBox.Show("Възникна проблем при изтриването. Опитайте отново.", "Проблем", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RunFile(object sender, SelectionChangedEventArgs e)
        {
            var path = (ListViewAppointments.SelectedItem as DocumentViewModel).Url;
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Нямате програма за отваряне на този файл.");
            }
        }
    }
}
