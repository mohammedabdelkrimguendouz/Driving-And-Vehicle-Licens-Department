namespace DVLD.Licenses.International_License.Controls
{
    partial class ctrlDriverInternationalLicenseInfoWithFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.btnFindInternationalLicense = new System.Windows.Forms.Button();
            this.txtInternationalLicenseID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlDriverInternationalLicenseInfo1 = new DVLD.Licenses.International_InternationalLicenseInfos.ctrlDriverInternationalLicenseInfo();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilters
            // 
            this.gbFilters.Controls.Add(this.btnFindInternationalLicense);
            this.gbFilters.Controls.Add(this.txtInternationalLicenseID);
            this.gbFilters.Controls.Add(this.label2);
            this.gbFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilters.Location = new System.Drawing.Point(3, 3);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(518, 82);
            this.gbFilters.TabIndex = 136;
            this.gbFilters.TabStop = false;
            this.gbFilters.Text = "Filter";
            // 
            // btnFindInternationalLicense
            // 
            this.btnFindInternationalLicense.BackgroundImage = global::DVLD.Properties.Resources.License_View_323;
            this.btnFindInternationalLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFindInternationalLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFindInternationalLicense.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFindInternationalLicense.Location = new System.Drawing.Point(408, 29);
            this.btnFindInternationalLicense.Name = "btnFindInternationalLicense";
            this.btnFindInternationalLicense.Size = new System.Drawing.Size(64, 39);
            this.btnFindInternationalLicense.TabIndex = 134;
            this.btnFindInternationalLicense.UseVisualStyleBackColor = true;
            this.btnFindInternationalLicense.Click += new System.EventHandler(this.btnFindInternationalLicense_Click);
            // 
            // txtInternationalLicenseID
            // 
            this.txtInternationalLicenseID.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtInternationalLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInternationalLicenseID.Location = new System.Drawing.Point(193, 38);
            this.txtInternationalLicenseID.Name = "txtInternationalLicenseID";
            this.txtInternationalLicenseID.Size = new System.Drawing.Size(184, 24);
            this.txtInternationalLicenseID.TabIndex = 133;
            this.txtInternationalLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInternationalLicenseID_KeyPress);
            this.txtInternationalLicenseID.Validating += new System.ComponentModel.CancelEventHandler(this.txtInternationalLicenseID_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 24);
            this.label2.TabIndex = 132;
            this.label2.Text = "I.License ID :";
            // 
            // ctrlDriverInternationalLicenseInfo1
            // 
            this.ctrlDriverInternationalLicenseInfo1.Location = new System.Drawing.Point(3, 91);
            this.ctrlDriverInternationalLicenseInfo1.Name = "ctrlDriverInternationalLicenseInfo1";
            this.ctrlDriverInternationalLicenseInfo1.Size = new System.Drawing.Size(968, 306);
            this.ctrlDriverInternationalLicenseInfo1.TabIndex = 137;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlDriverInternationalLicenseInfoWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ctrlDriverInternationalLicenseInfo1);
            this.Controls.Add(this.gbFilters);
            this.Name = "ctrlDriverInternationalLicenseInfoWithFilter";
            this.Size = new System.Drawing.Size(976, 396);
            this.Load += new System.EventHandler(this.ctrlDriverInternationalLicenseInfoWithFilter_Load);
            this.gbFilters.ResumeLayout(false);
            this.gbFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.Button btnFindInternationalLicense;
        private System.Windows.Forms.TextBox txtInternationalLicenseID;
        private System.Windows.Forms.Label label2;
        private International_InternationalLicenseInfos.ctrlDriverInternationalLicenseInfo ctrlDriverInternationalLicenseInfo1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
