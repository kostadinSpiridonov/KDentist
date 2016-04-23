using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDentist.Data;

namespace KDentist.Service
{
    public class BaseService
    {
        public ApplicationDbContext context;

        public BaseService()
        {
            context = new ApplicationDbContext();
        }
    }
}
