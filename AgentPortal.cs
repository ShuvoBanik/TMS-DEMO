using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RSS.Properties
{
    public partial class AgentPortal : Form
    {
        private string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS_TMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public AgentPortal()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(7);
            dateTimePicker2.Value = DateTime.Now.AddDays(14);
            dateTimePicker4.Value = DateTime.Now.AddDays(7);
            dateTimePicker3.Value = DateTime.Now.AddDays(10);

            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;

            LoadBookings();
        }

        private void BtnSaveBooking_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                    INSERT INTO Agent2 (
                        [Flight], [From], [To], [Departure_Date], [Return_Date], [Traveller_Class],
                        [Hotel], [Check_IN], [Check_Out], [Room_Guests], [Search_For],
                        [Packages], [Departure_Point], [Destination], [Extra_Service],
                        [Sub_Total], [Tax], [Total]
                    ) VALUES (
                        @Flight, @From, @To, @DepartureDate, @ReturnDate, @TravellerClass,
                        @Hotel, @CheckIN, @CheckOut, @RoomGuests, @SearchFor,
                        @Packages, @DeparturePoint, @Destination, @ExtraService,
                        @SubTotal, @Tax, @Total
                    )";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Flight Section
                    cmd.Parameters.AddWithValue("@Flight", GetSelectedFlightType());
                    cmd.Parameters.AddWithValue("@From", txtFrom.Text.Trim());
                    cmd.Parameters.AddWithValue("@To", txtTo.Text.Trim());
                    cmd.Parameters.AddWithValue("@DepartureDate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@ReturnDate", GetSelectedFlightType() == "Round Way" ? dateTimePicker2.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TravellerClass", GetSelectedTravellerClass());

                    // Hotel Section
                    cmd.Parameters.AddWithValue("@Hotel", txtHotel.Text.Trim());
                    cmd.Parameters.AddWithValue("@CheckIN", dateTimePicker4.Value);
                    cmd.Parameters.AddWithValue("@CheckOut", dateTimePicker3.Value);
                    cmd.Parameters.AddWithValue("@RoomGuests", GetRoomGuestsValue());
                    cmd.Parameters.AddWithValue("@SearchFor", GetSelectedSearchFor());

                    // Packages Section
                    cmd.Parameters.AddWithValue("@Packages", comboBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@DeparturePoint", txtDeperture.Text.Trim());
                    cmd.Parameters.AddWithValue("@Destination", txtDestination.Text.Trim());
                    cmd.Parameters.AddWithValue("@ExtraService", GetExtraServices());

                    // Pricing
                    decimal subTotal = CalculateSubTotal();
                    decimal tax = subTotal * 0.10m;
                    decimal total = subTotal + tax;

                    cmd.Parameters.AddWithValue("@SubTotal", subTotal);
                    cmd.Parameters.AddWithValue("@Tax", tax);
                    cmd.Parameters.AddWithValue("@Total", total);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Booking saved successfully!");
                        ClearForm();
                        LoadBookings();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving booking: " + ex.Message);
            }
        }

        private decimal CalculateSubTotal()
        {
            decimal total = 0;

            // Flight calculation
            if (!string.IsNullOrWhiteSpace(txtFrom.Text) && !string.IsNullOrWhiteSpace(txtTo.Text))
            {
                total += GetFlightPrice();
            }

            // Hotel calculation
            if (!string.IsNullOrWhiteSpace(txtHotel.Text))
            {
                total += GetHotelPrice();
            }

            // Package calculation
            if (!string.IsNullOrWhiteSpace(comboBox3.Text))
            {
                total += GetPackagePrice();
            }

            return total;
        }

        private decimal GetFlightPrice()
        {
            decimal basePrice = 0;
            string travellerClass = GetSelectedTravellerClass();

            switch (travellerClass)
            {
                case "Economy": basePrice = 500.00m; break;
                case "Business": basePrice = 1200.00m; break;
                case "First Class": basePrice = 2500.00m; break;
                default: basePrice = 500.00m; break;
            }

            if (GetSelectedFlightType() == "Round Way")
                basePrice *= 1.8m; 

            return basePrice;
        }

        private decimal GetHotelPrice()
        {
            int nights = (dateTimePicker3.Value - dateTimePicker4.Value).Days;
            if (nights <= 0) nights = 1;

            decimal basePrice = 0;
            string searchFor = GetSelectedSearchFor();

            switch (searchFor)
            {
                case "Business": basePrice = 200.00m; break;
                case "Couple": basePrice = 300.00m; break;
                case "Solo": basePrice = 150.00m; break;
                case "Family": basePrice = 400.00m; break;
                case "Friends": basePrice = 250.00m; break;
                default: basePrice = 200.00m; break;
            }

            return basePrice * nights * GetRoomGuestsValue();
        }

        private decimal GetPackagePrice()
        {
            if (txtDestination.Text.ToLower().Contains("europe")) return 3000.00m;
            if (txtDestination.Text.ToLower().Contains("asia")) return 2500.00m;
            if (txtDestination.Text.ToLower().Contains("america")) return 2000.00m;
            return 1500.00m;
        }

        private string GetSelectedFlightType()
        {
            if (radioButton1.Checked) return "One Way";
            if (radioButton2.Checked) return "Round Way";
            if (radioButton3.Checked) return "Multi Way";
            return "One Way";
        }

        private string GetSelectedTravellerClass()
        {
            if (comboBox1.SelectedItem != null)
                return comboBox1.SelectedItem.ToString();
            return "Economy";
        }

        private string GetSelectedSearchFor()
        {
            if (checkBox1.Checked) return "Business";
            if (checkBox2.Checked) return "Couple";
            if (checkBox3.Checked) return "Solo";
            if (checkBox4.Checked) return "Family";
            if (checkBox5.Checked) return "Friends";
            return "Business";
        }

        private int GetRoomGuestsValue()
        {
            if (comboBox2.SelectedItem != null && int.TryParse(comboBox2.SelectedItem.ToString(), out int guests))
                return guests;
            return 1;
        }

        private string GetExtraServices()
        {
            string services = "";
            if (checkBox6.Checked) services += "Airport Tax, ";
            if (checkBox7.Checked) services += "Travel Insurance, ";

            return services.TrimEnd(',', ' ');
        }

        private void LoadBookings()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                    SELECT TOP 1000 
                        [Flight], [From], [To], [Departure_Date], [Return_Date], [Traveller_Class],
                        [Hotel], [Check_IN], [Check_Out], [Room_Guests], [Search_For],
                        [Packages], [Departure_Point], [Destination], [Extra_Service],
                        [Sub_Total], [Tax], [Total]
                    FROM Agent2 
                    ORDER BY [Departure_Date] DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bookings: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            // Flight Section
            txtFrom.Clear();
            txtTo.Clear();
            dateTimePicker1.Value = DateTime.Now.AddDays(7);
            dateTimePicker2.Value = DateTime.Now.AddDays(14);
            radioButton1.Checked = true;
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;

            // Hotel Section
            txtHotel.Clear();
            dateTimePicker4.Value = DateTime.Now.AddDays(7);
            dateTimePicker3.Value = DateTime.Now.AddDays(10);
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox1.Checked = true; 

            comboBox3.Text = "";
            txtDeperture.Clear();
            txtDestination.Clear();
            checkBox6.Checked = false;
            checkBox7.Checked = false;

            txtSubTotal.Text = "$0.00";
            txtTax.Text = "$0.00";
            txtTotal.Text = "$0.00";
        }

        private void CalculateTotal_Click(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void CalculateAndDisplayPrices()
        {
            decimal subTotal = CalculateSubTotal();
            decimal tax = subTotal * 0.10m;
            decimal total = subTotal + tax;

            txtSubTotal.Text = subTotal.ToString("C2");
            txtTax.Text = tax.ToString("C2");
            txtTotal.Text = total.ToString("C2");
        }

        private void TxtFrom_TextChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void TxtTo_TextChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void TxtHotel_TextChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void TxtDestination_TextChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
            dateTimePicker2.Enabled = radioButton2.Checked;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
            dateTimePicker2.Enabled = radioButton2.Checked;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
            dateTimePicker2.Enabled = false;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            CalculateAndDisplayPrices();
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            CalculateAndDisplayPrices();
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            CalculateAndDisplayPrices();
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox5.Checked = false;
            }
            CalculateAndDisplayPrices();
        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
            CalculateAndDisplayPrices();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
        }

        private void BtnClearForm_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void BtnGenerateBill_Click(object sender, EventArgs e)
        {
            CalculateAndDisplayPrices();
            MessageBox.Show("Bill generated successfully!\n\n" +
                          $"Sub Total: {txtSubTotal.Text}\n" +
                          $"Tax: {txtTax.Text}\n" +
                          $"Total: {txtTotal.Text}",
                          "Bill Summary");
        }

        

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            login login = new login();
            login.Show();
        }
    }

    
}
