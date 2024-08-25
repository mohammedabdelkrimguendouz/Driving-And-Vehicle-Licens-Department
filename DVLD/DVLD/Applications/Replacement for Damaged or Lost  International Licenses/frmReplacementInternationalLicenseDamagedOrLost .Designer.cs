namespace DVLD.Applications.Replacement_for_Damaged_or_Lost__International_Licenses
{
    partial class frmReplacementInternationalLicenseDamagedOrLost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReplacementInternationalLicenseDamagedOrLost));
            this.lblTitle = new System.Windows.Forms.Label();
            this.ctrlDriverInternationalLicenseInfoWithFilter1 = new DVLD.Licenses.International_License.Controls.ctrlDriverInternationalLicenseInfoWithFilter();
            this.gbReplacementFor = new System.Windows.Forms.GroupBox();
            this.rbDamagedInternationalLicense = new System.Windows.Forms.RadioButton();
            this.rbLostInternationalLicense = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCreatedByUser = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.lblOldInternationalLicenseID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblReplacedInternationalLicenseID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lblReplacedApplicationID = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.llShowNewLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnIssueReplacement = new System.Windows.Forms.Button();
            this.gbReplacementFor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(45, -1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(891, 47);
            this.lblTitle.TabIndex = 122;
            this.lblTitle.Text = "Replacement For Damaged International License";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctrlDriverInternationalLicenseInfoWithFilter1
            // 
            this.ctrlDriverInternationalLicenseInfoWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrlDriverInternationalLicenseInfoWithFilter1.EnableFilter = true;
            this.ctrlDriverInternationalLicenseInfoWithFilter1.Location = new System.Drawing.Point(5, 49);
            this.ctrlDriverInternationalLicenseInfoWithFilter1.Name = "ctrlDriverInternationalLicenseInfoWithFilter1";
            this.ctrlDriverInternationalLicenseInfoWithFilter1.Size = new System.Drawing.Size(976, 396);
            this.ctrlDriverInternationalLicenseInfoWithFilter1.TabIndex = 123;
            this.ctrlDriverInternationalLicenseInfoWithFilter1.OnInternationalLicenseSelected += new System.EventHandler<DVLD.Licenses.International_License.Controls.ctrlDriverInternationalLicenseInfoWithFilter.InternationalLicenseSelectedEventArgs>(this.ctrlDriverInternationalLicenseInfoWithFilter1_OnInternationalLicenseSelected);
            // 
            // gbReplacementFor
            // 
            this.gbReplacementFor.Controls.Add(this.rbDamagedInternationalLicense);
            this.gbReplacementFor.Controls.Add(this.rbLostInternationalLicense);
            this.gbReplacementFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbReplacementFor.Location = new System.Drawing.Point(547, 58);
            this.gbReplacementFor.Name = "gbReplacementFor";
            this.gbReplacementFor.Size = new System.Drawing.Size(430, 77);
            this.gbReplacementFor.TabIndex = 138;
            this.gbReplacementFor.TabStop = false;
            this.gbReplacementFor.Text = "Replacement For : ";
            // 
            // rbDamagedInternationalLicense
            // 
            this.rbDamagedInternationalLicense.AutoSize = true;
            this.rbDamagedInternationalLicense.Checked = true;
            this.rbDamagedInternationalLicense.Location = new System.Drawing.Point(26, 23);
            this.rbDamagedInternationalLicense.Name = "rbDamagedInternationalLicense";
            this.rbDamagedInternationalLicense.Size = new System.Drawing.Size(228, 22);
            this.rbDamagedInternationalLicense.TabIndex = 1;
            this.rbDamagedInternationalLicense.TabStop = true;
            this.rbDamagedInternationalLicense.Text = "Damaged International License";
            this.rbDamagedInternationalLicense.UseVisualStyleBackColor = true;
            this.rbDamagedInternationalLicense.CheckedChanged += new System.EventHandler(this.rbDamagedInternationalLicense_CheckedChanged);
            // 
            // rbLostInternationalLicense
            // 
            this.rbLostInternationalLicense.AutoSize = true;
            this.rbLostInternationalLicense.Location = new System.Drawing.Point(26, 49);
            this.rbLostInternationalLicense.Name = "rbLostInternationalLicense";
            this.rbLostInternationalLicense.Size = new System.Drawing.Size(193, 22);
            this.rbLostInternationalLicense.TabIndex = 0;
            this.rbLostInternationalLicense.TabStop = true;
            this.rbLostInternationalLicense.Text = "Lost International License";
            this.rbLostInternationalLicense.UseVisualStyleBackColor = true;
            this.rbLostInternationalLicense.CheckedChanged += new System.EventHandler(this.rbLostInternationalLicense_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCreatedByUser);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.pictureBox8);
            this.groupBox1.Controls.Add(this.lblOldInternationalLicenseID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblReplacedInternationalLicenseID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.lblApplicationFees);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.lblApplicationDate);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.pictureBox5);
            this.groupBox1.Controls.Add(this.lblReplacedApplicationID);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.pictureBox10);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 451);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(969, 132);
            this.groupBox1.TabIndex = 139;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Info For International License Replacement";
            // 
            // lblCreatedByUser
            // 
            this.lblCreatedByUser.AutoSize = true;
            this.lblCreatedByUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedByUser.Location = new System.Drawing.Point(787, 97);
            this.lblCreatedByUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreatedByUser.Name = "lblCreatedByUser";
            this.lblCreatedByUser.Size = new System.Drawing.Size(48, 18);
            this.lblCreatedByUser.TabIndex = 204;
            this.lblCreatedByUser.Text = "[????]";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(634, 97);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 20);
            this.label14.TabIndex = 203;
            this.label14.Text = "Created By :";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD.Properties.Resources.User_32__21;
            this.pictureBox8.Location = new System.Drawing.Point(749, 90);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 26);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 205;
            this.pictureBox8.TabStop = false;
            // 
            // lblOldInternationalLicenseID
            // 
            this.lblOldInternationalLicenseID.AutoSize = true;
            this.lblOldInternationalLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldInternationalLicenseID.Location = new System.Drawing.Point(787, 57);
            this.lblOldInternationalLicenseID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOldInternationalLicenseID.Name = "lblOldInternationalLicenseID";
            this.lblOldInternationalLicenseID.Size = new System.Drawing.Size(48, 18);
            this.lblOldInternationalLicenseID.TabIndex = 201;
            this.lblOldInternationalLicenseID.Text = "[????]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(594, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 20);
            this.label5.TabIndex = 200;
            this.label5.Text = "Old I.License ID :";
            // 
            // lblReplacedInternationalLicenseID
            // 
            this.lblReplacedInternationalLicenseID.AutoSize = true;
            this.lblReplacedInternationalLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplacedInternationalLicenseID.Location = new System.Drawing.Point(787, 17);
            this.lblReplacedInternationalLicenseID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReplacedInternationalLicenseID.Name = "lblReplacedInternationalLicenseID";
            this.lblReplacedInternationalLicenseID.Size = new System.Drawing.Size(48, 18);
            this.lblReplacedInternationalLicenseID.TabIndex = 198;
            this.lblReplacedInternationalLicenseID.Text = "[????]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(546, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 20);
            this.label3.TabIndex = 197;
            this.label3.Text = "Renewed I.License ID :";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD.Properties.Resources.LocalDriving_License3;
            this.pictureBox4.Location = new System.Drawing.Point(749, 50);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 26);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 202;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD.Properties.Resources.Renew_Driving_License_326;
            this.pictureBox3.Location = new System.Drawing.Point(749, 10);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 26);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 199;
            this.pictureBox3.TabStop = false;
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationFees.Location = new System.Drawing.Point(255, 102);
            this.lblApplicationFees.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(48, 18);
            this.lblApplicationFees.TabIndex = 151;
            this.lblApplicationFees.Text = "[$$$$]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 20);
            this.label4.TabIndex = 150;
            this.label4.Text = "Application Fees :";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.money_322;
            this.pictureBox2.Location = new System.Drawing.Point(207, 97);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 152;
            this.pictureBox2.TabStop = false;
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDate.Location = new System.Drawing.Point(255, 68);
            this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(48, 18);
            this.lblApplicationDate.TabIndex = 145;
            this.lblApplicationDate.Text = "[????]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(46, 66);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 20);
            this.label10.TabIndex = 144;
            this.label10.Text = "Application Date :";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD.Properties.Resources.Calendar_322;
            this.pictureBox5.Location = new System.Drawing.Point(207, 62);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(31, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 146;
            this.pictureBox5.TabStop = false;
            // 
            // lblReplacedApplicationID
            // 
            this.lblReplacedApplicationID.AutoSize = true;
            this.lblReplacedApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplacedApplicationID.Location = new System.Drawing.Point(255, 34);
            this.lblReplacedApplicationID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReplacedApplicationID.Name = "lblReplacedApplicationID";
            this.lblReplacedApplicationID.Size = new System.Drawing.Size(48, 18);
            this.lblReplacedApplicationID.TabIndex = 140;
            this.lblReplacedApplicationID.Text = "[????]";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(43, 33);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(150, 20);
            this.label22.TabIndex = 139;
            this.label22.Text = "R.Application ID :";
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::DVLD.Properties.Resources.Number_32;
            this.pictureBox10.Location = new System.Drawing.Point(207, 27);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(31, 26);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 141;
            this.pictureBox10.TabStop = false;
            // 
            // llShowNewLicenseInfo
            // 
            this.llShowNewLicenseInfo.AutoSize = true;
            this.llShowNewLicenseInfo.Enabled = false;
            this.llShowNewLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowNewLicenseInfo.Location = new System.Drawing.Point(171, 598);
            this.llShowNewLicenseInfo.Name = "llShowNewLicenseInfo";
            this.llShowNewLicenseInfo.Size = new System.Drawing.Size(163, 18);
            this.llShowNewLicenseInfo.TabIndex = 143;
            this.llShowNewLicenseInfo.TabStop = true;
            this.llShowNewLicenseInfo.Text = "Show New License Info";
            this.llShowNewLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowNewLicenseInfo_LinkClicked);
            // 
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Enabled = false;
            this.llShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowLicenseHistory.Location = new System.Drawing.Point(4, 598);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(152, 18);
            this.llShowLicenseHistory.TabIndex = 142;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show License History";
            this.llShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseHistory_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(607, 591);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 33);
            this.btnClose.TabIndex = 141;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnIssueReplacement
            // 
            this.btnIssueReplacement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIssueReplacement.Enabled = false;
            this.btnIssueReplacement.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIssueReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssueReplacement.Image = global::DVLD.Properties.Resources.Renew_Driving_License_325;
            this.btnIssueReplacement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueReplacement.Location = new System.Drawing.Point(741, 591);
            this.btnIssueReplacement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIssueReplacement.Name = "btnIssueReplacement";
            this.btnIssueReplacement.Size = new System.Drawing.Size(234, 33);
            this.btnIssueReplacement.TabIndex = 140;
            this.btnIssueReplacement.Text = " Issue Replacement";
            this.btnIssueReplacement.UseVisualStyleBackColor = true;
            this.btnIssueReplacement.Click += new System.EventHandler(this.btnIssueReplacement_Click);
            // 
            // frmReplacementInternationalLicenseDamagedOrLost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(989, 634);
            this.Controls.Add(this.llShowNewLicenseInfo);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnIssueReplacement);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbReplacementFor);
            this.Controls.Add(this.ctrlDriverInternationalLicenseInfoWithFilter1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReplacementInternationalLicenseDamagedOrLost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replacement For Damaged International License";
            this.Load += new System.EventHandler(this.frmReplacementInternationalLicenseDamagedOrLost_Load);
            this.gbReplacementFor.ResumeLayout(false);
            this.gbReplacementFor.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Licenses.International_License.Controls.ctrlDriverInternationalLicenseInfoWithFilter ctrlDriverInternationalLicenseInfoWithFilter1;
        private System.Windows.Forms.GroupBox gbReplacementFor;
        private System.Windows.Forms.RadioButton rbDamagedInternationalLicense;
        private System.Windows.Forms.RadioButton rbLostInternationalLicense;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCreatedByUser;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label lblOldInternationalLicenseID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblReplacedInternationalLicenseID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lblReplacedApplicationID;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.LinkLabel llShowNewLicenseInfo;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnIssueReplacement;
    }
}