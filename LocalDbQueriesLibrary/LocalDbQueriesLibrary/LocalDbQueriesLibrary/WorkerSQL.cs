using System;
using System.Data;
using System.Data.SqlClient;

namespace LocalDbQueriesLibrary
{
    public static class WorkerSQL
    {
        public static DataSet GetWorkers(int departamentId)
        {
            using (DataSet data = new DataSet())
            {
                using (SqlConnection connection = new SqlConnection(DivisionSQL.Connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("Divisions.uspGetWorkers", connection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = departamentId;
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
        public static int Add(int divisionId, string persNum, string fullName, string birthday, string hiringDay, decimal salary, string profArea, string gender)
        {
            int result = -1;

            using (SqlConnection connection = new SqlConnection(DivisionSQL.Connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand sqlCommand = new SqlCommand("Divisions.uspAddWorker", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = divisionId;
                        sqlCommand.Parameters.Add(new SqlParameter("@PersNum", SqlDbType.NVarChar, 50)).Value = persNum;
                        sqlCommand.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 250)).Value = fullName;
                        sqlCommand.Parameters.Add(new SqlParameter("@Birthday", SqlDbType.Date)).Value = birthday;
                        sqlCommand.Parameters.Add(new SqlParameter("@HiringDay", SqlDbType.Date)).Value = hiringDay;
                        sqlCommand.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Money)).Value = salary;
                        sqlCommand.Parameters.Add(new SqlParameter("@ProfArea", SqlDbType.NVarChar, 150)).Value = profArea;
                        sqlCommand.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 4)).Value = gender;

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
        public static bool Update(int id, string persNum, string fullName, DateTime birthday, DateTime hiringDay, decimal salary, string profArea, string gender)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DivisionSQL.Connection))
            {
                const string sql = "UPDATE [Divisions].[Workers] SET [PersNum] = @PersNum, [FullName] = @Fullname, [Birthday] = @Birthday, [HiringDay] = @HiringDay,  [Salary] = @Salary, [ProfArea] = @ProfArea, [Gender] = @Gender WHERE [ID] = @ID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = id;
                    sqlCommand.Parameters.Add(new SqlParameter("@PersNum", SqlDbType.NVarChar, 50)).Value = persNum;
                    sqlCommand.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 250)).Value = fullName;
                    sqlCommand.Parameters.Add(new SqlParameter("@Birthday", SqlDbType.Date)).Value = birthday.ToShortDateString();
                    sqlCommand.Parameters.Add(new SqlParameter("@HiringDay", SqlDbType.Date)).Value = hiringDay.ToShortDateString();
                    sqlCommand.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Money)).Value = salary;
                    sqlCommand.Parameters.Add(new SqlParameter("@ProfArea", SqlDbType.NVarChar, 150)).Value = profArea;
                    sqlCommand.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 4)).Value = gender;
                    

                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
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

            using (SqlConnection connection = new SqlConnection(DivisionSQL.Connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    const string sql = "DELETE FROM [Divisions].[Workers] WHERE [ID] = @ID";

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
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
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
        public static bool DeleteByDivisiontId(int divisionID)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DivisionSQL.Connection))
            {
                const string sql = "DELETE FROM [Divisions].[Workers] WHERE [DepartamentID] = @DepartamentID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = divisionID;


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
