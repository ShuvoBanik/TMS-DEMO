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
using System.Xml.Linq;

namespace TMS
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void connect()
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "insert into Name (Name,Sername) values ('" + Name + "'," + Sername + ")";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();

        }

        private void Registration_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click_2(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Sername_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-R6ES0QJ\SQLEXPRESS;Initial Catalog=RSS-TMS;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "select * from Payment";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void Single_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
