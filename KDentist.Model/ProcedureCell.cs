using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Model
{
    public class ProcedureCell
    {
        public int Id { get; set; }

        public virtual DateTime Date { get; set; }

        public int? ProcedureTypeLeftId { get; set; }
        [ForeignKey("ProcedureTypeLeftId")]
        public virtual Diagnose ProcedureTypeLeft { get; set; }

        public int? ProcedureTypeRightId { get; set; }
        [ForeignKey("ProcedureTypeRightId")]
        public virtual Diagnose ProcedureTypeRight { get; set; }

        public int? ProcedureTypeTopId { get; set; }
        [ForeignKey("ProcedureTypeTopId")]
        public virtual Diagnose ProcedureTypeTop { get; set; }

        public int? ProcedureTypeBottomId { get; set; }
        [ForeignKey("ProcedureTypeBottomId")]
        public virtual Diagnose ProcedureTypeBottom { get; set; }

        public int? ProcedureTypeCenterId { get; set; }
        [ForeignKey("ProcedureTypeCenterId")]
        public virtual Diagnose ProcedureTypeCenter { get; set; }

        public int TableId { get; set; }

        public virtual ProceduresTable Table { get; set; }

        public int Tooth { get; set; }
    }
}
