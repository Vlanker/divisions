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
using System.Text.RegularExpressions;

namespace Divisions
{
    public partial class FNewOrChangeWorker : Form
    {
        public FNewOrChangeWorker()
        {
            InitializeComponent();
            btnComplite.Click += btnAddWorker_Click;
            dtpBirthday.ValueChanged += new System.EventHandler(dtpBirthday_ValueChanged);
            dtpBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpBirthday.CustomFormat = " ";
            cbGender.SelectedIndex = 0;
        }

        public FNewOrChangeWorker(object[] selectedRow): this()
        {
            btnComplite.Click += btnUpdateWorker_Click;
            tbPersNum.Text    = selectedRow[2].ToString();
            tbFullName.Text   = selectedRow[3].ToString();
            dtpBirthday.Text  = selectedRow[4].ToString();
            dtpHiringDay.Text = selectedRow[5].ToString();
            tbSalary.Text     = selectedRow[6].ToString();
            tbProfArea.Text   = selectedRow[7].ToString();
            cbGender.SelectedIndex = Convert.ToInt32(selectedRow[7].ToString());



        }

        private void btnUpdateWorker_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dtpBirthday_ValueChanged(object sender, EventArgs e)
        {
            if (dtpBirthday.Format == System.Windows.Forms.DateTimePickerFormat.Custom)
            {
                dtpBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            }
        }

        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            if (IsTitlesValid())
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        const string sql = "INSERT INTO [Offices].[Workers] (StructureID, PersNum, FullName, Birthday, HiringDay, Salary, ProfArea, Gender) VALUES (@StructureID, @PersNum, @FullName, @Birthday, @HiringDay, @Salary, @ProfArea, @Gender)";

                        using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                        {
                            sqlCommand.Parameters.Add(new SqlParameter("@StructureID", SqlDbType.Int)).Value = FDivisionsNavigation.StructureID;
                            sqlCommand.Parameters.Add(new SqlParameter("@PersNum", SqlDbType.NVarChar, 50)).Value = tbPersNum.Text;
                            sqlCommand.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 250)).Value = tbFullName.Text;
                            sqlCommand.Parameters.Add(new SqlParameter("@Birthday", SqlDbType.Date)).Value = dtpBirthday.Value.ToShortDateString();
                            sqlCommand.Parameters.Add(new SqlParameter("@HiringDay", SqlDbType.Date)).Value = dtpHiringDay.Value.ToShortDateString();
                            sqlCommand.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Money)).Value = GetSalary();
                            sqlCommand.Parameters.Add(new SqlParameter("@ProfArea", SqlDbType.NVarChar, 150)).Value = tbProfArea.Text;
                            sqlCommand.Parameters.Add(new SqlParameter("@Gender", SqlDbType.Bit)).Value = Convert.ToBoolean(cbGender.SelectedIndex);

                            adapter.InsertCommand = sqlCommand;

                            try
                            {
                                connection.Open();
                                adapter.InsertCommand.ExecuteNonQuery();
                            }
                            catch 
                            {
                                MessageBox.Show("Работник не добавлен.");
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

        private int GetSalary()
        {
            if(tbSalary.Text == "" || Regex.IsMatch(tbSalary.Text, @"^\D*$"))
            {
                return 0;
            }
            return Int32.Parse(tbSalary.Text);
        }

        private bool IsTitlesValid()
        {
            if(tbPersNum.Text == ""  ||
               tbFullName.Text == "" || 
               dtpBirthday.Format == System.Windows.Forms.DateTimePickerFormat.Custom)
            {
                MessageBox.Show("Введите Табельный номер, ФИО и выберите Дату рождения.");
                return false;
            }
            return true;
        }
    }
}
