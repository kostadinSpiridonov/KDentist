using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IDiagnoseService
    {
        void Add(Diagnose model);
        ICollection<Diagnose> GetAll();
        void Update(Diagnose model);
        bool Exist(string code);
        void Remove(int id);
        Diagnose GetById(int id);
        bool ExistUpdate(string code,int id);
    }

    public class DiagnoseService:BaseService,IDiagnoseService
    {
        public void Add(Diagnose model)
        {
            this.context.Diagnoses.Add(model);
            this.context.SaveChanges();
        }

        public ICollection<Diagnose> GetAll()
        {
            return this.context.Diagnoses.ToList();
        }

        public void Update(Diagnose model)
        {
            var diagnose = this.context.Diagnoses.Find(model.Id);
            diagnose.Code = model.Code;
            diagnose.Name = model.Name;
            this.context.SaveChanges();
        }

        public bool Exist(string code)
        {
            return this.context.Diagnoses.Any(x => x.Code == code);
        }

        public void Remove(int id)
        {
            var diagnose = this.context.Diagnoses.Find(id);
            this.context.Diagnoses.Remove(diagnose);
            this.context.SaveChanges();
        }

        public Diagnose GetById(int id)
        {
            return this.context.Diagnoses.Find(id);
        }


        public bool ExistUpdate(string code,int id)
        {
            return this.context.Diagnoses.Any(x => x.Code == code&&x.Id!=id);

        }
    }
}
