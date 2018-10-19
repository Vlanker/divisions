using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace Divisions
{
    public partial class FDivisionsNavigation : Form
    {
        private DataSet dsWorkers = new DataSet();
        public static int DepartamentID { get; set; }
        public static string Title { get; set; }
        public static int Lvl { get; set; }
        public static int StructureID { get; set; }
        public static bool IsTVDepartamensNull { get; set; }
       


        private static int indexSelectedRow; 

        public FDivisionsNavigation()
        {
            InitializeComponent();

            tvDivisions.BeforeSelect += tvDivisions_BeforeSelect;
            tvDivisions.BeforeExpand += tvDivisions_BeforeExpand;
            tvDivisions.AfterSelect += tvDivisions_AfterSelect;
            DepartamentID = 1;
            Lvl = 0;
            

            FillDivisionsNodes();
        }

        private void tvDivisions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lb.Text = "Работники:";
            DepartamentID = Convert.ToInt32(e.Node.Name);
            Lvl = Convert.ToInt32(e.Node.Tag);
            Title = e.Node.Text;
            SetStructureID(DepartamentID);

            if (Convert.ToInt32(e.Node.Tag) == 0)
            {
                dsWorkers.Clear();
                dgvWorkers.DataSource = null;
                btnCreateWorker.Enabled = false;
                btnChangeWorker.Enabled = false;
                btnDeleteWorker.Enabled = false;
                return;
            }

            lb.Text = "Работники: " + e.Node.Text;
            btnCreateWorker.Enabled = true;
            btnChangeWorker.Enabled = true;
            btnDeleteWorker.Enabled = true;
            GetWorkers();
        }

        private void SetStructureID(int departamentID)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {

                using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetStructureID", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int)).Value = departamentID;
                    sqlCommand.Parameters.Add(new SqlParameter("@StructureID", SqlDbType.Int)).Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        
                        StructureID = (int)sqlCommand.Parameters["@StructureID"].Value;
                    }
                    catch
                    {
                        MessageBox.Show("Номер из таблицы Structure не получен.");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                
            }

        }

        private void tvDivisions_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            FillTreeDepartaments(e.Node, Convert.ToInt32(e.Node.Name), Convert.ToInt32(e.Node.Tag) + 1);
        }

        private void GetWorkers()
        {
            dsWorkers.Clear();
            dgvWorkers.DataSource = null;
            
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetWorkers", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@DepartamentID", SqlDbType.Int));
                        sqlCommand.Parameters["@DepartamentID"].Value = DepartamentID;
                        adapter.SelectCommand = sqlCommand;

                        try
                        {
                            connection.Open();
                            adapter.SelectCommand.ExecuteNonQuery();
                            adapter.Fill(dsWorkers);

                            dgvWorkers.DataSource = dsWorkers.Tables[0];
                            dgvWorkers.Columns["WorkerID"].Visible = false;
                            dgvWorkers.Columns["StructureID"].Visible = false;
                        }
                        catch
                        {
                            MessageBox.Show("Запрошенные Работники не могут загрузиться на форму.");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        private void tvDivisions_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            FillTreeDepartaments(e.Node, Convert.ToInt32(e.Node.Name), Convert.ToInt32(e.Node.Tag)+ 1);
        }

        private void FillDivisionsNodes()
        {
            DataSet dsRoots = new DataSet();
            tvDivisions.Nodes.Clear();
            FillDSRoots(dsRoots);
            IsTVDepartamensNull = true;
            if (dsRoots.Tables.Count > 0)
            {
                foreach (DataRow row in dsRoots.Tables[0].Rows)
                {
                    TreeNode depRoot = new TreeNode(row["Title"].ToString());
                    
                    DepartamentID = Convert.ToInt32(row["DepartamentID"]);
                    
                    depRoot.Name = DepartamentID.ToString();
                    depRoot.Tag = 0;

                    IsTVDepartamensNull = false;
                    FillTreeDepartaments(depRoot, DepartamentID, 1);
                    
                    tvDivisions.Nodes.Add(depRoot);
                }
            }

            if(tvDivisions.Nodes.Count == 0)
            {
                btnChangeDivision.Enabled = false;
                btnDeleteDivision.Enabled = false;
            }

            btnChangeDivision.Enabled = true;
            btnDeleteDivision.Enabled = true;
        }

        private void FillDSRoots(DataSet dsRoots)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetRoots", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        adapter.SelectCommand = sqlCommand;

                        try
                        {
                            connection.Open();
                            adapter.SelectCommand.ExecuteNonQuery();
                            
                            adapter.Fill(dsRoots);
                        }
                        catch
                        {
                            MessageBox.Show("Запрошенные Отделения не могут загрузиться на форму.");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        private void FillTreeDepartaments(TreeNode depRoot, int ancestorID, int level)
        {
            DataSet dsTreePath = new DataSet();
            FillDSTreePath(dsTreePath, ancestorID, level);
            
            if (dsTreePath.Tables[0].Rows.Count.ToString() != "")
            {
                foreach (DataRow row in dsTreePath.Tables[0].Rows)
                {
                    TreeNode treePath = new TreeNode();
                    treePath.Text = row["Title"].ToString();
                    treePath.Name = row["DepartamentID"].ToString();
                    treePath.Tag = Convert.ToInt32(row["Level"]);
                                       
                    FillTreeDepartaments(treePath, Convert.ToInt32(row["DepartamentID"]), ++level);
                    
                    depRoot.Nodes.Add(treePath);
                }
            }
        }

        private void FillDSTreePath(DataSet dsTreePath, int ancestorID, int level)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand sqlCommand = new SqlCommand("Offices.uspGetTreePath", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@Level", SqlDbType.Int));
                        sqlCommand.Parameters["@Level"].Value = level;
                        sqlCommand.Parameters.Add(new SqlParameter("@AncestorID", SqlDbType.Int));
                        sqlCommand.Parameters["@AncestorID"].Value = ancestorID;

                        adapter.SelectCommand = sqlCommand;

                        try
                        {
                            connection.Open();
                            adapter.SelectCommand.ExecuteNonQuery();
                            
                            adapter.Fill(dsTreePath);
                        }
                        catch
                        {
                            MessageBox.Show("Запрошенные Отделы/подотделы не могут загрузиться на форму.");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        private void btnCreateDivision_Click(object sender, EventArgs e)
        {
            if (tvDivisions.SelectedNode == null)
            {
                Form frmNewRoot = new FNewOrChangeDivision();
                frmNewRoot.ShowDialog();
                FillDivisionsNodes();
                return;
            }
            Form frmNewDivElement = new FNewOrChangeDivision(DepartamentID, Lvl);
            frmNewDivElement.ShowDialog();
            FillDivisionsNodes();

        }

        private void btnChangeDivision_Click(object sender, EventArgs e)
        {
            Form updateDepartament = new FNewOrChangeDivision(DepartamentID, Title, Lvl);
            updateDepartament.ShowDialog();
            FillDivisionsNodes();
        }

        private void btnCreateWorker_Click(object sender, EventArgs e)
        {
            Form frm = new FNewOrChangeWorker();
            frm.ShowDialog();
            GetWorkers();
        }

        private void btnChangeWorker_Click(object sender, EventArgs e)
        {
            if (IndexSelectedRowValid())
            {
                object[] selectedRow = dsWorkers.Tables[0].Rows[indexSelectedRow].ItemArray;
                Form frm = new FNewOrChangeWorker(selectedRow);
                frm.ShowDialog();
                GetWorkers();
            }
        }

        private void btnDeleteWorker_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.SelectedRows != null)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Вы уверены, что хотите удалить выбраного Работника?", "Confirm Deletion", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    DeleteWorker();
                }

                GetWorkers();
            }
        }

        private void DeleteWorker()
        {
            if (IndexSelectedRowValid())
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        const string sql = "DELETE FROM [Offices].[Workers] WHERE [WorkerID] = @WorkerID";

                        using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                        {
                            sqlCommand.Parameters.Add(new SqlParameter("@WorkerID", SqlDbType.Int)).Value = Convert.ToInt32(dsWorkers.Tables[0].Rows[indexSelectedRow]["WorkerID"]);

                            adapter.DeleteCommand = sqlCommand;
                            try
                            {
                                connection.Open();
                                adapter.DeleteCommand.ExecuteNonQuery();
                            }
                            catch
                            {
                                MessageBox.Show("Выбранный Работник не удалён .");
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }

                    }
                }
            }

            
        }

        private bool IndexSelectedRowValid()
        {
            if(dgvWorkers.CurrentCell == null)
            {
                return false;
            }
            indexSelectedRow = dgvWorkers.CurrentCell.RowIndex;
            return true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            
            switch (keyData)
            {
                case Keys.F5:
                    FillDivisionsNodes();
                    bHandled = true;
                    break;
            }
            return bHandled;
        }

        private void btnDeleteDivision_Click(object sender, EventArgs e)
        {
            if (tvDivisions.SelectedNode != null)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Вы уверены, что хотите удалить?", "Confirm Deletion", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    
                    DeleteDepartament(tvDivisions.SelectedNode);
                }

                GetWorkers();
            }
        }

        private void DeleteDepartament(TreeNode selectedNode)
        {
            MessageBox.Show(selectedNode.ToString());
            if (selectedNode.Nodes != null) {
                foreach (TreeNode item in selectedNode.Nodes)
                {
                    DeleteDepartament(item);
                    SetStructureID(Convert.ToInt32(item.Name));
                    MessageBox.Show(StructureID.ToString());
                    //DeleteWorkers()
                }
            }
        }

        private void DeleteWorkers()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    const string sql = "DELETE FROM [Offices].[Workers] WHERE [StructureID] = @StructureID";

                    using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@StructureID", SqlDbType.Int)).Value = StructureID;

                        adapter.DeleteCommand = sqlCommand;
                        try
                        {
                            connection.Open();
                            adapter.DeleteCommand.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("Выбранный Работник не удалён .");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }

                }
            }
        }
    }
}

