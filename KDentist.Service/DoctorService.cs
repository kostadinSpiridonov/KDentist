using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IDoctorService
    {
        void Add(Doctor model);
        ICollection<Doctor> GetAll();
        void Update(Doctor model);
        void Remove(int id);
        bool Exist(int id);
        Doctor GetById(int id);
    }

    public class DoctorService:BaseService, IDoctorService
    {
        public void Add(Doctor model)
        {
            this.context.Doctors.Add(model);
            this.context.SaveChanges();
        }

        public ICollection<Doctor> GetAll()
        {
            return this.context.Doctors.ToList();
        }

        public void Update(Doctor model)
        {
            var doctor = this.context.Doctors.Find(model.Id);
            doctor.FirstName = model.FirstName;
            doctor.SecondName = model.SecondName;
            doctor.LastName = model.LastName;
            
            this.context.SaveChanges();
        }

        public void Remove(int id)
        {
            var doctor = this.context.Doctors.Find(id);

            this.context.Doctors.Remove(doctor);

            this.context.SaveChanges();
        }


        public bool Exist(int id)
        {
            return this.context.Doctors.Any(x => x.Id == id);
        }


        public Doctor GetById(int id)
        {
            return this.context.Doctors.Find(id);
        }
    }
}
