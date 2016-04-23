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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDentist.Views.RemindersViews
{
    /// <summary>
    /// Interaction logic for CompletedReminders.xaml
    /// </summary>
    public partial class CompletedReminders : Page
    {
        public CompletedReminders()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomeReminders());
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var context = (this.DataContext as CompletedRemindersViewModel);
            context.GetRemindersFromDb();

            var word = (sender as TextBox).Text.ToString().Trim().ToLower().Replace(" ", "");

            var searchedBy = this.SearchBy.Text.ToString();

            var items = context.Reminders;


            for (int i = 0; i < items.Count; i++)
            {
                if (searchedBy == "Съдържание")
                {
                    if (!items[i].Content.ToLower().Contains(word))
                    {
                        context.Reminders.Remove(items[i]);
                        i--;
                    }
                }

                if (searchedBy == "Дата")
                {
                    if (!items[i].ToDate.ToString().ToLower().Contains(word))
                    {
                        context.Reminders.Remove(items[i]);
                        i--;
                    }
                }
            }

            context.NotifyRemindersChanged();
        }

        private void RemindersListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col1 = 0.70;
            var col2 = 0.30;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }
    }
}
