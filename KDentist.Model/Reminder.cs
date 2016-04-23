using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Model
{
    public class Reminder
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public virtual DateTime ToDate { get; set; }

        public bool Completed { get; set; }
    }
}
