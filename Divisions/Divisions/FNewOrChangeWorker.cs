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
        private int workerID;
        private int structureID;

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
            btnComplite.Click -= btnAddWorker_Click;
            btnComplite.Click += btnUpdateWorker_Click;
            workerID                = Convert.ToInt32(selectedRow[0].ToString());
            tbPersNum.Text          = selectedRow[2].ToString();
            tbFullName.Text         = selectedRow[3].ToString();
            dtpBirthday.Value       = DateTime.Parse(selectedRow[4].ToString());
            dtpHiringDay.Value      = DateTime.Parse(selectedRow[5].ToString());
            tbSalary.Text           = selectedRow[6].ToString();
            tbProfArea.Text         = selectedRow[7].ToString();
            cbGender.SelectedIndex  = selectedRow[7].ToString() == "false" ? 0 : 1;
        }

        private void btnUpdateWorker_Click(object sender, EventArgs e)
        {
            if (IsTitlesValid())
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    const string sql = "UPDATE [Offices].[Workers] SET [StructureID] = @StructureID, [PersNum] = @PersNum, [FullName] = @Fullname, [Birthday] = @Birthday, [HiringDay] = @HiringDay,  [Salary] = @Salary, [ProfArea] = @ProfArea, [Gender] = @Gender WHERE [WorkerID] = @WorkerID";

                    using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.Parameters.Add(new SqlParameter("@StructureID", SqlDbType.Int)).Value = FDivisionsNavigation.StructureID;
                        sqlCommand.Parameters.Add(new SqlParameter("@PersNum", SqlDbType.NVarChar, 50)).Value = tbPersNum.Text;
                        sqlCommand.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 250)).Value = tbFullName.Text;
                        sqlCommand.Parameters.Add(new SqlParameter("@Birthday", SqlDbType.Date)).Value = dtpBirthday.Value.ToShortDateString();
                        sqlCommand.Parameters.Add(new SqlParameter("@HiringDay", SqlDbType.Date)).Value = dtpHiringDay.Value.ToShortDateString();
                        sqlCommand.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Money)).Value = GetSalary();
                        sqlCommand.Parameters.Add(new SqlParameter("@ProfArea", SqlDbType.NVarChar, 150)).Value = tbProfArea.Text;
                        sqlCommand.Parameters.Add(new SqlParameter("@Gender", SqlDbType.Bit)).Value = Convert.ToBoolean(cbGender.SelectedIndex);
                        sqlCommand.Parameters.Add(new SqlParameter("@WorkerID", SqlDbType.Int)).Value = workerID;

                        try
                        {
                            connection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("Работник не изменён");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    
                }
                this.Close();
            }
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
                    
                    const string sql = "INSERT INTO [Offices].[Workers] (StructureID, PersNum, FullName, Birthday, HiringDay, Salary, ProfArea, Gender) VALUES (@StructureID, @PersNum, @FullName, @Birthday, @HiringDay, @Salary, @ProfArea, @Gender)";

                    using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        sqlCommand.Parameters.Add(new SqlParameter("@StructureID", SqlDbType.Int)).Value = GetStructureID();
                        sqlCommand.Parameters.Add(new SqlParameter("@PersNum", SqlDbType.NVarChar, 50)).Value = tbPersNum.Text;
                        sqlCommand.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 250)).Value = tbFullName.Text;
                        sqlCommand.Parameters.Add(new SqlParameter("@Birthday", SqlDbType.Date)).Value = dtpBirthday.Value.ToShortDateString();
                        sqlCommand.Parameters.Add(new SqlParameter("@HiringDay", SqlDbType.Date)).Value = dtpHiringDay.Value.ToShortDateString();
                        sqlCommand.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Money)).Value = GetSalary();
                        sqlCommand.Parameters.Add(new SqlParameter("@ProfArea", SqlDbType.NVarChar, 150)).Value = tbProfArea.Text;
                        sqlCommand.Parameters.Add(new SqlParameter("@Gender", SqlDbType.Bit)).Value = Convert.ToBoolean(cbGender.SelectedIndex);

                        try
                        {
                            connection.Open();
                            sqlCommand.ExecuteNonQuery();
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
                this.Close();
            }
            
        }

        private int GetStructureID()
        {
            if(FDivisionsNavigation.StructureID == 0)
            {
                return FDivisionsNavigation.DepartamentIDStructureID[FDivisionsNavigation.DepartamentID]; 
            }
            return FDivisionsNavigation.StructureID;
        }

        private decimal GetSalary()
        {
            if(tbSalary.Text == "" || Regex.IsMatch(tbSalary.Text, @"^\D*$"))
            {
                return 0;
            }
            return Convert.ToDecimal(tbSalary.Text);
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
