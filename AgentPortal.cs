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
    public partial class AgentPortal : Form
    {
        public AgentPortal()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            login login = new login();
            login.Show();
        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void AgentPortal_Load(object sender, EventArgs e)
        {
            LoadFlightData();
            LoadHotelData();
            LoadPackageData();
        }

        private void LoadFlightData()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Flights";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    // dgvFlights.DataSource = dt; // Assuming there's a DataGridView for flights
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading flight data: " + ex.Message, "Error");
            }
        }

        private void LoadHotelData()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Hotels";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    // dgvHotels.DataSource = dt; // Assuming there's a DataGridView for hotels
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading hotel data: " + ex.Message, "Error");
            }
        }

        private void LoadPackageData()
        {
            try
            {
                // Load packages into combo box
                cmbPackages.Items.Clear();
                cmbPackages.Items.AddRange(new string[] { 
                    "Basic Package", 
                    "Premium Package", 
                    "Luxury Package", 
                    "Family Package", 
                    "Business Package" 
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading package data: " + ex.Message, "Error");
            }
        }

        private void btnSearchFlight_Click(object sender, EventArgs e)
        {
            SearchFlights();
        }

        private void SearchFlights()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFrom.Text) || string.IsNullOrWhiteSpace(txtTo.Text))
                {
                    MessageBox.Show("Please enter both departure and destination locations.", "Input Required");
                    return;
                }

                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"SELECT * FROM Flights 
                                   WHERE DepartureCity LIKE @From 
                                   AND ArrivalCity LIKE @To 
                                   AND DepartureDate >= @DepartureDate";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@From", "%" + txtFrom.Text + "%");
                        cmd.Parameters.AddWithValue("@To", "%" + txtTo.Text + "%");
                        cmd.Parameters.AddWithValue("@DepartureDate", dtpDepartureDate.Value.Date);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // dgvFlights.DataSource = dt; // Display results
                        
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No flights found for the specified criteria.", "No Results");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching flights: " + ex.Message, "Error");
            }
        }

        private void btnSearchHotel_Click(object sender, EventArgs e)
        {
            SearchHotels();
        }

        private void SearchHotels()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"SELECT * FROM Hotels 
                                   WHERE City LIKE @City 
                                   AND AvailableFrom <= @CheckIn 
                                   AND AvailableTo >= @CheckOut";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@City", "%" + txtHotelCity.Text + "%");
                        cmd.Parameters.AddWithValue("@CheckIn", dtpCheckIn.Value.Date);
                        cmd.Parameters.AddWithValue("@CheckOut", dtpCheckOut.Value.Date);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // dgvHotels.DataSource = dt; // Display results
                        
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No hotels found for the specified criteria.", "No Results");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching hotels: " + ex.Message, "Error");
            }
        }

        private void btnCalculateTotal_Click(object sender, EventArgs e)
        {
            CalculateBookingTotal();
        }

        private void CalculateBookingTotal()
        {
            try
            {
                decimal subtotal = 0;
                decimal tax = 0;
                decimal total = 0;

                // Calculate based on selected services
                if (decimal.TryParse(txtSubTotal.Text, out subtotal))
                {
                    // Add extra services
                    if (chkAirportTax.Checked)
                    {
                        subtotal += 50; // Airport tax
                    }
                    if (chkInsurance.Checked)
                    {
                        subtotal += 25; // Travel insurance
                    }

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

        private void btnGenerateBill_Click(object sender, EventArgs e)
        {
            GenerateCustomerBill();
        }

        private void GenerateCustomerBill()
        {
            try
            {
                if (ValidateBookingInfo())
                {
                    SaveCustomerBooking();
                    MessageBox.Show("Customer booking saved successfully! Bill generated.", "Success");
                    ClearBookingForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating bill: " + ex.Message, "Error");
            }
        }

        private bool ValidateBookingInfo()
        {
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
            if (cmbAccommodation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select accommodation.", "Validation Error");
                return false;
            }
            return true;
        }

        private void SaveCustomerBooking()
        {
            string connectionString = "Data Source=DESKTOP-R6ES0QJ\\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"INSERT INTO AgentBookings 
                    (DeparturePoint, Destination, Accommodation, ExtraServices, 
                     SubTotal, Tax, Total, BookingDate, AgentId) 
                    VALUES 
                    (@DeparturePoint, @Destination, @Accommodation, @ExtraServices, 
                     @SubTotal, @Tax, @Total, @BookingDate, @AgentId)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DeparturePoint", txtDeparturePoint.Text);
                    cmd.Parameters.AddWithValue("@Destination", txtDestination.Text);
                    cmd.Parameters.AddWithValue("@Accommodation", cmbAccommodation.Text);
                    cmd.Parameters.AddWithValue("@ExtraServices", GetSelectedExtraServices());
                    cmd.Parameters.AddWithValue("@SubTotal", decimal.Parse(txtSubTotal.Text));
                    cmd.Parameters.AddWithValue("@Tax", decimal.Parse(txtTax.Text));
                    cmd.Parameters.AddWithValue("@Total", decimal.Parse(txtTotal.Text));
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@AgentId", GetCurrentAgentId());

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetSelectedExtraServices()
        {
            List<string> services = new List<string>();
            if (chkAirportTax.Checked) services.Add("Airport Tax");
            if (chkInsurance.Checked) services.Add("Travel Insurance");
            return string.Join(", ", services);
        }

        private int GetCurrentAgentId()
        {
            // This should be set when the agent logs in
            // For now, return a default value
            return 1;
        }

        private void ClearBookingForm()
        {
            txtDeparturePoint.Clear();
            txtDestination.Clear();
            txtSubTotal.Clear();
            txtTax.Clear();
            txtTotal.Clear();
            cmbAccommodation.SelectedIndex = -1;
            chkAirportTax.Checked = false;
            chkInsurance.Checked = false;
        }
    }
}
