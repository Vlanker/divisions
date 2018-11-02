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
        private DbContext context;
        private static  DivisionRepository repos;
        private DivisionRepository()
        {
            context = new DbContext();
        }

        internal static DivisionRepository GetRepository()
        {
            if (repos == null)
            {
                repos = new DivisionRepository();
            }
            return repos;
        }
        public void Create(Division division)
        {
            context.Add(division);
        }
        public bool Delete(Division division)
        {
            return context.Remove(division);
        }
        public void Edit(Division division)
        {
             context.Edit(division);
        }
        public Division GetById(int id)
        {
            return context.Find(id);
        }
        public List<Division> DivisionList()
        {
            return context.DivisionList();
        }
        public List<Division> GetDivisions()
        {
            return context.GetDivisions();
        }

        internal void Create(string name)
        {
            throw new NotImplementedException();
        }

        internal void Create(string name, int parentid)
        {
            throw new NotImplementedException();
        }

        internal void Edit(string name)
        {
            throw new NotImplementedException();
        }
    }
}
