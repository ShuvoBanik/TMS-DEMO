using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace RSS.Properties
{
    public partial class AdminPortal : Form
    {
        public AdminPortal()
        {
            InitializeComponent();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login = new login();
            login.Show();
        }

        private void reloadData()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True");
            con.Open();
            string query = "select * from [Admin portal]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reloadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "INSERT INTO [Admin portal] ([Agent Name], Branch, Password, [Number of Customer]) " +
               "VALUES (@AgentName, @Branch, @Password, @CustomerCount)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AgentName", txtaname.Text);
            cmd.Parameters.AddWithValue("@Branch", txtbranch.Text);
            cmd.Parameters.AddWithValue("@Password", txtpass.Text);
            cmd.Parameters.AddWithValue("@CustomerCount", int.Parse(txtcus.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Insert done");
        }

        private void AdminPortal_Load(object sender, EventArgs e)
        {
            reloadData();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void txtcus_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Update button
            try
            {
                if (string.IsNullOrWhiteSpace(txtaid.Text))
                {
                    MessageBox.Show("Please enter Agent ID to update.", "Input Required");
                    return;
                }

                string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE [Admin portal] SET [Agent Name] = @AgentName, Branch = @Branch, Password = @Password, [Number of Customer] = @CustomerCount WHERE [Agent ID] = @AgentID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AgentID", txtaid.Text);
                        cmd.Parameters.AddWithValue("@AgentName", txtaname.Text);
                        cmd.Parameters.AddWithValue("@Branch", txtbranch.Text);
                        cmd.Parameters.AddWithValue("@Password", txtpass.Text);
                        cmd.Parameters.AddWithValue("@CustomerCount", int.Parse(txtcus.Text));
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully!", "Success");
                            reloadData();
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("No record found with the specified Agent ID.", "Record Not Found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message, "Error");
            }

            /*
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();


            string aid = txtaid.Text;
            string aname = txtaname.Text;
            string abranch = txtbranch.Text;
            string apassword = txtpass.Text;
            string anum = txtcus.Text;
           
            string query = "update Admin portal set Agent Name='" + aname + "', Branch=" + abranch + ", Password=" + apassword + ", Number of Customer" + txtcus + " where aid=" + aid + "";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Update done");
            */

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(e.RowIndex.ToString());
        }

        private void txtaid_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearFields()
        {
            txtaid.Clear();
            txtaname.Clear();
            txtbranch.Clear();
            txtpass.Clear();
            txtcus.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete button
            try
            {
                if (string.IsNullOrWhiteSpace(txtaid.Text))
                {
                    MessageBox.Show("Please enter Agent ID to delete.", "Input Required");
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this record?", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM [Admin portal] WHERE [Agent ID] = @AgentID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@AgentID", txtaid.Text);
                            
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully!", "Success");
                                reloadData();
                                ClearFields();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the specified Agent ID.", "Record Not Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Search functionality
            try
            {
                if (string.IsNullOrWhiteSpace(txtaid.Text))
                {
                    MessageBox.Show("Please enter Agent ID to search.", "Input Required");
                    return;
                }

                string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM [Admin portal] WHERE [Agent ID] = @AgentID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AgentID", txtaid.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtaname.Text = reader["Agent Name"]?.ToString() ?? "";
                                txtbranch.Text = reader["Branch"]?.ToString() ?? "";
                                txtpass.Text = reader["Password"]?.ToString() ?? "";
                                txtcus.Text = reader["Number of Customer"]?.ToString() ?? "";
                                MessageBox.Show("Record found!", "Success");
                            }
                            else
                            {
                                MessageBox.Show("No record found with the specified Agent ID.", "Record Not Found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching record: " + ex.Message, "Error");
            }
        }
    }
}

