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
using WindowsForm_Project.Models;

namespace WindowsForm_Project.All_User_Control
{
    public partial class UC_Checkout : UserControl
    {
        public UC_Checkout()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool ValidateInput()
        {
            if (txtsophong.Text == "" || txtcccd_cus.Text == "" || txtdate_co.Value == null)
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }
        private void RefreshControl()
        {
            clearAll();
            LoadBookingData();
        }
        public void clearAll()
        {
            txtcccd_cus.Clear();
            txtsophong.Clear();
            txtdate_co.Value = DateTime.Now;
        }
        private void UC_Checkout_Leave(object sender, EventArgs e)
        {
            
        }
        private void UC_Checkout_Enter(object sender, EventArgs e)
        {
            
        }
        private void LoadBookingData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getbooking(conn);
                if (response.list9 != null && response.list9.Count > 0)
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list9;
                    DataGridView1.ColumnHeadersHeight = 25;
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView1.Width = 800;
                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }

        private void UC_Checkout_Leave_1(object sender, EventArgs e)
        {
            clearAll();
        }

        private void UC_Checkout_Enter_1(object sender, EventArgs e)
        {
            try
            {
                LoadBookingData();
                DataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsophong_TextChanged(object sender, EventArgs e)
        {
            txtcccd_cus.Clear();
            txtdate_co.Value = DateTime.Now;

            string connectionString = DatabaseConnection.Connection();
            string query = @"SELECT cccd_cus
                             FROM Bookings 
                             WHERE roomnumber = @roomnumber";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@roomnumber", txtsophong.ToString());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtcccd_cus.Text = reader.GetString(0);
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtcccd_cus.Text))
            {
                txtcccd_cus.Text = txtcccd_cus.Text;
            }
        }

        private void txtcccd_cus_TextChanged(object sender, EventArgs e)
        {
            txtdate_co.Value = DateTime.Now;

            string connectionString = DatabaseConnection.Connection();
            string query = @"SELECT date_co
                             FROM Bookings 
                             WHERE roomnumber = @roomnumber AND cccd_cus = @cccd_cus";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@roomnumber", txtsophong.ToString());
                    command.Parameters.AddWithValue("@cccd_cus", txtcccd_cus.ToString());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtdate_co.Value = reader.GetDateTime(0);
                        }
                    }
                }
            }
            if (txtdate_co.Value == DateTime.Now)
            {
                txtdate_co.Value = txtdate_co.Value;
            }
        }
    }
}
