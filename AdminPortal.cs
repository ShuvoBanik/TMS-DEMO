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

        private void ReloadData()
        {
            try
            {
                string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Admin_Portal";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    GridViewAdmin2.AutoGenerateColumns = true;
                    GridViewAdmin2.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reloading data: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void AdminPortal_Load(object sender, EventArgs e)
        {
            this.customer_OwnerTableAdapter.Fill(this.rSS_TMSDataSet6.Customer_Owner);
            this.admin2TableAdapter2.Fill(this.rSS_TMSDataSet5.Admin2);
            this.admin2TableAdapter1.Fill(this.rSS_TMSDataSet4.Admin2);
            
            this.admin2TableAdapter.Fill(this.rSS_TMSDataSet3.Admin2);
            ReloadData();
            txtaid.ReadOnly = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void txtcus_TextChanged(object sender, EventArgs e)
        {

        }

        /*private void button5_Click(object sender, EventArgs e)
        {
            
            
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

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = GridViewAdmin2.Rows[e.RowIndex];

                txtaid.Text = row.Cells["Agent ID"].Value.ToString();
                txtaname.Text = row.Cells["Agent Name"].Value.ToString();
                txtbranch.Text = row.Cells["Branch"].Value.ToString();
                txtpass.Text = row.Cells["Password"].Value.ToString();
                txtcus.Text = row.Cells["Number of Customer"].Value.ToString();
            }
        }

        

        private void txtaid_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                
                string query = "INSERT INTO Admin2 (Agent_Id, Agent_Name, Branch, Password, No_of_Customer) " +
                               "VALUES (@AgentId, @AgentName, @Branch, @Password, @NoOfCustomer)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@AgentId", txtaid.Text.Trim());
                    cmd.Parameters.AddWithValue("@AgentName", txtaname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Branch", txtbranch.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtpass.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfCustomer", Convert.ToInt32(txtcus.Text));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Agent data inserted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Insert failed.");
                    }
                }
            }

            ReloadData();

        }

        private void txtaname_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                
                string query = "UPDATE Admin2 SET Agent_Name = @AgentName, Branch = @Branch, " +
                               "Password = @Password, No_of_Customer = @NoOfCustomer WHERE Agent_ID = @AgentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters with proper data types
                    cmd.Parameters.AddWithValue("@AgentName", txtaname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Branch", txtbranch.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtpass.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfCustomer", Convert.ToInt32(txtcus.Text));
                    cmd.Parameters.AddWithValue("@AgentId", txtaid.Text.Trim());

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Agent data updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No records updated. Please check if the Agent ID exists.");
                    }
                }
            }
        }

        private void GridViewAdmin2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();


            string Agent_ID = txtaid.Text;

            string query = "delete from Admin2 where Agent_ID = '" + Agent_ID + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Delete done");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtaid_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                
                string query = "SELECT * FROM Customer_Owner WHERE Name LIKE @SearchText";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@SearchText", "%" + txtsearch.Text.Trim() + "%");

                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        GridViewAdmin2.AutoGenerateColumns = true;
                        GridViewAdmin2.DataSource = dt;
                    }
                }
            }
        }

        
    }
}

