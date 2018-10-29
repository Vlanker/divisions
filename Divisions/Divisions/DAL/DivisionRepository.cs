using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalDbQueriesLibrary;

namespace Divisions
{
    class DivisionRepository
    {
        private DivisionSQL db = new DivisionSQL(Properties.Settings.Default.connString);

        private List<Division> divisions = new List<Division>();

        public List<Division> GetDivisionsList()
        {
            DataSet data = db.GetDepartaments();

            if (data.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    divisions.Add(new Division { Id = Convert.ToInt32(row["DepartamentID"]), Name = Convert.ToString(row["DepartamentName"]), ParentId = Convert.ToInt32(row["ParentID"]) });
                }

            }
            return divisions;
        }


    }
}
