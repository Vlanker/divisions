using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisions.DAL.Repository
{
    interface IRepository<T>
        where T : class
    {
        IEnumerable<T> List(); 
        T GetById(int id);
        T Create(T itemToCreate);
        T Edit(T itemToEdit);
        void Delete(T itemToDelete);
    }
}
