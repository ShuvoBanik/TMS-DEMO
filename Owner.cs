using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;


namespace RSS.Properties
{
    public partial class Owner : Form
    {
        // Remove the dataset reference since it's causing issues
        // private RSS_TMSDataSet rSS_TMSDataSet;

        public Owner()
        {
            InitializeComponent();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login = new login();
            login.Show();
        }

        private void GridViewAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Add cell click handling if needed
        }

        private void Owner_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rSS_TMSDataSet2.Customer_Owner' table. You can move, or remove it, as needed.
            this.customer_OwnerTableAdapter.Fill(this.rSS_TMSDataSet2.Customer_Owner);
            // TODO: This line of code loads data into the 'rSS_TMSDataSet1.Agent_Owner' table. You can move, or remove it, as needed.
            this.agent_OwnerTableAdapter.Fill(this.rSS_TMSDataSet1.Agent_Owner);
            try
            {
                ReloadData(); // Use our custom method to load data
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
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

                    GridViewAdmin.AutoGenerateColumns = true;
                    GridViewAdmin.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reloading data: " + ex.Message);
            }
        }

        private void ShowData()
        {
            try
            {
                string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Admin_Portal";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    GridViewAdmin.AutoGenerateColumns = true;
                    GridViewAdmin.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error showing data: " + ex.Message);
            }
        }

        public void ClearFields()
        {
            txtAdminId.Text = "";
            txtAdminPassword.Text = "";
            txtNoofAgent.Text = "";
            txtAdminSal.Text = "";
            txtAdminComm.Text = "";
        }

        private void BtnAdminInsert_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(txtAdminId.Text) ||
                    string.IsNullOrWhiteSpace(txtAdminPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtNoofAgent.Text) ||
                    string.IsNullOrWhiteSpace(txtAdminSal.Text) ||
                    string.IsNullOrWhiteSpace(txtAdminComm.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Admin_Portal (Admin_ID, Admin_Password, No_of_Agent, Salary, Commission) " +
                                   "VALUES (@Admin_ID, @Admin_Password, @No_of_Agent, @Salary, @Commission)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Use parameters for safety and correct data handling
                        cmd.Parameters.AddWithValue("@Admin_ID", txtAdminId.Text);
                        cmd.Parameters.AddWithValue("@Admin_Password", txtAdminPassword.Text);

                        // Convert numeric values properly
                        cmd.Parameters.AddWithValue("@No_of_Agent", Convert.ToInt32(txtNoofAgent.Text));
                        cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtAdminSal.Text));
                        cmd.Parameters.AddWithValue("@Commission", Convert.ToDecimal(txtAdminComm.Text));

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Admin data inserted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Insert failed.");
                        }
                    }
                }

                ClearFields();       // Reset all input fields
                ReloadData();  // Refresh DataGridView
            }
            catch (FormatException)
            {
                MessageBox.Show("Please check your numeric fields (No of Agent, Salary, Commission) for correct format.");
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627) // Primary key violation
                {
                    MessageBox.Show("Admin ID already exists. Please use a different ID.");
                }
                else
                {
                    MessageBox.Show("Database error: " + sqlEx.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data: " + ex.Message);
            }
        }

        // Add these methods for better functionality
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void GridViewAdmin_SelectionChanged(object sender, EventArgs e)
        {
            // Optional: Populate text boxes when a row is selected
            if (GridViewAdmin.CurrentRow != null && GridViewAdmin.CurrentRow.DataBoundItem is DataRowView row)
            {
                txtAdminId.Text = row["Admin_ID"].ToString();
                txtAdminPassword.Text = row["Admin_Password"].ToString();
                txtNoofAgent.Text = row["No_of_Agent"].ToString();
                txtAdminSal.Text = row["Salary"].ToString();
                txtAdminComm.Text = row["Commission"].ToString();
            }
        }

        private void btnAgentInsert_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Use parameterized query to prevent SQL injection and handle data types properly
                string query = "INSERT INTO Agent_Owner (Agent_Id, No_of_Customer, Password, Salary, Commission) " +
                               "VALUES (@AgentId, @NoOfCustomer, @Password, @Salary, @Commission)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters with proper data types
                    cmd.Parameters.AddWithValue("@AgentId", txtAid.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfCustomer", Convert.ToInt32(txtCus.Text));
                    cmd.Parameters.AddWithValue("@Password", txtPass.Text.Trim());
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSal.Text));
                    cmd.Parameters.AddWithValue("@Commission", Convert.ToDecimal(txtAComm.Text));

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

            ClearFields();
            ReloadData();
            ShowData(); // Remove this if ReloadData() already refreshes the display
            /*string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "insert into Agent_Owner (Agent_Id,No_of_Customer,Password,Salary,Commission) values ('" + txtAid + "'," + txtPass + "," + txtCus + "," + txtSal + "," + txtAComm + ")";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            ClearFields();
            ReloadData();
            ShowData();
            */
        }

        private void btnCusInsert_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Use parameterized query to prevent SQL injection and handle data types properly
                string query = "INSERT INTO Customer_Owner (Customer_Id, No_of_Package, Password, Bill, Activity) " +
                               "VALUES (@CustomerId, @NoOfPackage, @Password, @Bill, @Activity)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters with proper data types
                    cmd.Parameters.AddWithValue("@CustomerId", txtCid.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfPackage", Convert.ToInt32(txtNoofpack.Text));
                    cmd.Parameters.AddWithValue("@Password", txtCPass.Text.Trim());
                    cmd.Parameters.AddWithValue("@Bill", Convert.ToDecimal(txtBill.Text));
                    cmd.Parameters.AddWithValue("@Activity", c.Text.Trim());

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer data inserted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Insert failed.");
                    }
                }
            }

            ClearFields();
            ReloadData();
            ShowData(); // Remove this if ReloadData() already refreshes the display
        }

        private void btnAdminUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Use parameterized query to prevent SQL injection
                string query = "UPDATE Admin_Portal SET Admin_Password = @Password, No_of_Agent = @NoOfAgent, " +
                               "Salary = @Salary, Commission = @Commission WHERE Admin_ID = @AdminId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters with proper data types
                    cmd.Parameters.AddWithValue("@Password", txtAdminPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfAgent", Convert.ToInt32(txtNoofAgent.Text));
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtAdminSal.Text));
                    cmd.Parameters.AddWithValue("@Commission", Convert.ToDecimal(txtAdminComm.Text));
                    cmd.Parameters.AddWithValue("@AdminId", txtAdminId.Text.Trim());

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update done successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No records updated. Please check if the Admin ID exists.");
                    }
                }
            }

            ClearFields();
            ReloadData();
            /*string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string Admin_ID = txtAdminId.Text;
                string Password = txtAdminPassword.Text;
                int No_of_Agent = Convert.ToInt32(txtNoofAgent.Text);
                double Salary = Convert.ToSingle(txtAdminSal.Text);
                double Commission = Convert.ToSingle(txtAdminComm.Text); ;
                

                string query = "update Customer_Owner set Password='" + txtAdminPassword.Text + "', No_of_Agent=" + No_of_Agent + ", Salary=" + Salary + ", Commission=" + Commission + " where Admin_ID=" + txtAdminId.Text + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update done");


                ClearFields();
                ReloadData();
                */
            ShowData(); // Remove this if ReloadData() already refreshes the display

            /*string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        string query = "update Admin_Portal set Password=" + txtAdminPassword.Text + ", No_of_Agent=" + txtNoofAgent.Text + ", Salary=" + txtAdminSal.Text + ", Commission=" + txtAdminComm.Text + " where Admin_ID='" + txtAdminId.Text + "'";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        ClearFields();
        ReloadData();
        ShowData();
        */
        }

        private void btnAdminDelete_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();


            string Admin_ID = txtAdminId.Text;

            string query = "delete from Admin_Portal where Admin_ID = '" + Admin_ID + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Delete done");

        }

        private void btnAgentUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Use parameterized query to prevent SQL injection
                string query = "UPDATE Agent_Owner SET Password = @Password, No_of_Customer = @NoOfCustomer, " +
                               "Salary = @Salary, Commission = @Commission WHERE Agent_ID = @AgentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters with proper data types
                    cmd.Parameters.AddWithValue("@Password", txtPass.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfCustomer", Convert.ToInt32(txtCus.Text));
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSal.Text));
                    cmd.Parameters.AddWithValue("@Commission", Convert.ToDecimal(txtAComm.Text));
                    cmd.Parameters.AddWithValue("@AgentId", txtAid.Text.Trim());

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

            ClearFields();
            ReloadData();
        }

        private void btnAgentDelete_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();


            string Agent_ID = txtAid.Text;

            string query = "delete from Agent_Owner where Agent_ID = '" + Agent_ID + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Delete done");
        }

        private void btnCusUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Use parameterized query to prevent SQL injection
                string query = "UPDATE Customer_Owner SET Password = @Password, No_of_Package = @NoOfPackage, " +
                               "Bill = @Bill, Activity = @Activity WHERE Customer_ID = @CustomerId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters with proper data types
                    cmd.Parameters.AddWithValue("@Password", txtCPass.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfPackage", Convert.ToInt32(txtNoofpack.Text)); // Fixed textbox name
                    cmd.Parameters.AddWithValue("@Bill", Convert.ToDecimal(txtBill.Text));
                    cmd.Parameters.AddWithValue("@Activity", Convert.ToInt32(c.Text)); // Fixed textbox name
                    cmd.Parameters.AddWithValue("@CustomerId", txtCid.Text.Trim());

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer data updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No records updated. Please check if the Customer ID exists.");
                    }
                }
            }

            ClearFields();
            ReloadData();
        }

        private void btnCusDelete_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;TrustServerCertificate=True";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();


            string Customer_ID = txtCid.Text;

            string query = "delete from Customer_Owner where Customer_ID = '" + Customer_ID + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Delete done");
        }
    }
}