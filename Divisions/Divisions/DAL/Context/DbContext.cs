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
        private readonly string connection = Properties.Settings.Default.connString;

        public DbContext()
        {
            new DivisionSQL().Connect(connection);
            
        }

        public DataSet DivisionList()
        {
            Math
            return DivisionSQL;
        }

         
    }
}
