using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IReminderService
    {
        void Add(Reminder model);
        ICollection<Reminder> GetAll();
        void Remove(int id);
        Reminder GetById(int id);
        void Update(Reminder model);
        void SetCompleted(int id);
        void UpdateContext();
    }

    public class ReminderService:BaseService, IReminderService
    {
        public void Add(Reminder model)
        {
            this.context.Reminders.Add(model);
            this.context.SaveChanges();
        }

        public ICollection<Reminder> GetAll()
        {
            return this.context.Reminders.ToList();
        }

        public void Remove(int id)
        {
            var reminder = this.context.Reminders.Find(id);
            this.context.Reminders.Remove(reminder);
            this.context.SaveChanges();
        }

        public Reminder GetById(int id)
        {
            return this.context.Reminders.Find(id);
        }

        public void Update(Reminder model)
        {
            var reminder = this.context.Reminders.Find(model.Id);
            reminder.Content = model.Content;
            reminder.ToDate = model.ToDate;
            this.context.SaveChanges();
        }

        public void SetCompleted(int id)
        {
            this.context.Reminders.Find(id).Completed = true;
            this.context.SaveChanges();
        }


        public void UpdateContext()
        {
            this.context = new Data.ApplicationDbContext();
        }
    }
}
