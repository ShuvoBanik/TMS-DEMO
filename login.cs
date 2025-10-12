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
    public partial class login : Form
    {
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
            // SIMPLE TEST - Bypass all validation
            string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // First try hard-coded values to test the query
                    string testQuery = "SELECT Role FROM Login WHERE UserName = 'admin' AND Password = 'admin123'";
                    using (SqlCommand testCmd = new SqlCommand(testQuery, con))
                    {
                        object result = testCmd.ExecuteScalar();
                        if (result != null)
                        {
                            MessageBox.Show($"Hard-coded test SUCCESS! Role: {result}", "Test Result");

                            // Now test with user input
                            string username = txtUsername.Text.Trim();
                            string password = txtPassword.Text.Trim();
                            MessageBox.Show($"Your input: Username='{username}', Password='{password}'", "Debug Input");

                            // Test dynamic query
                            string dynamicQuery = $"SELECT Role FROM Login WHERE UserName = '{username}' AND Password = '{password}'";
                            MessageBox.Show($"Query: {dynamicQuery}", "Debug Query");

                            using (SqlCommand dynCmd = new SqlCommand(dynamicQuery, con))
                            {
                                object dynResult = dynCmd.ExecuteScalar();
                                if (dynResult != null)
                                {
                                    string role = dynResult.ToString();
                                    MessageBox.Show($"Dynamic test SUCCESS! Role: {role}", "Success");

                                    if (role.Equals("Admin"))
                                    {
                                        this.Hide();
                                        AdminPortal portal = new AdminPortal();
                                        portal.Show(this);
                                    }
                                    // Add other role checks...
                                }
                                else
                                {
                                    MessageBox.Show("Dynamic test FAILED - no match found", "Failed");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hard-coded test FAILED - check database setup", "Failed");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Error: {sqlEx.Message}\nError Number: {sqlEx.Number}\nSeverity: {sqlEx.Class}\nState: {sqlEx.State}", "SQL Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Error");
            }
        }
    }
}
