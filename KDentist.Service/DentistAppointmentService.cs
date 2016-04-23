using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IDentistAppointmentService
    {
        void Add(DentistAppointment model);
        ICollection<DentistAppointment> GetAll();
        void Delete(int id);
        DentistAppointment GetById(int id);
    }

    public class DentistAppointmentService : BaseService, IDentistAppointmentService
    {
        public void Add(DentistAppointment model)
        {
            this.context.DentistAppontments.Add(model);
            this.context.SaveChanges();
        }

        public ICollection<DentistAppointment> GetAll()
        {
            return this.context.DentistAppontments.ToList();
        }

        public void Delete(int id)
        {
            var appointment = this.context.DentistAppontments.Find(id);
            this.context.DentistAppontments.Remove(appointment);
            this.context.SaveChanges();
        }


        public DentistAppointment GetById(int id)
        {
            return this.context.DentistAppontments.Find(id);
        }
    }
}
