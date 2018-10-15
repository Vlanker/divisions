namespace Divisions
{
    partial class FNewOrChangeDivision
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddRoot = new System.Windows.Forms.Button();
            this.btnAddBranch = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAddRoot
            // 
            this.btnAddRoot.Enabled = false;
            this.btnAddRoot.Location = new System.Drawing.Point(12, 76);
            this.btnAddRoot.Name = "btnAddRoot";
            this.btnAddRoot.Size = new System.Drawing.Size(137, 23);
            this.btnAddRoot.TabIndex = 2;
            this.btnAddRoot.Text = "Добавить отделение";
            this.btnAddRoot.UseVisualStyleBackColor = true;
            this.btnAddRoot.Visible = false;
            this.btnAddRoot.Click += new System.EventHandler(this.btnAddRoot_Click);
            // 
            // btnAddBranch
            // 
            this.btnAddBranch.Enabled = false;
            this.btnAddBranch.Location = new System.Drawing.Point(155, 76);
            this.btnAddBranch.Name = "btnAddBranch";
            this.btnAddBranch.Size = new System.Drawing.Size(180, 23);
            this.btnAddBranch.TabIndex = 3;
            this.btnAddBranch.Text = "Добавить отдел/подотдел";
            this.btnAddBranch.UseVisualStyleBackColor = true;
            this.btnAddBranch.Visible = false;
            this.btnAddBranch.Click += new System.EventHandler(this.btnAddBranch_Click);
            // 
            // btnChange
            // 
            this.btnChange.Enabled = false;
            this.btnChange.Location = new System.Drawing.Point(424, 76);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "Изменить";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Название:";
            
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(12, 25);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(486, 20);
            this.tbTitle.TabIndex = 6;
            
            // 
            // FNewOrChangeDivision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(510, 127);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnAddBranch);
            this.Controls.Add(this.btnAddRoot);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FNewOrChangeDivision";
            this.Text = "Отделения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddRoot;
        private System.Windows.Forms.Button btnAddBranch;
        private System.Windows.Forms.Button btnChange;
        
        private System.Windows.Forms.BindingSource departamentsBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTitle;
    }
}