namespace RSS.Properties
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvlRSST = new System.Windows.Forms.Label();
            this.destination = new System.Windows.Forms.ComboBox();
            this.Flights = new System.Windows.Forms.ComboBox();
            this.Hotels = new System.Windows.Forms.ComboBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.lvlRSST);
            this.panel1.Controls.Add(this.destination);
            this.panel1.Controls.Add(this.Flights);
            this.panel1.Controls.Add(this.Hotels);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 505);
            this.panel1.TabIndex = 0;
            // 
            // lvlRSST
            // 
            this.lvlRSST.AutoSize = true;
            this.lvlRSST.BackColor = System.Drawing.Color.Transparent;
            this.lvlRSST.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlRSST.Location = new System.Drawing.Point(98, 15);
            this.lvlRSST.Name = "lvlRSST";
            this.lvlRSST.Size = new System.Drawing.Size(124, 20);
            this.lvlRSST.TabIndex = 4;
            this.lvlRSST.Text = "RSS Travelers";
            this.lvlRSST.Click += new System.EventHandler(this.lvlRSST_Click);
            // 
            // destination
            // 
            this.destination.FormattingEnabled = true;
            this.destination.Location = new System.Drawing.Point(263, 14);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(83, 21);
            this.destination.TabIndex = 3;
            this.destination.Text = "Desitnations";
            this.destination.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // Flights
            // 
            this.Flights.FormattingEnabled = true;
            this.Flights.Location = new System.Drawing.Point(410, 14);
            this.Flights.Name = "Flights";
            this.Flights.Size = new System.Drawing.Size(60, 21);
            this.Flights.TabIndex = 2;
            this.Flights.Text = "Flights";
            this.Flights.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // Hotels
            // 
            this.Hotels.FormattingEnabled = true;
            this.Hotels.Location = new System.Drawing.Point(552, 14);
            this.Hotels.Name = "Hotels";
            this.Hotels.Size = new System.Drawing.Size(60, 21);
            this.Hotels.TabIndex = 1;
            this.Hotels.Text = "Hotels";
            this.Hotels.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(681, 12);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "LogIn";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lvlRSST;
        private System.Windows.Forms.ComboBox destination;
        private System.Windows.Forms.ComboBox Flights;
        private System.Windows.Forms.ComboBox Hotels;
    }
}