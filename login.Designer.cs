namespace RSS.Properties
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnexit = new System.Windows.Forms.Button();
            this.btnback = new System.Windows.Forms.Button();
            this.btnclickhere = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnsignup = new System.Windows.Forms.Button();
            this.btnlogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.Label();
            this.RSSTvl = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.txtUsername);
            this.panel1.Controls.Add(this.btnexit);
            this.panel1.Controls.Add(this.btnback);
            this.panel1.Controls.Add(this.btnclickhere);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnsignup);
            this.panel1.Controls.Add(this.btnlogin);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.password);
            this.panel1.Controls.Add(this.username);
            this.panel1.Controls.Add(this.RSSTvl);
            this.panel1.Location = new System.Drawing.Point(403, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 457);
            this.panel1.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(150, 179);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(161, 20);
            this.txtPassword.TabIndex = 11;
            this.txtPassword.Text = "admin";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(150, 132);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(161, 20);
            this.txtUsername.TabIndex = 10;
            this.txtUsername.Text = "3333";
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(253, 386);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 9;
            this.btnexit.Text = "Exit";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnback
            // 
            this.btnback.Location = new System.Drawing.Point(52, 386);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(75, 23);
            this.btnback.TabIndex = 8;
            this.btnback.Text = "Back";
            this.btnback.UseVisualStyleBackColor = true;
            this.btnback.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnclickhere
            // 
            this.btnclickhere.Location = new System.Drawing.Point(207, 335);
            this.btnclickhere.Name = "btnclickhere";
            this.btnclickhere.Size = new System.Drawing.Size(75, 23);
            this.btnclickhere.TabIndex = 7;
            this.btnclickhere.Text = "Click here ";
            this.btnclickhere.UseVisualStyleBackColor = true;
            this.btnclickhere.Click += new System.EventHandler(this.btnclickhere_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Forget Password ?";
            // 
            // btnsignup
            // 
            this.btnsignup.Location = new System.Drawing.Point(253, 282);
            this.btnsignup.Name = "btnsignup";
            this.btnsignup.Size = new System.Drawing.Size(75, 23);
            this.btnsignup.TabIndex = 5;
            this.btnsignup.Text = "Sing Up ";
            this.btnsignup.UseVisualStyleBackColor = true;
            this.btnsignup.Click += new System.EventHandler(this.btnsignup_Click);
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(52, 282);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(75, 23);
            this.btnlogin.TabIndex = 4;
            this.btnlogin.Text = "Login ";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(221, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Don\'t you have any account please sign up ?";
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.BackColor = System.Drawing.SystemColors.Control;
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.ForeColor = System.Drawing.SystemColors.ControlText;
            this.password.Location = new System.Drawing.Point(22, 183);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(83, 16);
            this.password.TabIndex = 2;
            this.password.Text = "Password :";
            this.password.Click += new System.EventHandler(this.label3_Click);
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.BackColor = System.Drawing.SystemColors.Control;
            this.username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.ForeColor = System.Drawing.SystemColors.ControlText;
            this.username.Location = new System.Drawing.Point(22, 136);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(114, 16);
            this.username.TabIndex = 1;
            this.username.Text = "Username / ID :";
            // 
            // RSSTvl
            // 
            this.RSSTvl.AutoSize = true;
            this.RSSTvl.BackColor = System.Drawing.Color.Transparent;
            this.RSSTvl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RSSTvl.Location = new System.Drawing.Point(139, 45);
            this.RSSTvl.Name = "RSSTvl";
            this.RSSTvl.Size = new System.Drawing.Size(143, 24);
            this.RSSTvl.TabIndex = 0;
            this.RSSTvl.Text = "RSS Travelers";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(-5, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 471);
            this.panel2.TabIndex = 1;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "login";
            this.Text = "login";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.Label RSSTvl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnback;
        private System.Windows.Forms.Button btnclickhere;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnsignup;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
    }
}