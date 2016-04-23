using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.ReminderViewModels
{
    public class EditReminderViewModel: BaseViewModel,INotifyPropertyChanged
    {

        private int id;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private string content;

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        private DateTime toDate;

        public DateTime ToDate
        {
            get
            {
                return this.toDate;
            }
            set
            {
                this.toDate = value;
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (this.save == null)
                {
                    this.save = new RelayCommand(this.SaveC);
                }
                return this.save;
            }
        }
        private void SaveC(object obj)
        {
            var reminder = new Reminder
            {
                Id=this.id,
                ToDate=this.toDate,
                Content=this.content
            };

            this.reminderService.Update(reminder);
        }

        public void LoadData(int id)
        {
            var reminder = this.reminderService.GetById(id);
            this.id = id;
            this.content = reminder.Content;
            this.toDate = reminder.ToDate;
        }
        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.content))
            {
                canExecute = false;
            }
            

            if (this.toDate == null)
            {
                canExecute = false;
            }

            return canExecute;
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

        public void NotifyAllChanged()
        {
            NotifyPropertyChanged("Id");
            NotifyPropertyChanged("Content");
            NotifyPropertyChanged("ToDate");
        }
    }
}
