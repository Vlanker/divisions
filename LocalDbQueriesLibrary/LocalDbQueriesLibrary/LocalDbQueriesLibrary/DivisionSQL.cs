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
        internal static string Connection { get; set; }
        
        public DivisionSQL()
        {
        }
        public void Connect(string connection)
        {
            Connection = connection;
        }
        public DataSet GetDepartaments()
        {
            using (DataSet resultData = new DataSet())
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("Divisions.uspGetDepartaments", connection))
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
        public bool Add(string name, int parentId)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand sqlCommand = new SqlCommand("Divisions.uspAddDepartament", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 40)).Value = name;
                        sqlCommand.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int)).Value = parentId;

                        adapter.InsertCommand = sqlCommand;

                        try
                        {
                            connection.Open();
                            adapter.InsertCommand.ExecuteNonQuery();
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
            }

            return result;
        }
        public bool Update(int id, string name)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                const string sql = "UPDATE [Divisions].[Departaments] SET [Name] = @Name WHERE [ID] = @ID";

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
        public bool DeleteById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    const string sql = "DELETE FROM [Divisions].[Departaments] WHERE [ID] = @ID";

                    using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = id;

                        adapter.DeleteCommand = sqlCommand;
                        try
                        {
                            connection.Open();
                            adapter.DeleteCommand.ExecuteNonQuery();
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
            }

            return result;
        }
        public bool DeleteByParentId(int parentId)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    const string sql = "DELETE FROM [Divisions].[Departaments] WHERE [ParentID] = @ParentID";

                    using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int)).Value = parentId;

                        adapter.DeleteCommand = sqlCommand;
                        try
                        {
                            connection.Open();
                            adapter.DeleteCommand.ExecuteNonQuery();
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
            }

            return result;
        }
    }
}