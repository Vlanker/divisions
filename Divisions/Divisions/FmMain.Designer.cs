namespace Divisions
{
    partial class FmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.scCommon = new System.Windows.Forms.SplitContainer();
            this.tvDivisions = new System.Windows.Forms.TreeView();
            this.lbDivisions = new System.Windows.Forms.Label();
            this.btnDeleteDivision = new System.Windows.Forms.Button();
            this.btnCreateDivision = new System.Windows.Forms.Button();
            this.btnChangeDivision = new System.Windows.Forms.Button();
            this.lb = new System.Windows.Forms.Label();
            this.lbWorkers = new System.Windows.Forms.Label();
            this.btnDeleteWorker = new System.Windows.Forms.Button();
            this.btnChangeWorker = new System.Windows.Forms.Button();
            this.btnCreateWorker = new System.Windows.Forms.Button();
            this.dgvWorkers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.scCommon)).BeginInit();
            this.scCommon.Panel1.SuspendLayout();
            this.scCommon.Panel2.SuspendLayout();
            this.scCommon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkers)).BeginInit();
            this.SuspendLayout();
            // 
            // scCommon
            // 
            this.scCommon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCommon.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scCommon.IsSplitterFixed = true;
            this.scCommon.Location = new System.Drawing.Point(0, 0);
            this.scCommon.Name = "scCommon";
            // 
            // scCommon.Panel1
            // 
            this.scCommon.Panel1.Controls.Add(this.tvDivisions);
            this.scCommon.Panel1.Controls.Add(this.lbDivisions);
            this.scCommon.Panel1.Controls.Add(this.btnDeleteDivision);
            this.scCommon.Panel1.Controls.Add(this.btnCreateDivision);
            this.scCommon.Panel1.Controls.Add(this.btnChangeDivision);
            // 
            // scCommon.Panel2
            // 
            this.scCommon.Panel2.Controls.Add(this.lb);
            this.scCommon.Panel2.Controls.Add(this.lbWorkers);
            this.scCommon.Panel2.Controls.Add(this.btnDeleteWorker);
            this.scCommon.Panel2.Controls.Add(this.btnChangeWorker);
            this.scCommon.Panel2.Controls.Add(this.btnCreateWorker);
            this.scCommon.Panel2.Controls.Add(this.dgvWorkers);
            this.scCommon.Size = new System.Drawing.Size(1109, 450);
            this.scCommon.SplitterDistance = 247;
            this.scCommon.TabIndex = 0;
            // 
            // tvDivisions
            // 
            this.tvDivisions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvDivisions.Location = new System.Drawing.Point(3, 25);
            this.tvDivisions.Name = "tvDivisions";
            this.tvDivisions.Size = new System.Drawing.Size(237, 384);
            this.tvDivisions.TabIndex = 4;
            // 
            // lbDivisions
            // 
            this.lbDivisions.AutoSize = true;
            this.lbDivisions.Location = new System.Drawing.Point(3, 9);
            this.lbDivisions.Name = "lbDivisions";
            this.lbDivisions.Size = new System.Drawing.Size(87, 13);
            this.lbDivisions.TabIndex = 3;
            this.lbDivisions.Text = "Подразделения";
            // 
            // btnDeleteDivision
            // 
            this.btnDeleteDivision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteDivision.Enabled = false;
            this.btnDeleteDivision.Location = new System.Drawing.Point(165, 415);
            this.btnDeleteDivision.Name = "btnDeleteDivision";
            this.btnDeleteDivision.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteDivision.TabIndex = 2;
            this.btnDeleteDivision.Text = "Удалить";
            this.btnDeleteDivision.UseVisualStyleBackColor = true;
            this.btnDeleteDivision.Click += new System.EventHandler(this.btnDeleteDivision_Click);
            // 
            // btnCreateDivision
            // 
            this.btnCreateDivision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateDivision.Location = new System.Drawing.Point(3, 415);
            this.btnCreateDivision.Name = "btnCreateDivision";
            this.btnCreateDivision.Size = new System.Drawing.Size(75, 23);
            this.btnCreateDivision.TabIndex = 0;
            this.btnCreateDivision.Text = "Добавить";
            this.btnCreateDivision.UseVisualStyleBackColor = true;
            this.btnCreateDivision.Click += new System.EventHandler(this.btnAddDivision_Click);
            // 
            // btnChangeDivision
            // 
            this.btnChangeDivision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangeDivision.Enabled = false;
            this.btnChangeDivision.Location = new System.Drawing.Point(84, 415);
            this.btnChangeDivision.Name = "btnChangeDivision";
            this.btnChangeDivision.Size = new System.Drawing.Size(75, 23);
            this.btnChangeDivision.TabIndex = 1;
            this.btnChangeDivision.Text = "Изменить";
            this.btnChangeDivision.UseVisualStyleBackColor = true;
            this.btnChangeDivision.Click += new System.EventHandler(this.btnChangeDivision_Click);
            // 
            // lb
            // 
            this.lb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb.AutoSize = true;
            this.lb.Location = new System.Drawing.Point(12, 425);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(64, 13);
            this.lb.TabIndex = 5;
            this.lb.Text = "Работники:";
            // 
            // lbWorkers
            // 
            this.lbWorkers.AutoSize = true;
            this.lbWorkers.Location = new System.Drawing.Point(3, 9);
            this.lbWorkers.Name = "lbWorkers";
            this.lbWorkers.Size = new System.Drawing.Size(61, 13);
            this.lbWorkers.TabIndex = 4;
            this.lbWorkers.Text = "Работники";
            // 
            // btnDeleteWorker
            // 
            this.btnDeleteWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteWorker.Enabled = false;
            this.btnDeleteWorker.Location = new System.Drawing.Point(780, 415);
            this.btnDeleteWorker.Name = "btnDeleteWorker";
            this.btnDeleteWorker.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteWorker.TabIndex = 3;
            this.btnDeleteWorker.Text = "Удалить";
            this.btnDeleteWorker.UseVisualStyleBackColor = true;
            this.btnDeleteWorker.Click += new System.EventHandler(this.btnDeleteWorker_Click);
            // 
            // btnChangeWorker
            // 
            this.btnChangeWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeWorker.Enabled = false;
            this.btnChangeWorker.Location = new System.Drawing.Point(699, 415);
            this.btnChangeWorker.Name = "btnChangeWorker";
            this.btnChangeWorker.Size = new System.Drawing.Size(75, 23);
            this.btnChangeWorker.TabIndex = 2;
            this.btnChangeWorker.Text = "Изменить";
            this.btnChangeWorker.UseVisualStyleBackColor = true;
            this.btnChangeWorker.Click += new System.EventHandler(this.btnChangeWorker_Click);
            // 
            // btnCreateWorker
            // 
            this.btnCreateWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateWorker.Enabled = false;
            this.btnCreateWorker.Location = new System.Drawing.Point(618, 415);
            this.btnCreateWorker.Name = "btnCreateWorker";
            this.btnCreateWorker.Size = new System.Drawing.Size(75, 23);
            this.btnCreateWorker.TabIndex = 1;
            this.btnCreateWorker.Text = "Добавить";
            this.btnCreateWorker.UseVisualStyleBackColor = true;
            this.btnCreateWorker.Click += new System.EventHandler(this.btnCreateWorker_Click);
            // 
            // dgvWorkers
            // 
            this.dgvWorkers.AllowUserToAddRows = false;
            this.dgvWorkers.AllowUserToDeleteRows = false;
            this.dgvWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWorkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkers.Location = new System.Drawing.Point(3, 25);
            this.dgvWorkers.MultiSelect = false;
            this.dgvWorkers.Name = "dgvWorkers";
            this.dgvWorkers.ReadOnly = true;
            this.dgvWorkers.Size = new System.Drawing.Size(852, 384);
            this.dgvWorkers.TabIndex = 0;
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 450);
            this.Controls.Add(this.scCommon);
            this.Name = "FmMain";
            this.Text = "Подразделения";
            this.scCommon.Panel1.ResumeLayout(false);
            this.scCommon.Panel1.PerformLayout();
            this.scCommon.Panel2.ResumeLayout(false);
            this.scCommon.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCommon)).EndInit();
            this.scCommon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scCommon;
        private System.Windows.Forms.Button btnDeleteDivision;
        private System.Windows.Forms.Button btnChangeDivision;
        private System.Windows.Forms.Button btnCreateDivision;
        private System.Windows.Forms.TreeView tvDivisions;
        private System.Windows.Forms.Label lbDivisions;
        private System.Windows.Forms.Label lbWorkers;
        private System.Windows.Forms.Button btnDeleteWorker;
        private System.Windows.Forms.Button btnChangeWorker;
        private System.Windows.Forms.Button btnCreateWorker;
        private System.Windows.Forms.DataGridView dgvWorkers;
        private System.Windows.Forms.Label lb;
    }
}

