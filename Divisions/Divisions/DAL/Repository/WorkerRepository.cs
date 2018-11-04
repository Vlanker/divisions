using Divisions.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisions.DAL.Repository
{
    class WorkerRepository
    {
        private DbContext context;

        public WorkerRepository()
        {
            context = DbContext.Context;
        }

        internal List<Worker> WorkerList(int divisionId)
        {
            
            return context.GetWorkers(divisionId);
        }
        
        internal void Add(int divisionId, string persNum, string fullName, DateTime birthday, DateTime hiringDay, decimal salary, string profArea, string gender)
        {
            context.Add(divisionId, persNum, fullName, birthday, hiringDay, salary, profArea, gender); 
        }
        internal bool Remove(Worker worker)
        {
            return context.Remove(worker);
        }
        internal void Edit(Worker worker)
        {
            context.Edit(worker);
        }
    }
}
