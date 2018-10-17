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
        private static int desedantID;
        //private static int lvl;

        public FNewOrChangeDivision()
        {
            InitializeComponent();
            desedantID = FDivisionsNavigation.DetartamentID;
        }

        public FNewOrChangeDivision(bool isTVDivisionsNodesCounMoreZero) : this()
        {
            

            //if (!isTVDivisionsNodesCounMoreZero)
            //{
            //    btnAddRoot.Enabled = true;
            //    btnAddRoot.Visible = true;
            //    btnAddBranch.Enabled = false;
            //    btnAddBranch.Visible = true;
            //}
            //else
            //{
            //    btnAddRoot.Enabled = true;
            //    btnAddRoot.Visible = true;
            //    btnAddBranch.Enabled = true;
            //    btnAddBranch.Visible = true;
            //}

            //btnChange.Visible = false;

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
            }
            this.Close();
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
            /*NOP*/
            this.Close();

        }
    }
}
