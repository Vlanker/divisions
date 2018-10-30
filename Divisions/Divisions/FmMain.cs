using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Divisions.DAL.Repository;

namespace Divisions
{
    public partial class FmMain : Form
    {

        DivisionRepository repos = new DivisionRepository();

        public FmMain()
        {
            InitializeComponent();

            InitializeTVDivisions(repos.GetList(), null);
            tvDivisions.AfterSelect += tvDivisions_AfterSelect;

            if (tvDivisions.Nodes.Count == 0)
            {
                btnChangeDivision.Enabled = false;
                btnDeleteDivision.Enabled = false;
            }

            btnChangeDivision.Enabled = true;
            btnDeleteDivision.Enabled = true;
        }

        private void tvDivisions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Convert.ToInt32(e.Node.Tag) == 0)
            {
                dgvWorkers.DataSource = null;
                btnCreateWorker.Enabled = false;
                btnChangeWorker.Enabled = false;
                btnDeleteWorker.Enabled = false;
                //GetAllWorkerOfDepartamentsSQL();
                return;
            }

            lb.Text = $"Работники: {e.Node.Text}";
            btnCreateWorker.Enabled = true;
            btnChangeWorker.Enabled = true;
            btnDeleteWorker.Enabled = true;
            //GetWorkersSQL();
        }

        private void InitializeTVDivisions(List<Division> items, TreeNode node)
        {
            var parentID = node != null ? (int)node.Tag : 0;

            var nodeCollection = node != null ? node.Nodes : tvDivisions.Nodes;

            foreach (var item in items.Where(d => d.ParentId == parentID))

            {
                var newNode = nodeCollection.Add(item.Name);
                newNode.Tag = item.Id;

                InitializeTVDivisions(items, newNode);
            }
        }

        private void FormatDVGWorkers()
        {
            dgvWorkers.Columns["WorkerID"].Visible = false;
            dgvWorkers.Columns["StructureID"].Visible = false;
            dgvWorkers.Columns["PersNum"].HeaderText = "Табельный номер";
            dgvWorkers.Columns["FullName"].HeaderText = "ФИО";
            dgvWorkers.Columns["Birthday"].HeaderText = "Дата рождения";
            dgvWorkers.Columns["HiringDay"].HeaderText = "Дата приёма";
            dgvWorkers.Columns["Salary"].HeaderText = "Зарплата";
            dgvWorkers.Columns["ProfArea"].HeaderText = "Профобласть";
            dgvWorkers.Columns["Gender"].HeaderText = "Пол";
            
        }


        private void btnCreateDivision_Click(object sender, EventArgs e)
        {
            //if (tvDivisions.SelectedNode == null)
            //{
            //    Form frmNewRoot = new FNewOrChangeDivision();
            //    frmNewRoot.ShowDialog();
            //    FillTVDivisions();
            //    return;
            //}
            //Form frmNewDivElement = new FNewOrChangeDivision(DepartamentID, Lvl);
            //frmNewDivElement.ShowDialog();
            //FillTVDivisions();
        }

        private void btnChangeDivision_Click(object sender, EventArgs e)
        {
            //Form updateDepartament = new FNewOrChangeDivision(DepartamentID, Title, Lvl);
            //updateDepartament.ShowDialog();
            //FillTVDivisions();
        }

        private void btnCreateWorker_Click(object sender, EventArgs e)
        {
            Form frm = new FNewOrChangeWorker();
            frm.ShowDialog();
        }

        private void btnChangeWorker_Click(object sender, EventArgs e)
        {
            //if (IndexSelectedRowValid())
            //{
            //    object[] selectedRow = dsWorkers.Tables[0].Rows[indexSelectedRow].ItemArray;
            //    Form frm = new FNewOrChangeWorker(selectedRow);
            //    frm.ShowDialog();
            //    GetWorkersSQL();
            //}
        }

        private void btnDeleteWorker_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.SelectedRows != null)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Вы уверены, что хотите удалить выбраного Работника?", "Confirm Deletion", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                   //     
                }

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            
            switch (keyData)
            {
                case Keys.F5:
                    InitializeTVDivisions(repos.GetDivisionsList(), null);
                    bHandled = true;
                    break;
            }
            return bHandled;
        }

        private void btnDeleteDivision_Click(object sender, EventArgs e)
        {
            //if (tvDivisions.SelectedNode != null)
            //{
            //    DialogResult dialogResult;
            //    dialogResult = MessageBox.Show($"Вы уверены, что хотите удалить {Title}?", "Confirm Deletion", MessageBoxButtons.YesNo);

            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        DeleteDepartament();
            //    }

            //    FillTVDivisions();
                
            //    dgvWorkers.DataSource = null;
                
            //}
            
        }
    }
}

