using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Divisions.DAL.Repository;
using Divisions.DAL;
using Divisions.DAL.View_model;

namespace Divisions
{

    public partial class AddOrEditDivision : Form
    {
        private Division DivisionView { get; set; }
        private DivisionRepository divisionRepo = new DivisionRepository();
        //Добавление Отделения
        internal AddOrEditDivision()
        {
            InitializeComponent();
                
            btnAddRoot.Visible = true;
            btnAddBranch.Visible = true;
        }
        //Добавление Отделения или отдела/подотдела
        internal AddOrEditDivision(int divisionId) : this()
        {
            DivisionView = new DivisionViewModel(divisionId).Division; 
            btnAddBranch.Enabled = true;
        }
        //изменение
        internal AddOrEditDivision(Division division) : this()
        {
            btnAddRoot.Visible = false;
            btnAddBranch.Visible = false;
            btnUpdate.Visible = true;
            tbName.Text = division.Name;
            DivisionView = division;
        }

        private void btnAddAsRoot_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                string name = tbName.Text;
                divisionRepo.Add(name, 0);
            }
            
        }
        private void btnAddBranch_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                string name = tbName.Text;
                int parentId = DivisionView.Id;
                divisionRepo.Add(name, parentId);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                DivisionView.Name = tbName.Text;
                divisionRepo.Edit(DivisionView);
                this.Close();
            }
        }
        private bool IsTitleValid()
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите Название");
                return false;
            }
            return true;
        }
    }
}
