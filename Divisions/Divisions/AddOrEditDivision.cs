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
using Divisions.DAL.Repository;
using Divisions.DAL;
using Divisions.DAL.View_model;

namespace Divisions
{

    public partial class AddOrEditDivision : Form
    {
        //private int ParentId { get; set; }
        private Division DViewModel { get; set; }
        //Добавление Отделения
        internal AddOrEditDivision()
        {
            InitializeComponent();
                
            btnAddRoot.Visible = true;
            btnAddBranch.Visible = true;
        }
        //Добавление Отделения или отдела/подотдела
        internal AddOrEditDivision(int id) : this()
        {
            DViewModel = new DivisionViewModel(id).Division; 
            btnAddBranch.Enabled = true;
        }
        //изменение
        internal AddOrEditDivision(Division division) : this()
        {
            btnAddRoot.Visible = false;
            btnAddBranch.Visible = false;
            btnUpdate.Visible = true;
            tbName.Text = division.Name;
            DViewModel = division;
        }

        private void btnAddAsRoot_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                string name = tbName.Text;
                new DivisionRepository().Add(name, 0);
            }
            
        }
        private void btnAddBranch_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                string name = tbName.Text;
                int parentId = DViewModel.Id;
                new DivisionRepository().Add(name, parentId);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                DViewModel.Name = tbName.Text;
                new DivisionRepository().Edit(DViewModel);
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
