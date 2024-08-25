namespace DVLD.Users
{
    partial class frmForgotPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmForgotPassword));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbVerification = new System.Windows.Forms.GroupBox();
            this.btnVerification = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtVerificationCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbResetPassword = new System.Windows.Forms.GroupBox();
            this.ctrlConfirmPassword = new DVLD.Users.ctrlPassword();
            this.ctrlNewPassword = new DVLD.Users.ctrlPassword();
            this.btnReset = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lable1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.btnFind = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.gbVerification.SuspendLayout();
            this.gbResetPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // gbVerification
            // 
            this.gbVerification.Controls.Add(this.btnVerification);
            this.gbVerification.Controls.Add(this.lblMessage);
            this.gbVerification.Controls.Add(this.lblEmail);
            this.gbVerification.Controls.Add(this.txtVerificationCode);
            this.gbVerification.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbVerification.Location = new System.Drawing.Point(12, 189);
            this.gbVerification.Name = "gbVerification";
            this.gbVerification.Size = new System.Drawing.Size(584, 162);
            this.gbVerification.TabIndex = 154;
            this.gbVerification.TabStop = false;
            this.gbVerification.Text = "Verification";
            // 
            // btnVerification
            // 
            this.btnVerification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerification.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVerification.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerification.Image = global::DVLD.Properties.Resources.OK_321;
            this.btnVerification.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerification.Location = new System.Drawing.Point(407, 117);
            this.btnVerification.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnVerification.Name = "btnVerification";
            this.btnVerification.Size = new System.Drawing.Size(154, 37);
            this.btnVerification.TabIndex = 4;
            this.btnVerification.Text = "   Verification";
            this.btnVerification.UseVisualStyleBackColor = true;
            this.btnVerification.Click += new System.EventHandler(this.btnVerification_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Black;
            this.lblMessage.Location = new System.Drawing.Point(40, 22);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(461, 18);
            this.lblMessage.TabIndex = 129;
            this.lblMessage.Text = "Please enter the verification code sent to the following email";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.Red;
            this.lblEmail.Location = new System.Drawing.Point(89, 49);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(0, 18);
            this.lblEmail.TabIndex = 144;
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtVerificationCode
            // 
            this.txtVerificationCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtVerificationCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVerificationCode.Location = new System.Drawing.Point(43, 83);
            this.txtVerificationCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtVerificationCode.MaxLength = 50;
            this.txtVerificationCode.Name = "txtVerificationCode";
            this.txtVerificationCode.Size = new System.Drawing.Size(434, 24);
            this.txtVerificationCode.TabIndex = 3;
            this.txtVerificationCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 137;
            this.label1.Text = "New Password : ";
            // 
            // gbResetPassword
            // 
            this.gbResetPassword.Controls.Add(this.ctrlConfirmPassword);
            this.gbResetPassword.Controls.Add(this.ctrlNewPassword);
            this.gbResetPassword.Controls.Add(this.btnReset);
            this.gbResetPassword.Controls.Add(this.label1);
            this.gbResetPassword.Controls.Add(this.pictureBox1);
            this.gbResetPassword.Controls.Add(this.pictureBox2);
            this.gbResetPassword.Controls.Add(this.label3);
            this.gbResetPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbResetPassword.Location = new System.Drawing.Point(8, 357);
            this.gbResetPassword.Name = "gbResetPassword";
            this.gbResetPassword.Size = new System.Drawing.Size(584, 141);
            this.gbResetPassword.TabIndex = 153;
            this.gbResetPassword.TabStop = false;
            this.gbResetPassword.Text = "Reset Password";
            // 
            // ctrlConfirmPassword
            // 
            this.ctrlConfirmPassword.BackColor = System.Drawing.Color.White;
            this.ctrlConfirmPassword.Location = new System.Drawing.Point(245, 59);
            this.ctrlConfirmPassword.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlConfirmPassword.Name = "ctrlConfirmPassword";
            this.ctrlConfirmPassword.Password = "";
            this.ctrlConfirmPassword.Size = new System.Drawing.Size(305, 26);
            this.ctrlConfirmPassword.TabIndex = 6;
            this.ctrlConfirmPassword.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlConfirmNewPassword_Validating);
            // 
            // ctrlNewPassword
            // 
            this.ctrlNewPassword.BackColor = System.Drawing.Color.White;
            this.ctrlNewPassword.Location = new System.Drawing.Point(245, 14);
            this.ctrlNewPassword.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlNewPassword.Name = "ctrlNewPassword";
            this.ctrlNewPassword.Password = "";
            this.ctrlNewPassword.Size = new System.Drawing.Size(305, 26);
            this.ctrlNewPassword.TabIndex = 5;
            this.ctrlNewPassword.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlNewPassword_Validating);
            // 
            // btnReset
            // 
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = global::DVLD.Properties.Resources.Password_324;
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReset.Location = new System.Drawing.Point(451, 96);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(126, 37);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "   Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Password_322;
            this.pictureBox1.Location = new System.Drawing.Point(198, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 138;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.Password_322;
            this.pictureBox2.Location = new System.Drawing.Point(198, 59);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 140;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 20);
            this.label3.TabIndex = 139;
            this.label3.Text = "Confirm Password : ";
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable1.Location = new System.Drawing.Point(37, 152);
            this.lable1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(103, 20);
            this.lable1.TabIndex = 151;
            this.lable1.Text = "User Name:";
            // 
            // txtUserName
            // 
            this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(194, 152);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(223, 24);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserName_KeyPress);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(236, 506);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Image = global::DVLD.Properties.Resources.Password_Reset___100;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(206, 5);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(192, 128);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 150;
            this.pbPersonImage.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD.Properties.Resources.Person_32;
            this.pictureBox8.Location = new System.Drawing.Point(147, 150);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 26);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 152;
            this.pictureBox8.TabStop = false;
            // 
            // btnFind
            // 
            this.btnFind.BackgroundImage = global::DVLD.Properties.Resources.SearchPerson1;
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFind.Location = new System.Drawing.Point(439, 144);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(64, 39);
            this.btnFind.TabIndex = 2;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // frmForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(605, 549);
            this.Controls.Add(this.gbVerification);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbPersonImage);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.gbResetPassword);
            this.Controls.Add(this.lable1);
            this.Controls.Add(this.txtUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(621, 588);
            this.MinimumSize = new System.Drawing.Size(621, 588);
            this.Name = "frmForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forgot Password";
            this.Load += new System.EventHandler(this.frmForgotPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.gbVerification.ResumeLayout(false);
            this.gbVerification.PerformLayout();
            this.gbResetPassword.ResumeLayout(false);
            this.gbResetPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox gbVerification;
        private System.Windows.Forms.Button btnVerification;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtVerificationCode;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.GroupBox gbResetPassword;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.TextBox txtUserName;
        private ctrlPassword ctrlConfirmPassword;
        private ctrlPassword ctrlNewPassword;
    }
}