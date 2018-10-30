using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDbQueriesLibrary
{
    class WorkerSQL
    {
        public DataSet GetWorkersd()
        {
            using (DataSet data = new DataSet())
            {
                using (SqlConnection connection = new SqlConnection(DivisionSQL.GetInstance().Connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("Divisions.uspGetWorkers", connection))
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
        public bool Add(int departamentId, string persNum, string fullName, DateTime birthday, DateTime hiringDay, decimal salary, string profArea, string gender)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DivisionSQL.GetInstance().Connection))
            {

                const string sql = "INSERT INTO [Divisions].[Workers] (DepartamentID, PersNum, FullName, Birthday, HiringDay, Salary, ProfArea, Gender) VALUES (@DepartamentID, @PersNum, @FullName, @Birthday, @HiringDay, @Salary, @ProfArea, @Gender)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = departamentId;
                    sqlCommand.Parameters.Add(new SqlParameter("@PersNum", SqlDbType.NVarChar, 50)).Value = persNum;
                    sqlCommand.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 250)).Value = fullName;
                    sqlCommand.Parameters.Add(new SqlParameter("@Birthday", SqlDbType.Date)).Value = birthday;
                    sqlCommand.Parameters.Add(new SqlParameter("@HiringDay", SqlDbType.Date)).Value = hiringDay;
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
        public bool Update(int id, int departamentId, string persNum, string fullName, DateTime birthday, DateTime hiringDay, decimal salary, string profArea, string gender)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DivisionSQL.GetInstance().Connection))
            {
                const string sql = "UPDATE [Divisions].[Workers] SET [DepartamentID] = @DepartamentID, [PersNum] = @PersNum, [FullName] = @Fullname, [Birthday] = @Birthday, [HiringDay] = @HiringDay,  [Salary] = @Salary, [ProfArea] = @ProfArea, [Gender] = @Gender WHERE [WorkerID] = @WorkerID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = departamentId;
                    sqlCommand.Parameters.Add(new SqlParameter("@PersNum", SqlDbType.NVarChar, 50)).Value = persNum;
                    sqlCommand.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 250)).Value = fullName;
                    sqlCommand.Parameters.Add(new SqlParameter("@Birthday", SqlDbType.Date)).Value = birthday;
                    sqlCommand.Parameters.Add(new SqlParameter("@HiringDay", SqlDbType.Date)).Value = hiringDay;
                    sqlCommand.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Money)).Value = salary;
                    sqlCommand.Parameters.Add(new SqlParameter("@ProfArea", SqlDbType.NVarChar, 150)).Value = profArea;
                    sqlCommand.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 4)).Value = gender;
                    sqlCommand.Parameters.Add(new SqlParameter("@WorkerID", SqlDbType.Int)).Value = id;

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
        public bool DeleteById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(DivisionSQL.GetInstance().Connection))
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
    }
}
