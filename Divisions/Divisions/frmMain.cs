using Divisions.DAL;
using Divisions.DAL.Context;
using Divisions.DAL.Repository;
using Divisions.DAL.View_model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Divisions
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            InitializeTVDivisions(new DivisionsViewModel().Divisions, null);

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
            int parentID = Convert.ToInt32(e.Node.Name);
            int divisionId = Convert.ToInt32(e.Node.Tag);
            if (parentID == 0)
            {
                dgvWorkers.DataSource = null;
                btnCreateWorker.Enabled = false;
                btnChangeWorker.Enabled = false;
                btnDeleteWorker.Enabled = false;
                return;
            }
            dgvWorkers.AutoGenerateColumns = true;
            var source = new WorkersViewModel(divisionId).Workers;
            dgvWorkers.DataSource = source.ToArray();
            lb.Text = $"Работники: {e.Node.Text}";
            btnCreateWorker.Enabled = true;
            btnChangeWorker.Enabled = true;
            btnDeleteWorker.Enabled = true;
        }

        private void btnAddDivision_Click(object sender, EventArgs e)
        {   
            //если никакой элемент не выбран, то добавить можно только новое Отделение
            if (tvDivisions.SelectedNode == null)
            {
                Form frmNewRoot = new AddOrEditDivision();
                frmNewRoot.ShowDialog();
                ClearAndInitializeTVDivisions(new DivisionsViewModel().Divisions);
                return;
            }
            //Если выделен элемент, можно добавить как новое  Отделение, 
            //так и новый Отдел/подотдел в Выбранный пункт
            int selectedDivisionId = Convert.ToInt32(tvDivisions.SelectedNode.Tag);
            
            Form frmNewDivElement = new AddOrEditDivision(selectedDivisionId);
            frmNewDivElement.ShowDialog();
            ClearAndInitializeTVDivisions(new DivisionsViewModel().Divisions);
        }
        private void btnEditDivision_Click(object sender, EventArgs e)
        {
            if (tvDivisions.SelectedNode != null)
            {
                int idSelectednode = Convert.ToInt32(tvDivisions.SelectedNode.Tag);
                Division division = new DivisionViewModel(idSelectednode).Division;
                Form updateDepartament = new AddOrEditDivision(division);
                updateDepartament.ShowDialog();
                ClearAndInitializeTVDivisions(new DivisionsViewModel().Divisions);
            }
        }
        private void btnRemoveDivision_Click(object sender, EventArgs e)
        {
            if (tvDivisions.SelectedNode != null)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show($"Вы уверены, что хотите удалить {tvDivisions.SelectedNode.Name}?", "Confirm Deletion", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    int id = (int)tvDivisions.SelectedNode.Tag;
                    Division division = new DivisionViewModel(id).Division;
                    new DivisionRepository().Remove(division);
                    ClearAndInitializeTVDivisions(new DivisionsViewModel().Divisions);
                }
            }

        }
        //treeview {Text = db.Name, Tag = db.ID } 
        private void InitializeTVDivisions(List<Division> divisionList, TreeNode node)
        {
            var parentID = node != null ? (int)node.Tag : 0;
            
            var nodeCollection = node != null ? node.Nodes : tvDivisions.Nodes;

            foreach (var item in divisionList.Where(d => d.ParentId == parentID))
            {
                var newNode = nodeCollection.Add(item.Name);
                newNode.Tag = item.Id;
                newNode.Name = parentID.ToString();

                InitializeTVDivisions(divisionList, newNode);
            }
        }
        private void ClearAndInitializeTVDivisions(List<Division> divisionList)
        {
            tvDivisions.Nodes.Clear();
            InitializeTVDivisions(divisionList, null);
        }


        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            int selectedDivisionId = Convert.ToInt32(tvDivisions.SelectedNode.Tag);
            Form frm = new AddOrEditWorker(selectedDivisionId);
            frm.ShowDialog();
        }
        private void btnEditWorker_Click(object sender, EventArgs e)
        {
            //if (IndexSelectedRowValid())
            //{
            //    int id = Convert.ToInt32(tvDivisions.SelectedNode.Tag);
            //    Worker worker = new WorkerViewModel(id).Worker;
            //    Form frm = new AddOrEditWorker(worker);
            //    frm.ShowDialog();
               
            //}
        }
        private void btnRemoveWorker_Click(object sender, EventArgs e)
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
                    ClearAndInitializeTVDivisions(new DivisionsViewModel().Divisions);
                    bHandled = true;
                    break;
            }
            return bHandled;
        }
        
    }
}

