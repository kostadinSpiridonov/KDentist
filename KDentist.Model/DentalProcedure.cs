using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Model
{
    public class DentalProcedure
    {
        public int Id { get; set; }

        public string Tooth { get; set; }

        public string Diagnosis { get; set; }

        public string Note { get; set; }

        public virtual DateTime Date { get; set; }

        public int? MDTId { get; set; }

        [ForeignKey("MDTId")]
        public virtual MDT MDT { get; set; }

        public int? PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }

        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }

        public decimal PaidValue { get; set; }
    }
}
