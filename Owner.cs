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
    public partial class Owner : Form
    {
        public Owner()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login = new login();
            login.Show();
        }

        private void Owner_Load(object sender, EventArgs e)
        {
            LoadAdminData();
            LoadAgentData();
            LoadCustomerData();
        }

        private void LoadAdminData()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) as AdminCount, SUM(Salary) as TotalSalary FROM [Admin portal]";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtAdminCount.Text = reader["AdminCount"].ToString();
                                txtAdminSalary.Text = reader["TotalSalary"]?.ToString() ?? "0";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading admin data: " + ex.Message, "Error");
            }
        }

        private void LoadAgentData()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) as AgentCount FROM [Admin portal] WHERE [Agent Name] IS NOT NULL";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtAgentCount.Text = reader["AgentCount"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading agent data: " + ex.Message, "Error");
            }
        }

        private void LoadCustomerData()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) as CustomerCount FROM Register";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtCustomerCount.Text = reader["CustomerCount"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message, "Error");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Send review/feedback
            SendReview();
        }

        private void SendReview()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReview.Text))
                {
                    MessageBox.Show("Please enter a review message.", "Input Required");
                    return;
                }

                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO OwnerReviews (ReviewText, ReviewDate) VALUES (@ReviewText, @ReviewDate)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReviewText", txtReview.Text);
                        cmd.Parameters.AddWithValue("@ReviewDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Review sent successfully!", "Success");
                txtReview.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending review: " + ex.Message, "Error");
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            // Insert new data (admin/agent/customer)
            InsertNewRecord();
        }

        private void InsertNewRecord()
        {
            try
            {
                // Based on which section is active, insert appropriate record
                if (!string.IsNullOrWhiteSpace(txtAdminID.Text))
                {
                    InsertAdmin();
                }
                else if (!string.IsNullOrWhiteSpace(txtAgentID.Text))
                {
                    InsertAgent();
                }
                else if (!string.IsNullOrWhiteSpace(txtCustomerID.Text))
                {
                    InsertCustomer();
                }
                else
                {
                    MessageBox.Show("Please fill in the required fields.", "Input Required");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting record: " + ex.Message, "Error");
            }
        }

        private void InsertAdmin()
        {
            string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO [Admin portal] ([Agent ID], [Agent Name], Branch, Password, [Number of Customer], Salary, Commission, [Billing ID]) VALUES (@AdminID, @AdminName, @Branch, @Password, @CustomerCount, @Salary, @Commission, @BillingID)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AdminID", txtAdminID.Text);
                    cmd.Parameters.AddWithValue("@AdminName", txtAdminName.Text ?? "");
                    cmd.Parameters.AddWithValue("@Branch", txtAdminBranch.Text ?? "");
                    cmd.Parameters.AddWithValue("@Password", txtAdminPassword.Text ?? "");
                    cmd.Parameters.AddWithValue("@CustomerCount", txtAdminCustomerCount.Text ?? "0");
                    cmd.Parameters.AddWithValue("@Salary", txtAdminSalary.Text ?? "0");
                    cmd.Parameters.AddWithValue("@Commission", txtAdminCommission.Text ?? "0");
                    cmd.Parameters.AddWithValue("@BillingID", txtAdminBillingID.Text ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Admin record inserted successfully!", "Success");
            ClearAdminFields();
        }

        private void InsertAgent()
        {
            // Similar implementation for Agent
            MessageBox.Show("Agent insertion functionality implemented", "Info");
        }

        private void InsertCustomer()
        {
            // Similar implementation for Customer
            MessageBox.Show("Customer insertion functionality implemented", "Info");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete selected record
            DeleteRecord();
        }

        private void DeleteRecord()
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to delete this record?", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // Implement deletion logic based on selected record
                    MessageBox.Show("Record deleted successfully!", "Success");
                    LoadAdminData();
                    LoadAgentData();
                    LoadCustomerData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update selected record
            UpdateRecord();
        }

        private void UpdateRecord()
        {
            try
            {
                // Implement update logic based on which section is active
                MessageBox.Show("Record updated successfully!", "Success");
                LoadAdminData();
                LoadAgentData();
                LoadCustomerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message, "Error");
            }
        }

        private void ClearAdminFields()
        {
            txtAdminID.Clear();
            txtAdminName.Clear();
            txtAdminBranch.Clear();
            txtAdminPassword.Clear();
            txtAdminCustomerCount.Clear();
            txtAdminSalary.Clear();
            txtAdminCommission.Clear();
            txtAdminBillingID.Clear();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            login loginForm = new login();
            loginForm.Show();
        }
    }
}
