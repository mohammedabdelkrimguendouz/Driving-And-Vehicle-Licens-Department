namespace DVLD.Users
{
    partial class ctrlPassword
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnShowHidePassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(0, 0);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(167, 24);
            this.txtPassword.TabIndex = 1;
            // 
            // btnShowHidePassword
            // 
            this.btnShowHidePassword.BackColor = System.Drawing.Color.White;
            this.btnShowHidePassword.BackgroundImage = global::DVLD.Properties.Resources.HidePassword_32;
            this.btnShowHidePassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowHidePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowHidePassword.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnShowHidePassword.FlatAppearance.BorderSize = 0;
            this.btnShowHidePassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnShowHidePassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnShowHidePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHidePassword.Location = new System.Drawing.Point(172, 0);
            this.btnShowHidePassword.Name = "btnShowHidePassword";
            this.btnShowHidePassword.Size = new System.Drawing.Size(28, 20);
            this.btnShowHidePassword.TabIndex = 3;
            this.btnShowHidePassword.UseVisualStyleBackColor = false;
            this.btnShowHidePassword.Click += new System.EventHandler(this.btnShowHidePassword_Click);
            // 
            // ctrlPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnShowHidePassword);
            this.Controls.Add(this.txtPassword);
            this.Name = "ctrlPassword";
            this.Size = new System.Drawing.Size(207, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnShowHidePassword;
    }
}
