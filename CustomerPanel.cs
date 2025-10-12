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
    public partial class CustomerPanel : Form
    {
        public CustomerPanel()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Calculate Total button
            try
            {
                decimal subtotal = 0;
                decimal tax = 0;
                decimal total = 0;

                // Get values from text boxes (assuming they exist in the designer)
                if (decimal.TryParse(txtSubTotal.Text, out subtotal))
                {
                    tax = subtotal * 0.1m; // 10% tax
                    total = subtotal + tax;
                    
                    txtTax.Text = tax.ToString("F2");
                    txtTotal.Text = total.ToString("F2");
                }
                else
                {
                    MessageBox.Show("Please enter a valid subtotal amount.", "Invalid Input");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating total: " + ex.Message, "Error");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login = new login();
            login.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerateBill_Click(object sender, EventArgs e)
        {
            // Generate Bill button functionality
            try
            {
                if (ValidateCustomerInfo())
                {
                    SaveBookingToDatabase();
                    MessageBox.Show("Booking saved successfully! Bill generated.", "Success");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating bill: " + ex.Message, "Error");
            }
        }

        private bool ValidateCustomerInfo()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter your name.", "Validation Error");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                MessageBox.Show("Please enter your surname.", "Validation Error");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter your email.", "Validation Error");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDeparturePoint.Text))
            {
                MessageBox.Show("Please enter departure point.", "Validation Error");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDestination.Text))
            {
                MessageBox.Show("Please enter destination.", "Validation Error");
                return false;
            }
            return true;
        }

        private void SaveBookingToDatabase()
        {
            string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"INSERT INTO CustomerBookings 
                    (Name, Surname, DateOfBirth, Gender, Address, Email, 
                     DeparturePoint, Destination, Accommodation, Package, 
                     FlightType, TicketType, SubTotal, Tax, Total, BookingDate) 
                    VALUES 
                    (@Name, @Surname, @DateOfBirth, @Gender, @Address, @Email, 
                     @DeparturePoint, @Destination, @Accommodation, @Package, 
                     @FlightType, @TicketType, @SubTotal, @Tax, @Total, @BookingDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", dtpDateOfBirth.Value);
                    cmd.Parameters.AddWithValue("@Gender", rbMale.Checked ? "Male" : "Female");
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DeparturePoint", txtDeparturePoint.Text);
                    cmd.Parameters.AddWithValue("@Destination", txtDestination.Text);
                    cmd.Parameters.AddWithValue("@Accommodation", cmbAccommodation.Text);
                    cmd.Parameters.AddWithValue("@Package", cmbPackage.Text);
                    cmd.Parameters.AddWithValue("@FlightType", GetSelectedFlightType());
                    cmd.Parameters.AddWithValue("@TicketType", GetSelectedTicketType());
                    cmd.Parameters.AddWithValue("@SubTotal", decimal.Parse(txtSubTotal.Text));
                    cmd.Parameters.AddWithValue("@Tax", decimal.Parse(txtTax.Text));
                    cmd.Parameters.AddWithValue("@Total", decimal.Parse(txtTotal.Text));
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetSelectedFlightType()
        {
            if (rbEconomy.Checked) return "Economy";
            if (rbFirstClass.Checked) return "First Class";
            if (rbBusinessClass.Checked) return "Business Class";
            return "Economy";
        }

        private string GetSelectedTicketType()
        {
            if (rbOneWay.Checked) return "One Way";
            if (rbRoundTrip.Checked) return "Round Trip";
            return "One Way";
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            txtDeparturePoint.Clear();
            txtDestination.Clear();
            txtSubTotal.Clear();
            txtTax.Clear();
            txtTotal.Clear();
            cmbAccommodation.SelectedIndex = -1;
            cmbPackage.SelectedIndex = -1;
            rbMale.Checked = true;
            rbEconomy.Checked = true;
            rbOneWay.Checked = true;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            // Show booking history
            LoadBookingHistory();
        }

        private void LoadBookingHistory()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM CustomerBookings ORDER BY BookingDate DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // Assuming there's a DataGridView to show booking history
                    if (dgvBookings != null)
                    {
                        dgvBookings.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading booking history: " + ex.Message, "Error");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete selected booking
            if (dgvBookings != null && dgvBookings.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete this booking?", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    DeleteSelectedBooking();
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to delete.", "No Selection");
            }
        }

        private void DeleteSelectedBooking()
        {
            try
            {
                int bookingId = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells["BookingId"].Value);
                
                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM CustomerBookings WHERE BookingId = @BookingId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BookingId", bookingId);
                        cmd.ExecuteNonQuery();
                    }
                }
                
                MessageBox.Show("Booking deleted successfully.", "Success");
                LoadBookingHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting booking: " + ex.Message, "Error");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            login loginForm = new login();
            loginForm.Show();
        }
    }
}
