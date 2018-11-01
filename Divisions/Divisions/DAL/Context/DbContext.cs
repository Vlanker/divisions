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
        private DataSet divisionSet { get; set; }
        private DataSet workerSet { get; set; }

        private DivisionSQL divisionToSQL = new DivisionSQL();
        private WorkerSQL workerToSQL = new WorkerSQL();
        public DbContext()
        {
            divisionToSQL.Connect(Properties.Settings.Default.connString);
            divisionSet = divisionToSQL.GetDepartaments();
            workerSet = workerToSQL.GetWorkers();
        }

        public DataSet DivisionList()
        {
            return divisionSet;
        }

         
    }
}
