using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Service
{
    public interface IProcedureCellService
    {
        void Add(ProcedureCell model);
        ICollection<ProcedureCell> GetForTable(int tableId);
        void Update(ProcedureCell model);
        ProcedureCell GetById(int id);
        void Remove(int id);
    }

    public class ProcedureCellService : BaseService, IProcedureCellService
    {
        public void Add(ProcedureCell model)
        {
            this.context.ProceduresCells.Add(model);
            this.context.SaveChanges();
        }

        public ICollection<ProcedureCell> GetForTable(int tableId)
        {
            return this.context.ProceduresCells.Where(x => x.TableId == tableId).ToList();
        }


        public void Update(ProcedureCell model)
        {
            var cell = this.context.ProceduresCells.Find(model.Id);
            cell.ProcedureTypeLeftId = model.ProcedureTypeLeftId;
            cell.ProcedureTypeRightId = model.ProcedureTypeRightId;
            cell.ProcedureTypeTopId = model.ProcedureTypeTopId;
            cell.ProcedureTypeBottomId = model.ProcedureTypeBottomId;
            cell.ProcedureTypeCenterId = model.ProcedureTypeCenterId;
            this.context.SaveChanges();
        }


        public ProcedureCell GetById(int id)
        {
            return this.context.ProceduresCells.Find(id);
        }


        public void Remove(int id)
        {
            var cell = this.context.ProceduresCells.Find(id);
            this.context.ProceduresCells.Remove(cell);
            this.context.SaveChanges();
        }
    }
}
