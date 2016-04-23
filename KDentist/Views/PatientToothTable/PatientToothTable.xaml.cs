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

namespace KDentist.Views.PatientToothTable
{
    /// <summary>
    /// Interaction logic for PatientToothTable.xaml
    /// </summary>
    public partial class PatientToothTable : Page
    {
        public PatientToothTable()
        {
            InitializeComponent();
        }
        private void ScrollSynchroFirst(object sender, ScrollChangedEventArgs e)
        {

            ScrollViewer _listboxScrollViewer1 = GetDescendantByType(Second, typeof(ScrollViewer)) as ScrollViewer;
            ScrollViewer _listboxScrollViewer2 = GetDescendantByType(First, typeof(ScrollViewer)) as ScrollViewer;
            _listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
        }

        public Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }

        private void ScrollSynchroSecond(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer _listboxScrollViewer1 = GetDescendantByType(Third, typeof(ScrollViewer)) as ScrollViewer;
            ScrollViewer _listboxScrollViewer2 = GetDescendantByType(Fourth, typeof(ScrollViewer)) as ScrollViewer;
            _listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
        }

        private void ScrollDown(object sender, RoutedEventArgs e)
        {
            ScrollViewer _listboxScrollViewer1 = GetDescendantByType(Third, typeof(ScrollViewer)) as ScrollViewer;
            ScrollViewer _listboxScrollViewer2 = GetDescendantByType(Second, typeof(ScrollViewer)) as ScrollViewer;
            _listboxScrollViewer2.ScrollToBottom();
            _listboxScrollViewer1.ScrollToBottom();
        }

        private void TableListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = (double)((double)1 / (double)gView.Columns.Count);
            gView.Columns[0].Width = workingWidth*col1;
            for (int i = 1; i < gView.Columns.Count; i++)
            {
                gView.Columns[i].Width = workingWidth * col1;

            }
        }

        private void ChooseLegendSymbol_Click(object sender, MouseButtonEventArgs e)
        {
            var cell = (((sender as TextBlock).Parent as Grid).Tag as ListCellViewModel);
            var id = (int)cell.Id;

            var context = new AddToothSymbolViewModel();

            if (id != 0)
            {
                context.HasSymbol = true;
                context.CellId = cell.Id;
                context.PatientId = (this.DataContext as ProceduresTableViewModel).PatientId;
            }
            else
            {

                context.HasSymbol = false;
                context.PatientId = (this.DataContext as ProceduresTableViewModel).PatientId;
            }

            context.Year = cell.Year;
            context.Tooth = cell.Tooth;
            context.ToothOrient = int.Parse((sender as TextBlock).Tag.ToString());
            SetToothSymbol win = new SetToothSymbol();

            win.DataContext = context;
            win.Closed += delegate
            {
                //  The user has closed our dialog.
                var a = (win.DataContext as AddToothSymbolViewModel);
                (this.DataContext as ProceduresTableViewModel).UpdateRowCell(a);
                (this.DataContext as ProceduresTableViewModel).NotifyAll();
            };
            win.Show();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var id = (this.DataContext as ProceduresTableViewModel).PatientId;

            var page = new PatientProfile();

            (page.DataContext as PatientProfileViewModel).LoadData(id);

            NavigationService.Navigate(page);
        }
    }
}
