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
using Divisions.DAL;
using Divisions.DAL.View_model;
using Divisions.DAL.Repository;

namespace Divisions
{
    public partial class AddOrEditWorker : Form
    {
        private int divisionId;
        private Worker WorkerView { get; set; }

        internal AddOrEditWorker()
        {
            InitializeComponent();
        }
        //добавление нового
        internal AddOrEditWorker(int divisionId): this()
        {
            this.divisionId = divisionId;
            btnComplite.Click += btnAddWorker_Click;
            dtpBirthday.ValueChanged += new System.EventHandler(dtpBirthday_ValueChanged);
            dtpBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpBirthday.CustomFormat = " ";
            cbGender.SelectedIndex = 0;
        }
        //изменение
        internal AddOrEditWorker(Worker worker) : this()
        {
            WorkerView = worker;
            btnComplite.Click -= btnAddWorker_Click;
            btnComplite.Click += btnUpdateWorker_Click;
            FillComponents();
           
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
                string persNum = tbPersNum.Text;
                string fullName = tbFullName.Text;
                string birthday = dtpBirthday.Value.ToShortDateString();
                string hiringDay = dtpHiringDay.Value.ToShortDateString();
                decimal salary = Convert.ToDecimal(tbSalary.Text);
                string profArea = tbProfArea.Text;
                string gender = cbGender.Text;
                new WorkerRepository().Add(divisionId, persNum, fullName, birthday, hiringDay, salary, profArea, gender);
            }
        }
        private void btnUpdateWorker_Click(object sender, EventArgs e)
        {
            if (IsTitlesValid())
            {

                WorkerView.PersNum = tbPersNum.Text;
                WorkerView.FullName = tbFullName.Text;
                WorkerView.Birthday = Convert.ToDateTime(dtpBirthday.Value.ToShortDateString());
                WorkerView.HiringDay = Convert.ToDateTime(dtpHiringDay.Value.ToShortDateString());
                WorkerView.Salary = Convert.ToDecimal(tbSalary.Text);
                WorkerView.ProfArea = tbProfArea.Text;
                WorkerView.Gender = cbGender.Text;
                new WorkerRepository().Edit(WorkerView);
                this.Close();
            }
        }

        private bool IsTitlesValid()
        {
            if (tbPersNum.Text == "" ||
               tbFullName.Text == "" ||
               dtpBirthday.Format == System.Windows.Forms.DateTimePickerFormat.Custom)
            {
                MessageBox.Show("Введите Табельный номер, ФИО и выберите Дату рождения.");
                return false;
            }
            if (tbSalary.Text == "" || Regex.IsMatch(tbSalary.Text, @"^\D*$"))
            {
                MessageBox.Show("Введите зарплату.");
                return false;
            }

            return true;
        }
        private void FillComponents()
        {
            tbPersNum.Text = WorkerView.PersNum;
            tbFullName.Text = WorkerView.FullName;
            dtpBirthday.Value = WorkerView.Birthday;
            dtpHiringDay.Value = WorkerView.HiringDay;
            tbSalary.Text = WorkerView.Salary.ToString();
            tbProfArea.Text = WorkerView.ProfArea;
            cbGender.Text = WorkerView.Gender;
        }
       }
}
