namespace DVLD.Backups
{
    partial class frmCreateBackup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateBackup));
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbBackupType = new System.Windows.Forms.PictureBox();
            this.btnCreateBackup = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackupType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(46, 130);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(350, 43);
            this.lblTitle.TabIndex = 189;
            this.lblTitle.Text = "Create Backup ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(158, 209);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilePath.MaxLength = 50;
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(279, 24);
            this.txtFilePath.TabIndex = 188;
            this.txtFilePath.Validating += new System.ComponentModel.CancelEventHandler(this.txtFilePath_Validating);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(176, 273);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 190;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbBackupType
            // 
            this.pbBackupType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbBackupType.Image = global::DVLD.Properties.Resources.Create_backup_2561;
            this.pbBackupType.InitialImage = null;
            this.pbBackupType.Location = new System.Drawing.Point(117, 8);
            this.pbBackupType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbBackupType.Name = "pbBackupType";
            this.pbBackupType.Size = new System.Drawing.Size(185, 107);
            this.pbBackupType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBackupType.TabIndex = 187;
            this.pbBackupType.TabStop = false;
            // 
            // btnCreateBackup
            // 
            this.btnCreateBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateBackup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateBackup.Image = global::DVLD.Properties.Resources.Add_backup_322;
            this.btnCreateBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateBackup.Location = new System.Drawing.Point(311, 273);
            this.btnCreateBackup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCreateBackup.Name = "btnCreateBackup";
            this.btnCreateBackup.Size = new System.Drawing.Size(126, 37);
            this.btnCreateBackup.TabIndex = 186;
            this.btnCreateBackup.Text = "  Create";
            this.btnCreateBackup.UseVisualStyleBackColor = true;
            this.btnCreateBackup.Click += new System.EventHandler(this.btnCreateBackup_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChoose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChoose.Image = global::DVLD.Properties.Resources.Cursore;
            this.btnChoose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChoose.Location = new System.Drawing.Point(13, 203);
            this.btnChoose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(119, 37);
            this.btnChoose.TabIndex = 185;
            this.btnChoose.Text = "   Choose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmCreateBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(458, 319);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbBackupType);
            this.Controls.Add(this.btnCreateBackup);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.txtFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCreateBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Backup";
            this.Load += new System.EventHandler(this.frmCreateBackup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBackupType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbBackupType;
        private System.Windows.Forms.Button btnCreateBackup;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}