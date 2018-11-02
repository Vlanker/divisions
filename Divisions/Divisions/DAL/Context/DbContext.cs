using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalDbQueriesLibrary;

namespace Divisions.DAL.Context
{
    class DbContext
    {
        private DivisionSQL divisionToSQL = DivisionSQL.Connect(Properties.Settings.Default.connString);
        private List<Division> divisionList;

        internal List<Division> DivisionList()
        {
            using (DataSet ds = DivisionSQL.GetDivisions())
            {
                divisionList = ds.Tables[0].AsEnumerable().Select(dataRow => new Division
                {
                    Id = dataRow.Field<int>("ID"),
                    Name = dataRow.Field<string>("Name"),
                    ParentId = dataRow.Field<int>("ParentID")
                }).ToList();
            }
            return divisionList;
        }
        internal List<Division> GetDivisions()
        {
            return divisionList;
        }
        internal void Edit(Division division)
        {
            if (DivisionSQL.Update(division.Id, division.Name))
            {
                divisionList.Where(d => d.Id == division.Id).First().Name = division.Name;
            }
                   
        }

        internal bool Remove(Division division)
        {
            if (WorkerSQL.DeleteByDivisiontId(division.Id) && DivisionSQL.DeleteByParentId(division.Id) && DivisionSQL.DeleteById(division.Id))
            {
                
                return divisionList.Remove(division);
            }
            return divisionList.Remove(division);
        }

        internal Division Find(int id)
        {
            return divisionList.Find(d => d.Id == id);
        }

        internal void Add(Division division)
        {
            if (DivisionSQL.Add(division.Name, division.ParentId))
            {
                divisionList.Add(division);
            }
        }

    }
}
