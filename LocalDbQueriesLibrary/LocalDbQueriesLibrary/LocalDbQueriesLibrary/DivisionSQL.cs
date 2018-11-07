using System;
using System.Data.SqlClient;
using System.Data;

namespace LocalDbQueriesLibrary
{
    public class DivisionSQL 
    {
        private const string CON_STR = @"Data Source=(LocalDb)\MSSQLLocalDb;Initial Catalog=Divisions;Integrated Security=True;Pooling=True";
        private const string DB_NAME = "Divisions";
        private const string TBL_NAME = "Departaments";

        private const string USP_GET = "uspGetDepartaments";
        private const string USP_ADD = "uspAddDepartament";

        private const string COL_ID = "ID";
        private const string COL_NAME = "Name";
        private const string COL_PARENTID = "ParentID";

        
        private static DivisionSQL divisionToSQL;

        private DivisionSQL()
        {
           
        }

        public static DataSet GetDivisions()
        {
            using (DataSet data = new DataSet())
            {
                using (SqlConnection connection = new SqlConnection(CON_STR))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand($"{DB_NAME}.{USP_GET}", connection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            adapter.SelectCommand = sqlCommand;

                            try
                            {
                                connection.Open();
                                adapter.SelectCommand.ExecuteNonQuery();

                                adapter.Fill(data);
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
                return data;
            }
        }
        public static int Add(string name, int parentId)
        {
            int result = -1;

            using (SqlConnection connection = new SqlConnection(CON_STR))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand sqlCommand = new SqlCommand($"{DB_NAME}.{USP_ADD}", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 40)).Value = name;
                        sqlCommand.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int)).Value = parentId;
                        sqlCommand.Parameters.Add(new SqlParameter("@RC", SqlDbType.Int)).Direction = ParameterDirection.ReturnValue;
                        adapter.InsertCommand = sqlCommand;

                        try
                        {
                            connection.Open();
                            adapter.InsertCommand.ExecuteNonQuery();
                            
                            result = (int)adapter.InsertCommand.Parameters["@RC"].Value;
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

            return result;
        }
        public static bool Update(int id, string name)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(CON_STR))
            {
                const string sql = "UPDATE [Division].[Departament] SET [Name] = @Name WHERE [ID] = @ID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = id;
                    sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 40)).Value = name;

                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }

            return result;
        }
        public static bool DeleteById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(CON_STR))
            {

                const string sql = "DELETE FROM [Divisions].[Departaments] WHERE [ID] = @ID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = id;


                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }

            return result;
        }
    }
}