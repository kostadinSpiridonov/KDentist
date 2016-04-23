using KDentist.Model;
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
    /// Interaction logic for HomeReminders.xaml
    /// </summary>
    public partial class HomeReminders : Page
    {
        public HomeReminders()
        {
            InitializeComponent();
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var context = (this.DataContext as HomeRemindersViewModel);
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
            var col1 = 0.60;
            var col2 = 0.30;
            var col3 = 0.10;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
        }

        private void RemoveReminder_Click(object sender, RoutedEventArgs e)
        {
            var Id = int.Parse((sender as Button).CommandParameter.ToString());
            (this.DataContext as HomeRemindersViewModel).Delete.Execute(Id);
            
        }

        private void RedirectToReminder(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewReminders.SelectedItem != null)
            {
                var id = (this.ListViewReminders.SelectedItem as ShowListReminder).Id;
                EditReminderV win = new EditReminderV();
                win.Topmost = true;
                var windContext = (win.DataContext as EditReminderViewModel);

                 windContext.LoadData(id);

                 windContext.NotifyAllChanged();
                win.Closed += delegate
                {
                    //  The user has closed our dialog.
                    var context = (this.DataContext as HomeRemindersViewModel);
                    context.UpdateContext();
                    context.GetRemindersFromDb();
                    context.NotifyRemindersChanged();
                    this.ListViewReminders.SelectedItem = null;
                };
                win.Show();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void AddReminderWin(object sender, RoutedEventArgs e)
        {

            AddReminder win = new AddReminder();
            win.Closed += delegate
            {
                //  The user has closed our dialog.
                var context = (this.DataContext as HomeRemindersViewModel);
                context.GetRemindersFromDb();
                context.NotifyRemindersChanged();
            };
            win.Show();
        }


        private void CompletedReminder_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse((sender as Button).CommandParameter.ToString());
            (this.DataContext as HomeRemindersViewModel).Complete.Execute(id);
        }

        private void CompletedReminders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompletedReminders());
        }
    }
}
