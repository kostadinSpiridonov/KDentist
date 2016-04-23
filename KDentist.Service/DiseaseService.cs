using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IDiseaseService
    {
        void Add(Disease model);
        void Remove(int id);
        ICollection<Disease> GetForPatient(int patientId);
        void Update(Disease model);
        Disease GetById(int id);
        void UpdateContext();
    }

    public class DiseaseService:BaseService,IDiseaseService
    {
        public void Add(Disease model)
        {
            this.context.Diseases.Add(model);
            this.context.SaveChanges();
        }

        public void Remove(int id)
        {
            var disease = this.context.Diseases.Find(id);
            this.context.Diseases.Remove(disease);
            this.context.SaveChanges();
        }

        public ICollection<Disease> GetForPatient(int patientId)
        {
            return this.context.Diseases.Where(x => x.PatientId == patientId).ToList();
        }

        public void Update(Disease model)
        {
            var disease = this.context.Diseases.Find(model.Id);
            disease.Name = model.Name;
            this.context.SaveChanges();
        }


        public Disease GetById(int id)
        {
            return this.context.Diseases.Find(id);
        }


        public void UpdateContext()
        {
            this.context = new Data.ApplicationDbContext();
        }
    }
}
