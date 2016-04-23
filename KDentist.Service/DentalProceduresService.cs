using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IDentalProceduresService
    {
        void Add(DentalProcedure model);
        ICollection<DentalProcedure> GetAll();
        bool Exist(int id);
        void Remove(int id);
        void Update(DentalProcedure model);
        DentalProcedure GetById(int id);
        bool DoctorHasProcedures(int doctorId);
        bool ActivityHasProcedures(int activityId);
    }

    public class DentalProceduresService:BaseService,IDentalProceduresService
    {
        public void Add(DentalProcedure model)
        {
            this.context.DentalProcedures.Add(model);
            this.context.SaveChanges();
        }

        public ICollection<DentalProcedure> GetAll()
        {
            return this.context.DentalProcedures.ToList();
        }


        public bool Exist(int id)
        {
            return this.context.DentalProcedures.Any(x=>x.Id==id);
        }


        public void Remove(int id)
        {
            var procedure = this.context.DentalProcedures.Find(id);
            this.context.DentalProcedures.Remove(procedure);
            this.context.SaveChanges();
        }


        public void Update(DentalProcedure model)
        {
            var procedure = this.context.DentalProcedures.Find(model.Id);

            procedure.ActivityId= model.ActivityId;
            procedure.Date = model.Date;
            procedure.Diagnosis = model.Diagnosis;
            procedure.DoctorId = model.DoctorId;
            procedure.MDTId = model.MDTId;
            procedure.Note = model.Note;
            procedure.Tooth = model.Tooth;
            procedure.MDTId = model.MDTId;
            procedure.PaidValue = model.PaidValue;

            this.context.SaveChanges();
        }


        public DentalProcedure GetById(int id)
        {
            return this.context.DentalProcedures.Find(id);
        }


        public bool DoctorHasProcedures(int doctorId)
        {
            return this.context.DentalProcedures.Any(x => x.DoctorId == doctorId);
        }


        public bool ActivityHasProcedures(int activityId)
        {
            return this.context.DentalProcedures.Any(x => x.ActivityId == activityId);
        }
    }
}
