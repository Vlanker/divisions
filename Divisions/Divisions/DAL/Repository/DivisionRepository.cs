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
        private DivisionSQL db = DivisionSQL.GetInstance();

        private List<Division> context;

        public List<Division> GetList()
        {
            if (context == null)
            {
                db.Connect(Properties.Settings.Default.connString);
                DataSet data = db.GetDepartaments();

                if (data.Tables[0].Rows.Count != 0)
                {
                    context = new List<Division>();
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        context.Add(new Division { Id = Convert.ToInt32(row["ID"]), Name = Convert.ToString(row["Name"]), ParentId = Convert.ToInt32(row["ParentID"]) });
                    }

                }
            }
            return context;
        }
        public List<Division> GetById(int id)
        {
            return context.Find(d => d.Id == id);
        }


    }
}
