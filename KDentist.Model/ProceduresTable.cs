using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Model
{
    public class ProceduresTable
    {
        [Key,ForeignKey("Patient")]
        public int Id { get; set; }

        public virtual Patient Patient { get; set; }

        private ICollection<ProcedureCell> cells;

        public virtual ICollection<ProcedureCell> Cells
        {
            get
            {
                return this.cells;
            }
            set
            {
                this.cells = value;
            }
        }
    }
}
