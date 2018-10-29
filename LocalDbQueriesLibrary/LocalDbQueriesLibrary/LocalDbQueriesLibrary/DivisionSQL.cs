using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace LocalDbQueriesLibrary
{
    public class DivisionSQL
    {
        private string Connection { get; set; }

        public DivisionSQL(string connection)
        {
            this.Connection = connection;
        }

        public DataSet GetDepartaments()
        {
            DataSet resultData = new DataSet();

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand sqlCommand = new SqlCommand("Divisions.uspGetAllDepartaments", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand = sqlCommand;

                        try
                        {
                            connection.Open();
                            adapter.SelectCommand.ExecuteNonQuery();

                            adapter.Fill(resultData);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }

            return resultData;
        }
    }
}