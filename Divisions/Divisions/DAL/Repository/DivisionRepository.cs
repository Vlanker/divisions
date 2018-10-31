using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalDbQueriesLibrary;

namespace Divisions.DAL.Repository
{
    class DivisionRepository : IRepository<Division>
    {
        private List<Division> context = null;

        

        public Division Create(Division itemToCreate)
        {
            return 
        }

        public void Delete(Division itemToDelete)
        {
            throw new NotImplementedException();
        }

        public Division Edit(Division itemToEdit)
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
