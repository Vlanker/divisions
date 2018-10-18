using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Divisions
{

    public partial class FNewOrChangeDivision : Form
    {
        private int departamentID;
        private int lvl;

        public FNewOrChangeDivision()
        {
            InitializeComponent();
            
                btnAddRoot.Enabled = true;
                btnAddRoot.Visible = true;
                btnAddBranch.Enabled = false;
                btnAddBranch.Visible = true;
                
        }

        public FNewOrChangeDivision(int departamentID, int lvl): this()
        {
            this.departamentID = departamentID;
            this.lvl = lvl;
            lblTitle.Text = "Добавить отдел/ подотдел в " + FDivisionsNavigation.Title;
            btnAddBranch.Enabled = true;
        }

        private void btnAddRoot_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("Offices.uspInsertRoot", connection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            sqlCommand.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50));
                            sqlCommand.Parameters["@Title"].Value = tbTitle.Text;

                            adapter.InsertCommand = sqlCommand;

                            try
                            {
                                connection.Open();
                                adapter.InsertCommand.ExecuteNonQuery();
                               
                            }
                            catch
                            {
                                MessageBox.Show("Отделение не создано.");
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                    }
                }
                this.Close();
            }
            
        }

        private bool IsTitleValid()
        {
            if(tbTitle.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите Название");
                return false;
            }
            return true;
        }

        private void btnAddBranch_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("Offices.uspInsertBranch", connection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            sqlCommand.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50));
                            sqlCommand.Parameters["@Title"].Value = tbTitle.Text;
                            sqlCommand.Parameters.Add(new SqlParameter("@DesedantID", SqlDbType.Int));
                            sqlCommand.Parameters["@DesedantID"].Value = FDivisionsNavigation.DepartamentID;
                            sqlCommand.Parameters.Add(new SqlParameter("@Level", SqlDbType.Int));
                            sqlCommand.Parameters["@Level"].Value = FDivisionsNavigation.Lvl + 1;

                            adapter.InsertCommand = sqlCommand;

                            try
                            {
                                connection.Open();
                                adapter.InsertCommand.ExecuteNonQuery();

                            }
                            catch
                            {
                                MessageBox.Show("Отделение не создано.");
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                    }
                }
                this.Close();
            }

        }
    }
}
