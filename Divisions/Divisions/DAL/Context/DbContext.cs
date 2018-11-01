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
        List<Division> divisionList;


        public List<Division> DivisionList()
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

        internal Division Find(int id)
        {
            return divisionList.Find(d => d.Id == id);
        }
    }
}
