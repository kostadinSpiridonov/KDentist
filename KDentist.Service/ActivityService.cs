using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IActivityService
    {
        void Add(Activity model);
        ICollection<Activity> GetAll();
        Activity GetById(int id);
        void Update(Activity model);
        void Remove(int id);
        bool Exist(int id);
    }

    public class ActivityService:BaseService,IActivityService
    {
        public void Add(Activity model)
        {
            this.context.Activities.Add(model);
            this.context.SaveChanges();
        }

        public ICollection<Activity> GetAll()
        {
            return this.context.Activities.ToList();
        }

        public Activity GetById(int id)
        {
            return this.context.Activities.Find(id);
        }

        public void Update(Activity model)
        {
            var activity = this.context.Activities.Find(model.Id);
            activity.Description = model.Description;
            activity.Name = model.Name;
            activity.Price = model.Price;

            this.context.SaveChanges();
        }

        public void Remove(int id)
        {
            var activity = this.context.Activities.Find(id);
            this.context.Activities.Remove(activity);

            this.context.SaveChanges();
        }


        public bool Exist(int id)
        {
            return this.context.Activities.Any(x => x.Id == id);
        }
    }
}
