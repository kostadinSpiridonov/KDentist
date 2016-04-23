using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.ViewModels.ReminderViewModels
{
    public class CompletedRemindersViewModel : BaseViewModel,INotifyPropertyChanged
    {
        public CompletedRemindersViewModel()
        {
            GetRemindersFromDb();
        }

        private ObservableCollection<Reminder> reminders;

        public ObservableCollection<Reminder> Reminders
        {
            get
            {
                return this.reminders;
            }
        }

        public void GetRemindersFromDb()
        {
            this.reminders = new ObservableCollection<Reminder>(this.reminderService
                .GetAll()
                .Where(x => x.Completed)
                .OrderBy(x => x.ToDate));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public void NotifyRemindersChanged()
        {
            NotifyPropertyChanged("Reminders");
        }
    }
}
