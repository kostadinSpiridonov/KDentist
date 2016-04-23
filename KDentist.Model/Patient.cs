using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Model
{
    public class Patient
    {
        public Patient()
        {
            dentalProcedures = new HashSet<DentalProcedure>();
            diseases = new HashSet<Disease>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string EGN { get; set; }

        [Required]
        public int Age { get; set; }

        public string Profession { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        private ICollection<DentalProcedure> dentalProcedures;

        public virtual ICollection<DentalProcedure> DentalProcedures
        {
            get
            {
                return this.dentalProcedures;
            }
            set
            {
                this.dentalProcedures = value;
            }
        }

        private ICollection<Disease> diseases;

        public virtual ICollection<Disease> Diseases
        {
            get
            {
                return this.diseases;
            }
            set
            {
                this.diseases = value;
            }
        }

        public virtual ProceduresTable ProceduresTable { get; set; }
    }
}
