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
        List<T> DivisionList(); 
        T GetById(int id);
        void Create(T item);
        void Edit(T item);
        bool Delete(T item);
    }
}
