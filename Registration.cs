using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSS.Properties
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login = new login();
            login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnsignup_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True");
                con.Open();
                string query = "INSERT INTO Register (Name, Email, Username, Password) VALUES (@Name, @Email, @Username, @Password)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registration completed successfully!");
                con.Close();
                
                this.Hide();
                login loginForm = new login();
                loginForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
