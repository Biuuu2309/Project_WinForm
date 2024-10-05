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
            LoadCheckoutData();
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

        private void LoadCheckoutData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getcheckout(conn);
                if (response.list11 != null && response.list11.Count > 0)
                {
                    DataGridView2.DataSource = null; // Clear previous data
                    DataGridView2.DataSource = response.list11;
                    DataGridView2.ColumnHeadersHeight = 25;
                    DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView2.Width = 800;
                    DataGridView2.Refresh(); // Refresh the grid view
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
                LoadCheckoutData();
                DataGridView2.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsophong_TextChanged(object sender, EventArgs e)
        {
            //txtcccd_cus.Clear();
            //txtdate_co.Value = DateTime.Now;

            //string connectionString = DatabaseConnection.Connection();
            //string query = @"SELECT cccd_cus
            //         FROM Bookings 
            //         WHERE maphong = @maphong";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    using (SqlCommand command = new SqlCommand(query, connection))
            //    {
            //        // Correctly referencing the .Text property of the txtsophong PlaceholderTextBox
            //        if (int.TryParse(txtsophong.Text, out int roomNumber))
            //        {
            //            command.Parameters.AddWithValue("@maphong", roomNumber);
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    txtcccd_cus.Text = reader.GetString(0);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please enter a valid room number.");
            //        }
            //    }
            //}

            //if (!string.IsNullOrEmpty(txtcccd_cus.Text))
            //{
            //    txtcccd_cus.Text = txtcccd_cus.Text;
            //}
        }


        private void txtcccd_cus_TextChanged(object sender, EventArgs e)
        {
            //txtdate_co.Value = DateTime.Now;

            //string connectionString = DatabaseConnection.Connection();
            //string query = @"SELECT date_co
            //                 FROM Bookings 
            //                 WHERE roomnumber = @roomnumber AND cccd_cus = @cccd_cus";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    using (SqlCommand command = new SqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@roomnumber", txtsophong.ToString());
            //        command.Parameters.AddWithValue("@cccd_cus", txtcccd_cus.ToString());
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                txtdate_co.Value = reader.GetDateTime(0);
            //            }
            //        }
            //    }
            //}
            //if (txtdate_co.Value == DateTime.Now)
            //{
            //    txtdate_co.Value = txtdate_co.Value;
            //}
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string connectionString = DatabaseConnection.Connection();
                string query = @"SELECT first_name, last_name, maphong, date_ci
                         FROM Customer
                         INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
                         WHERE Bookings.cccd_cus = @cccd_cus";
                string firstName = string.Empty;
                string lastName = string.Empty;
                int maphong = 0; 
                DateTime dateCi = DateTime.MinValue;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cccd_cus", txtcccd_cus.Text);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                firstName = reader["first_name"].ToString();
                                lastName = reader["last_name"].ToString();
                                maphong = int.Parse(reader["maphong"].ToString());
                                dateCi = Convert.ToDateTime(reader["date_ci"]);
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng với CCCD này.");
                                return; 
                            }
                        }
                    }
                }
                Checkout checkout = new Checkout
                {
                    cccd_cus = txtcccd_cus.Text,      
                    first_name = firstName,           
                    last_name = lastName,             
                    maphong = maphong,               
                    sophong = int.Parse(txtsophong.Text), 
                    date_ci = dateCi,                 
                    date_co = txtdate_co.Value       
                };

                DAL dal = new DAL();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addcheckout(checkout, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }

    }
}
