namespace PhotoOrganizer
{
    partial class frmSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbSourcePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTempFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbtbTargetPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbWorkspace = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbExtensions = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source path";
            // 
            // tbSourcePath
            // 
            this.tbSourcePath.Location = new System.Drawing.Point(93, 58);
            this.tbSourcePath.Name = "tbSourcePath";
            this.tbSourcePath.Size = new System.Drawing.Size(476, 20);
            this.tbSourcePath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Temp folder";
            // 
            // tbTempFolder
            // 
            this.tbTempFolder.Location = new System.Drawing.Point(93, 88);
            this.tbTempFolder.Name = "tbTempFolder";
            this.tbTempFolder.Size = new System.Drawing.Size(476, 20);
            this.tbTempFolder.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target path";
            // 
            // tbtbTargetPath
            // 
            this.tbtbTargetPath.Location = new System.Drawing.Point(93, 118);
            this.tbtbTargetPath.Name = "tbtbTargetPath";
            this.tbtbTargetPath.Size = new System.Drawing.Size(476, 20);
            this.tbtbTargetPath.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Workspace";
            // 
            // cmbWorkspace
            // 
            this.cmbWorkspace.FormattingEnabled = true;
            this.cmbWorkspace.Items.AddRange(new object[] {
            "New"});
            this.cmbWorkspace.Location = new System.Drawing.Point(102, 13);
            this.cmbWorkspace.Name = "cmbWorkspace";
            this.cmbWorkspace.Size = new System.Drawing.Size(285, 21);
            this.cmbWorkspace.TabIndex = 7;
            this.cmbWorkspace.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(420, 218);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(518, 217);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Extensions";
            // 
            // tbExtensions
            // 
            this.tbExtensions.Location = new System.Drawing.Point(93, 149);
            this.tbExtensions.Name = "tbExtensions";
            this.tbExtensions.Size = new System.Drawing.Size(476, 20);
            this.tbExtensions.TabIndex = 11;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 338);
            this.Controls.Add(this.tbExtensions);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbWorkspace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbtbTargetPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTempFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSourcePath);
            this.Controls.Add(this.label1);
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSourcePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTempFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbtbTargetPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbWorkspace;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbExtensions;
    }
}