using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Divisions.DAL.Context;
using LocalDbQueriesLibrary;

namespace Divisions.DAL.Repository
{
    class DivisionRepository : IRepository<Division>
    {
        private DbContext context = new DbContext();

        

        public Division Create(Division divisionToCreate)
        {
            
            return divisionToCreate;
        }

        public void Delete(Division divisionToDelete)
        {
            throw new NotImplementedException();
        }

        public Division Edit(Division sivisionToEdit)
        {
            throw new NotImplementedException();
        }

        public Division GetById(int id)
        {
            return context.Find(d => d.Id == id);
        }

        public IEnumerable<Division> List()
        {
            return context;
        }
    }
}
