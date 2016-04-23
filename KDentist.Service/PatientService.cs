using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KDentist.Service
{
    public interface IPatientService
    {
        int Add(Patient model);
        ICollection<Patient> GetAll();
        bool Exist(string egn);
        Patient GetByEGN(string egn);
        Patient GetById(int id);
        void Update(Patient model);
        bool ExistUpdate(int id, string egn);
        void Delete(int id);
    }

    public class PatientService : BaseService, IPatientService
    {
        public int Add(Patient model)
        {
            var patient=this.context.Patients.Add(model);
            this.context.SaveChanges();

            return patient.Id;
        }

        public ICollection<Patient> GetAll()
        {
            return this.context.Patients
                .ToList();
        }


        public bool Exist(string egn)
        {
            return this.context.Patients.Any(x => x.EGN == egn);
        }

        public Patient GetByEGN(string egn)
        {
            return this.context.Patients.Where(x => x.EGN == egn).FirstOrDefault();
        }


        public void Update(Patient model)
        {
            var patient = this.context.Patients.Find(model.Id);
            patient.EGN = model.EGN;
            patient.FirstName = model.FirstName;
            patient.SecondName = model.SecondName;
            patient.LastName = model.LastName;
            patient.Profession = model.Profession;
            patient.Phone = model.Phone;
            patient.Address = model.Address;
            patient.Age = model.Age;
            this.context.SaveChanges();
        }


        public bool ExistUpdate(int id, string egn)
        {
            var patienst = this.context.Patients.Where(x => x.Id != id);
            return patienst.Any(x => x.EGN == egn);
        }


        public Patient GetById(int id)
        {
            return this.context.Patients.Where(x => x.Id == id).FirstOrDefault();
        }


        public void Delete(int id)
        {
            var patient = this.context.Patients.Where(x=>x.Id==id).FirstOrDefault();
           
            while(patient.DentalProcedures.Count()>0)
            {
                this.context.DentalProcedures.Remove(patient.DentalProcedures.First());
            }

            while (patient.ProceduresTable.Cells.Count() > 0)
            {
                this.context.ProceduresCells.Remove(patient.ProceduresTable.Cells.First());
            }
            this.context.Patients.Remove(patient);

            this.context.SaveChanges();
        }
    }
}
