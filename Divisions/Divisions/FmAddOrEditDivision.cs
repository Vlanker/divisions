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

namespace Divisions
{

    public partial class FmAddOrEditDivision : Form
    {

        private int parentId { get; set; }
        public FmAddOrEditDivision()
        {
            InitializeComponent();
                
            btnAddRoot.Visible = true;
            btnAddBranch.Visible = true;
        }

        public FmAddOrEditDivision(int idSelectedNode): this()
        {
            this.parentId = idSelectedNode;
            btnAddBranch.Enabled = true;
        }

        public FmAddOrEditDivision(int idSelectedNode, string nameSelectedNode) : this()
        {
            btnAddRoot.Visible = false;
            btnAddBranch.Visible = false;
            btnUpdate.Visible = true;
            tbName.Text = nameSelectedNode;
        }

        private void btnAddAsRoot_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                string name = tbName.Text;
                DivisionRepository.GetRepository().Create(name);
            }
            
        }
        private void btnAddBranch_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                string name = tbName.Text;
                DivisionRepository.GetRepository().Create(name, parentId);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsTitleValid())
            {
                string name = tbName.Text;
                DivisionRepository.GetRepository().Edit(name);
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
