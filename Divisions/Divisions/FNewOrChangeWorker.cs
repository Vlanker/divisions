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
    public partial class FNewOrChangeWorker : Form
    {
        private int structureID;
        string[] valuesWorkers;
        public FNewOrChangeWorker()
        {
            InitializeComponent();
        }

        public FNewOrChangeWorker(int structureID):this()
        {
            this.structureID = structureID;
            btnComplite.Click += btnAddFinishWorker_ClickInsert;
        }
        public FNewOrChangeWorker(string[] valuesWorkers) : this()
        {
            this.valuesWorkers = valuesWorkers;
            btnComplite.Click += btnAddFinishWorker_ClickUpdate;
        }

        private void btnAddFinishWorker_ClickUpdate(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAddFinishWorker_ClickInsert(object sender, EventArgs e)
        {
           // if (IsTitlesValid())
            //{
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = new SqlCommand("INSERT INTO [Offices].[Worker] (PersNum, FullName, Birthday, HiringDay, Salary, ProfArea, Gender) " +
                                                               "VALUE(@StructureID, @PersNum, @FullName, @Birthday, @HiringDay, @Salary, @ProfArea, @Gender)");
                        adapter.InsertCommand.Parameters.Add("@StructureID", SqlDbType.Int).Value = structureID;
                        adapter.InsertCommand.Parameters.Add("@PersNum", SqlDbType.NVarChar, 50).Value = tbPersNum;
                        adapter.InsertCommand.Parameters.Add("@FullName", SqlDbType.NVarChar, 50).Value = tbFullName;
                        adapter.InsertCommand.Parameters.Add("@Birthday", SqlDbType.Date).Value = dtpBirthday;
                        adapter.InsertCommand.Parameters.Add("@HiringDay", SqlDbType.Date).Value = dtpHiringDay;
                        adapter.InsertCommand.Parameters.Add("@Salary", SqlDbType.Money).Value = tbSalary;
                        adapter.InsertCommand.Parameters.Add("@ProfArea", SqlDbType.NVarChar, 250).Value = tbProfArea;
                        adapter.InsertCommand.Parameters.Add("@Gender", SqlDbType.Bit).Value = cbGender;

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
            //}
        }
    }
}
