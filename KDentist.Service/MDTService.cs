using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IMDTService
    {
        ICollection<MDT> GetAll();
        void Add(MDT model);
        MDT GetById(int id);
        void Update(MDT model);
        bool Exist(int id);
        void Remove(int id);
    }

    public class MDTService:BaseService,IMDTService
    {
        public ICollection<MDT> GetAll()
        {
            return this.context.MDTs.ToList();
        }

        public void Add(MDT model)
        {
            this.context.MDTs.Add(model);
            this.context.SaveChanges();
        }

        public MDT GetById(int id)
        {
           return this.context.MDTs.Find(id);
        }


        public void Update(MDT model)
        {
            var mdt = this.context.MDTs.Find(model.Id);
            mdt.Name = model.Name;
            mdt.Phone = model.Phone;
            this.context.SaveChanges();
        }


        public bool Exist(int id)
        {
            return this.context.MDTs.Any(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var mdt = this.context.MDTs.Find(id);
            this.context.MDTs.Remove(mdt);
            this.context.SaveChanges();
        }
    }
}
