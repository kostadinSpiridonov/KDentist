using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace KDentist.ViewModels.ReminderViewModels
{
    public class ShowListReminder
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public virtual DateTime ToDate { get; set; }

        public bool Completed { get; set; }

        public Brush Background { get; set; }
    }

    public class HomeRemindersViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public HomeRemindersViewModel()
        {
            GetRemindersFromDb();
        }

        private ObservableCollection<ShowListReminder> reminders;

        public ObservableCollection<ShowListReminder> Reminders
        {
            get
            {
                return this.reminders;
            }
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

        public void GetRemindersFromDb()
        {
            this.reminders = new ObservableCollection<ShowListReminder>(this.reminderService
                .GetAll()
                .Where(x=>!x.Completed)
                .OrderBy(x => x.ToDate)
                .ToList()
                .ConvertAll(
                x=> new ShowListReminder
                {
                    Background=DateTime.Now.ToShortDateString()!=x.ToDate.ToShortDateString()?Brushes.Transparent:Brushes.SkyBlue,
                    Completed=x.Completed,
                    Content=x.Content,
                    Id=x.Id,
                    ToDate=x.ToDate
                }));
        }

        public void UpdateContext()
        {
            this.reminderService.UpdateContext();
        }

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (this.delete == null)
                {
                    this.delete = new RelayCommand(this.DeleteC);
                }
                return this.delete;
            }
        }
        private void DeleteC(object obj)
        {
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете това напомняне?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                int id = int.Parse(obj.ToString());
                this.reminderService.Remove(id);
                Reminders.Remove(Reminders.Where(x => x.Id == id).FirstOrDefault());
                NotifyRemindersChanged();
            }
        }

        private ICommand complete;
        public ICommand Complete
        {
            get
            {
                if (this.complete == null)
                {
                    this.complete = new RelayCommand(this.CompleteC);
                }
                return this.complete;
            }
        }
        private void CompleteC(object obj)
        {
            int id = int.Parse(obj.ToString());
            this.reminderService.SetCompleted(id);
            Reminders.Remove(Reminders.Where(x => x.Id == id).FirstOrDefault());
            NotifyRemindersChanged();
        }
    }
}
