using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

//using System.Data.SqlClient;


namespace Divisions
{
    public partial class FmMain : Form
    {
        
        private DivisionRepository repos = new DivisionRepository();
        private List<Division> items;

        public FmMain()
        {
            InitializeComponent();

            tvDivisions.BeforeSelect += tvDivisions_BeforeSelect;
            tvDivisions.BeforeExpand += tvDivisions_BeforeExpand;
            tvDivisions.AfterSelect += tvDivisions_AfterSelect;
            
            FillTVDivisions();

            if (tvDivisions.Nodes.Count == 0)
            {
                btnChangeDivision.Enabled = false;
                btnDeleteDivision.Enabled = false;
            }

            btnChangeDivision.Enabled = true;
            btnDeleteDivision.Enabled = true;
        }

        private void tvDivisions_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            FillNode(items, e.Node);
        }

        private void tvDivisions_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            FillNode(items, e.Node);
        }

        private void tvDivisions_AfterSelect(object sender, TreeViewEventArgs e)
        {

            //lb.Text = "Работники:";
            ////DepartamentID = Convert.ToInt32(e.Node.Name);
            ////Lvl = Convert.ToInt32(e.Node.Tag);
            //// Title = e.Node.Text;
            //// SetStructureIDSQL(DepartamentID);

            //if (Convert.ToInt32(e.Node.Tag) == 0)
            //{
            //    dgvWorkers.DataSource = null;
            //    btnCreateWorker.Enabled = false;
            //    btnChangeWorker.Enabled = false;
            //    btnDeleteWorker.Enabled = false;
            //    GetAllWorkerOfDepartamentsSQL();
            //    return;
            //}

            //lb.Text = "Работники: " + e.Node.Text;
            //btnCreateWorker.Enabled = true;
            //btnChangeWorker.Enabled = true;
            //btnDeleteWorker.Enabled = true;
            //GetWorkersSQL();
        }

        private void FillTVDivisions()
        {
            items = repos.GetDivisionsList();
            FillNode(items, null);

        }

        private void FillNode(List<Division> items, TreeNode node)
        {
            var parentID = node != null ? (int)node.Tag : 0;

            var nodeCollection = node != null ? node.Nodes : tvDivisions.Nodes;

            foreach (var item in items.Where(d => d.ParentId == parentID))

            {
                var newNode = nodeCollection.Add(item.Name);
                newNode.Tag = item.Id;

                FillNode(items, newNode);
            }
        }

        private void GetAllWorkerOfDepartamentsSQL()
        {
            //dsWorkers.Reset();

            //dgvWorkers.DataSource = null;

            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //{
            //    using (SqlDataAdapter adapter = new SqlDataAdapter())
            //    {
            //        using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetAllWorkersOfDepartament", connection))
            //        {
            //            sqlCommand.CommandType = CommandType.StoredProcedure;
            //            sqlCommand.Parameters.Add(new SqlParameter("@AncestorID", SqlDbType.Int)).Value = DepartamentID;
            //            adapter.SelectCommand = sqlCommand;

            //            try
            //            {
            //                connection.Open();
            //                adapter.SelectCommand.ExecuteNonQuery();
            //                adapter.Fill(dsWorkers);

            //                dgvWorkers.DataSource = dsWorkers.Tables[0];
                            
            //                dgvWorkers.Columns["Title"].HeaderText = "Название отдела";
            //                FormatDVGWorkers();
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Запрошенные Работники Департамета не могут загрузиться на форму.");
            //            }
            //            finally
            //            {
            //                connection.Close();
            //            }
            //        }
            //    }
            //}
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

        private void SetStructureIDSQL(int departamentID)
        {
            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //{

            //    using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetStructureID", connection))
            //    {
            //        sqlCommand.CommandType = CommandType.StoredProcedure;
            //        sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = departamentID;
            //        sqlCommand.Parameters.Add(new SqlParameter("@StructureID", SqlDbType.Int)).Direction = ParameterDirection.Output;

            //        try
            //        {
            //            connection.Open();
            //            sqlCommand.ExecuteNonQuery();
                        
            //            StructureID = (int)sqlCommand.Parameters["@StructureID"].Value;
            //        }
            //        catch
            //        {
            //            MessageBox.Show("Номер из таблицы Structure не получен.");
            //        }
            //        finally
            //        {
            //            connection.Close();
            //        }
            //    }
            //}
        }

        
        private void GetWorkersSQL()
        {
            //dsWorkers.Reset();
            //dgvWorkers.DataSource = null;
            
            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //{
            //    using (SqlDataAdapter adapter = new SqlDataAdapter())
            //    {
            //        using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetWorkers", connection))
            //        {
            //            sqlCommand.CommandType = CommandType.StoredProcedure;
            //            sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int));
            //            sqlCommand.Parameters["@DepartamentID"].Value = DepartamentID;
            //            adapter.SelectCommand = sqlCommand;

            //            try
            //            {
            //                connection.Open();
            //                adapter.SelectCommand.ExecuteNonQuery();
            //                adapter.Fill(dsWorkers);

            //                dgvWorkers.DataSource = dsWorkers.Tables[0];
                            
            //                FormatDVGWorkers();
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Запрошенные Работники не могут загрузиться на форму.");
            //            }
            //            finally
            //            {
            //                connection.Close();
            //            }
            //        }
            //    }
            //}
        }

        
        //private void FillDivisionsNodes()
        //{
        //    //DataSet dsRoots = new DataSet();
        //    //tvDivisions.Nodes.Clear();
        //    FillDSRootsDSQL(dsRoots);
        //    IsTVDepartamensNull = true;
        //    if (dsRoots.Tables[0].Rows.Count != 0)
        //    {
        //        foreach (DataRow row in dsRoots.Tables[0].Rows)
        //        {
        //            TreeNode depRoot = new TreeNode(row["Title"].ToString());

        //            DepartamentID = Convert.ToInt32(row["DepartamentID"]);

        //            depRoot.Name = DepartamentID.ToString();
        //            depRoot.Tag = 0;

        //            IsTVDepartamensNull = false;
        //            FillTreeDepartaments(depRoot, DepartamentID, 1);

        //            tvDivisions.Nodes.Add(depRoot);
        //        }
        //    }
        //}

     //   private void FillDSRootsDSQL(DataSet dsRoots)
        //{
        //    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
        //    {
        //        using (SqlDataAdapter adapter = new SqlDataAdapter())
        //        {
        //            using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetRoots", connection))
        //            {
        //                sqlCommand.CommandType = CommandType.StoredProcedure;

        //                adapter.SelectCommand = sqlCommand;

        //                try
        //                {
        //                    connection.Open();
        //                    adapter.SelectCommand.ExecuteNonQuery();
                            
        //                    adapter.Fill(dsRoots);
        //                }
        //                catch
        //                {
        //                    MessageBox.Show("Запрошенные Отделения не могут загрузиться на форму.");
        //                }
        //                finally
        //                {
        //                    connection.Close();
        //                }
        //            }
        //        }
        //    }
        //}

        //private void FillTreeDepartaments(TreeNode depRoot, int ancestorID, int level)
        //{
        //    DataSet dsTreePath = new DataSet();
        //    FillDSTreePatSQL(dsTreePath, ancestorID, level);

        //    foreach (DataRow row in dsTreePath.Tables[0].Rows)
        //    {
        //        TreeNode treePath = new TreeNode();
        //        treePath.Text = row["Title"].ToString();
        //        treePath.Name = row["DepartamentID"].ToString();
        //        treePath.Tag = Convert.ToInt32(row["Level"]);

        //        FillTreeDepartaments(treePath, Convert.ToInt32(row["DepartamentID"]), ++level);

        //        depRoot.Nodes.Add(treePath);
        //    }
        //}

        //private void FillDSTreePatSQL(DataSet dsTreePath, int ancestorID, int level)
        //{
        //    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
        //    {
        //        using (SqlDataAdapter adapter = new SqlDataAdapter())
        //        {
        //            using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetTreePath", connection))
        //            {
        //                sqlCommand.CommandType = CommandType.StoredProcedure;
        //                sqlCommand.Parameters.Add(new SqlParameter("@Level", SqlDbType.Int));
        //                sqlCommand.Parameters["@Level"].Value = level;
        //                sqlCommand.Parameters.Add(new SqlParameter("@AncestorID", SqlDbType.Int));
        //                sqlCommand.Parameters["@AncestorID"].Value = ancestorID;

        //                adapter.SelectCommand = sqlCommand;

        //                try
        //                {
        //                    connection.Open();
        //                    adapter.SelectCommand.ExecuteNonQuery();

        //                    adapter.Fill(dsTreePath);
        //                }
        //                catch
        //                {
        //                    MessageBox.Show("Запрошенные Отделы/подотделы не могут загрузиться на форму.");
        //                }
        //                finally
        //                {
        //                    connection.Close();
        //                }
        //            }
        //        }
        //    }
        //}

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
            GetWorkersSQL();
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
                    DeleteWorkerSQL();
                }

                GetWorkersSQL();
            }
        }
       
        private void DeleteWorkerSQL()
        {
            //if (IndexSelectedRowValid())
            //{
            //    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //    {
            //        using (SqlDataAdapter adapter = new SqlDataAdapter())
            //        {
            //            const string sql = "DELETE FROM [Offices].[Workers] WHERE [WorkerID] = @WorkerID";

            //            using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
            //            {
            //                sqlCommand.Parameters.Add(new SqlParameter("@WorkerID", SqlDbType.Int)).Value = Convert.ToInt32(dsWorkers.Tables[0].Rows[indexSelectedRow]["WorkerID"]);

            //                adapter.DeleteCommand = sqlCommand;
            //                try
            //                {
            //                    connection.Open();
            //                    adapter.DeleteCommand.ExecuteNonQuery();
            //                }
            //                catch
            //                {
            //                    MessageBox.Show("Выбранный Работник не удалён .");
            //                }
            //                finally
            //                {
            //                    connection.Close();
            //                }
            //            }
            //        }
            //    }
            //}
        }

        //private bool IndexSelectedRowValid()
        //{
            //if(dgvWorkers.CurrentCell == null)
            //{
            //    return false;
            //}
            //indexSelectedRow = dgvWorkers.CurrentCell.RowIndex;
            //return true;
        //}

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            
            switch (keyData)
            {
                case Keys.F5:
                    FillTVDivisions();
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

        private void DeleteDepartament()
        {
            //deleteCount = 0;
            //DeleteWorkersOnSelectedDepartament(tvDivisions.SelectedNode);

            //if (deleteCount == 0)
            //{
            //    DeleteSelectedDepartamentFromStructureSQL();
            //    DeleteSelectedDepartamentFromDepartamentsSQL();
            //    return;
            //}
            //DeleteSubtreeSQLAndFromDepartaments();
        }

        private void DeleteSubtreeSQLAndFromDepartaments()
        {
            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //{
            //    using (SqlDataAdapter adapter = new SqlDataAdapter())
            //    {
            //        using (SqlCommand sqlCommand = new SqlCommand("Offices.uspDeleteSubtree", connection))
            //        {
            //            sqlCommand.CommandType = CommandType.StoredProcedure;
            //            sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = DepartamentID;

            //            adapter.DeleteCommand = sqlCommand;
            //            try
            //            {
            //                connection.Open();
            //                adapter.DeleteCommand.ExecuteNonQuery();
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Дерево Отделов/подотделов не удалены из Structure.");
            //            }
            //            finally
            //            {
            //                connection.Close();
            //            }
            //        }
            //    }
            //}
            DeleteSubTreeFromTVDepartaments(tvDivisions.SelectedNode);
        }

        private void DeleteSubTreeFromTVDepartaments(TreeNode selectedNode)
        {
            //foreach (TreeNode treeNode in selectedNode.Nodes)
            //{
            //    DeleteSubTreeFromTVDepartaments(treeNode);
            //}
            
            //DepartamentID = Convert.ToInt32(selectedNode.Name);
            //DeleteSelectedDepartamentFromDepartamentsSQL();
        }

        private void DeleteSelectedDepartamentFromStructureSQL()
        {

            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //{
            //    using (SqlDataAdapter adapter = new SqlDataAdapter())
            //    {
            //        const string sql = "DELETE FROM [Offices].[Structure] WHERE [DescendarID] = @DepartamentID";

            //        using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
            //        {
            //            sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = DepartamentID;

            //            adapter.DeleteCommand = sqlCommand;
            //            try
            //            {
            //                connection.Open();
            //                adapter.DeleteCommand.ExecuteNonQuery();
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Отдел/подотдел не удалён Structure.");
            //            }
            //            finally
            //            {
            //                connection.Close();
            //            }
            //        }
            //    }
            //}
        }

        private void DeleteSelectedDepartamentFromDepartamentsSQL()
        {
            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //{
            //    using (SqlDataAdapter adapter = new SqlDataAdapter())
            //    {
            //        const string sql = "DELETE FROM [Offices].[Departaments] WHERE [DepartamentID] = @DepartamentID";

            //        using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
            //        {
            //            sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = DepartamentID;

            //            adapter.DeleteCommand = sqlCommand;
            //            try
            //            {
            //                connection.Open();
            //                adapter.DeleteCommand.ExecuteNonQuery();
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Отдел/подотдел не удалён из Departaments.");
            //            }
            //            finally
            //            {
            //                connection.Close();
            //            }
            //        }
            //    }
            //}
        }

        private void DeleteWorkersOnSelectedDepartament(TreeNode selectedNode)
        {
            //foreach (TreeNode treeNode in selectedNode.Nodes)
            //{
            //    deleteCount++;
            //    DeleteWorkersOnSelectedDepartament(treeNode);
            //}
            
            //if (Convert.ToInt32(selectedNode.Tag) != 0)
            //{
            //    SetStructureIDSQL(Convert.ToInt32(selectedNode.Name));
            //    DeleteAllWorkersSQL();
            //}

        }

        private void DeleteAllWorkersSQL()
        {
            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            //{
            //    using (SqlDataAdapter adapter = new SqlDataAdapter())
            //    {
            //        const string sql = "DELETE FROM [Offices].[Workers] WHERE [StructureID] = @StructureID";

            //        using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
            //        {
            //            sqlCommand.Parameters.Add(new SqlParameter("@StructureID", SqlDbType.Int)).Value = StructureID;

            //            adapter.DeleteCommand = sqlCommand;
            //            try
            //            {
            //                connection.Open();
            //                adapter.DeleteCommand.ExecuteNonQuery();
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Работники не удалёны.");
            //            }
            //            finally
            //            {
            //                connection.Close();
            //            }
            //        }

            //    }
            //}
        }
    }
}

