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
    public partial class ForgetPassword : Form
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login = new login();
            login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Confirm button - Reset password
            ResetPassword();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ResetPassword()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter your username/ID.", "Input Required");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    MessageBox.Show("Please enter both new password and confirmation.", "Input Required");
                    return;
                }

                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("New password and confirmation password do not match.", "Password Mismatch");
                    return;
                }

                if (txtNewPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Password Too Short");
                    return;
                }

                // Check if username exists
                if (UserExists(txtUsername.Text))
                {
                    UpdatePassword(txtUsername.Text, txtNewPassword.Text);
                    MessageBox.Show("Password reset successfully!", "Success");
                    ClearForm();
                    
                    // Navigate back to login
                    this.Hide();
                    login loginForm = new login();
                    loginForm.Show();
                }
                else
                {
                    MessageBox.Show("Username/ID not found in the system.", "User Not Found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resetting password: " + ex.Message, "Error");
            }
        }

        private bool UserExists(string username)
        {
            string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Login WHERE UserID = @username OR UserName = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void UpdatePassword(string username, string newPassword)
        {
            string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE Login SET Passward = @password WHERE UserID = @username OR UserName = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", newPassword);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }
    }
}
