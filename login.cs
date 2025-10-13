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
using static System.Net.Mime.MediaTypeNames;


namespace RSS.Properties
{
    public partial class login : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        public login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();

        }

        private void btnsignup_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.Show();
        }

        private void btnclickhere_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForgetPassword forgetPassword = new ForgetPassword();
            forgetPassword.Show();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter Username and Password!");
                return;
            }

            try
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Login WHERE Username=@UserName AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    MessageBox.Show("Login Successful!");
                    // Open next form
                    Owner owner= new Owner();
                     owner.Show();
                    //AdminPortal admin = new AdminPortal();
                    //admin.Show(); 
                    //AgentPortal agent = new AgentPortal();
                    //agent.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Invalid Username or Password!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



    }
}
