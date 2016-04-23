using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.ReminderViewModels
{
    public class AddReminderViewModel : BaseViewModel
    {
        public AddReminderViewModel()
        {
            this.toDate = DateTime.Now;
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

        private ICommand add;
        public ICommand Add
        {
            get
            {
                if (this.add == null)
                {
                    this.add = new RelayCommand(this.AddC);
                }
                return this.add;
            }
        }
        private void AddC(object obj)
        {
            var reminder = new Reminder
            {
                ToDate=this.toDate,
                Content=this.content
            };

            this.reminderService.Add(reminder);
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
    }
}
