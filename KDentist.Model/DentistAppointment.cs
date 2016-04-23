using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Model
{
    public class DentistAppointment
    {
        public int Id { get; set; }

        public virtual DateTime DateTime { get; set; }

        public int? PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public string Note { get; set; }

        public string UnknowPatientName{ get; set; }
    }
}
