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
    class DivisionRepository
    {
        private DbContext context;
               
        public DivisionRepository()
        {
            context = DbContext.Context;
        }

        internal List<Division> DivisionList()
        {
            return context.GetDivisions();
        }
        //internal Division Division(int id)
        //{
        //    return context.GetDivisions().Find(d => d.Id == id);
        //}
        internal void Add(string name, int parentId)
        {
            context.Add(name, parentId);
        }
        internal void Remove(Division division)
        {
            context.Remove(division);
        }
        internal void Edit(Division division)
        {
            context.Edit(division);
        }
    }
}
