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
        private DivisionRepository divisionRepo = new DivisionRepository();
        private WorkerRepository workerRepo = new WorkerRepository();
        internal frmMain()
        {
            InitializeComponent();

            FillTVDivisions(new DivisionsViewModel().Divisions, null);

            tvDivisions.AfterSelect += tvDivisions_AfterSelect;
            if (tvDivisions.Nodes.Count == 0)
            {
                btnChangeDivision.Enabled = false;
                btnDeleteDivision.Enabled = false;
            }
            btnChangeDivision.Enabled = true;
            btnDeleteDivision.Enabled = true;
        }
        
        //
        //Работа с Division
        //
        private void tvDivisions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int parentID = Convert.ToInt32(e.Node.Name);
            
            if (parentID == 0)
            {
                dgvWorkers.DataSource = null;
                btnCreateWorker.Enabled = false;
                btnChangeWorker.Enabled = false;
                btnDeleteWorker.Enabled = false;
                lb.Text = $"{e.Node.Text}";
                return;
            }

            FullDGVWokers();
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
                ClearAndFillTVDivisions(new DivisionsViewModel().Divisions);
                lb.Text = "Добавлено";
                return;
            }
            //Если выделен элемент, можно добавить как новое  Отделение, 
            //так и новый Отдел/подотдел в Выбранный пункт
            int selectedDivisionId = Convert.ToInt32(tvDivisions.SelectedNode.Tag);
            
            Form frmNewDivElement = new AddOrEditDivision(selectedDivisionId);
            frmNewDivElement.ShowDialog();
            ClearAndFillTVDivisions(new DivisionsViewModel().Divisions);
            lb.Text = "Добавлено";

        }
        private void btnEditDivision_Click(object sender, EventArgs e)
        {
            if (tvDivisions.SelectedNode != null)
            {

                Division division = GetSelectedDivision();
                Form updateDepartament = new AddOrEditDivision(division);
                updateDepartament.ShowDialog();
                ClearAndFillTVDivisions(new DivisionsViewModel().Divisions);
                lb.Text = "Изменено";

            }
        }
        private void btnRemoveDivision_Click(object sender, EventArgs e)
        {
            if (tvDivisions.SelectedNode != null)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show($"Вы уверены, что хотите удалить {tvDivisions.SelectedNode.Text}?", "Confirm Deletion", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    divisionRepo.Remove(tvDivisions.SelectedNode);
                    dgvWorkers.DataSource = null;
                    ClearAndFillTVDivisions(new DivisionsViewModel().Divisions);
                    lb.Text = "Удаленно";
                }
            }

        }
    
    private void Remove(TreeNode node)
    {
        int divisionId = Convert.ToInt32(node.Tag);
        Division division = new DivisionViewModel(divisionId).Division;
            divisionRepo.Remove(division);
    }

    //treeview {Text = db.Name, Tag = db.ID } 
    private void FillTVDivisions(List<Division> divisionList, TreeNode node)
        {
            var parentID = node != null ? (int)node.Tag : 0;
            
            var nodeCollection = node != null ? node.Nodes : tvDivisions.Nodes;

            foreach (var item in divisionList.Where(d => d.ParentId == parentID))
            {
                var newNode = nodeCollection.Add(item.Name);
                newNode.Tag = item.Id;
                newNode.Name = parentID.ToString();

                FillTVDivisions(divisionList, newNode);
            }
        }
        private void ClearAndFillTVDivisions(List<Division> divisionList)
        {
            tvDivisions.Nodes.Clear();
            FillTVDivisions(divisionList, null);
        }
        private Division GetSelectedDivision()
        {
            int idSelectednode = Convert.ToInt32(tvDivisions.SelectedNode.Tag);
            return new DivisionViewModel(idSelectednode).Division;
        }
        //
        //Работа с Worker
        //
        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            int selectedDivisionId = Convert.ToInt32(tvDivisions.SelectedNode.Tag);
            Form frm = new AddOrEditWorker(selectedDivisionId);
            frm.ShowDialog();
            FullDGVWokers();
            lb.Text = "Добавлен";


        }
        private void btnEditWorker_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.CurrentCell != null)
            {

                Worker editWorker = GetSelectedWorker();
                Form frm = new AddOrEditWorker(editWorker);
                frm.ShowDialog();
                FullDGVWokers();
                lb.Text = "Изменён";

            }
        }
        private void btnRemoveWorker_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.CurrentCell != null)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Вы уверены, что хотите удалить выбраного Работника?", "Confirm Deletion", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    Worker worker = GetSelectedWorker();
                    workerRepo.Remove(worker);
                    FullDGVWokers();
                    lb.Text = "Удалён";

                }

            }
        }
        private void FullDGVWokers()
        {
            int divisionId = Convert.ToInt32(tvDivisions.SelectedNode.Tag);
            dgvWorkers.AutoGenerateColumns = true;
            var source = new WorkersViewModel(divisionId).Workers;
            dgvWorkers.DataSource = source.ToArray();
        }
        private Worker GetSelectedWorker()
        {
            int indexWorker = dgvWorkers.CurrentCell.RowIndex;
            return new WorkerViewModel(indexWorker).Worker;

        }
        //
        //F5 - обновление
        //
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            
            switch (keyData)
            {
                case Keys.F5:
                    ClearAndFillTVDivisions(new DivisionsViewModel().Divisions);
                    dgvWorkers.DataSource = null;
                    bHandled = true;
                    break;
            }
            return bHandled;
        }
        
    }
}

