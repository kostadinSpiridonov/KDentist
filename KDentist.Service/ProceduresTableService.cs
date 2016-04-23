using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IProceduresTableService
    {
        void Add(ProceduresTable model);
        ProceduresTable GetById(int id);
    }

    public class ProceduresTableService:BaseService,IProceduresTableService
    {
        public void Add(ProceduresTable model)
        {
            this.context.ProceduresTables.Add(model);
            this.context.SaveChanges();
        }

        public ProceduresTable GetById(int id)
        {
            return this.context.ProceduresTables.Find(id);
        }
    }
}
