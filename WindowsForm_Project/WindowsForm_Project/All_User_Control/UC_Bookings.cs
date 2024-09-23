using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForm_Project.Models;
using System.Data.SqlClient;

namespace WindowsForm_Project.All_User_Control
{
    public partial class UC_Bookings : UserControl
    {
        public UC_Bookings()
        {
            InitializeComponent();
        }

        private bool ValidateInput()
        {
            if(txtmaphong.Text == "" || txtsophong.Text == "" || txtloaiphong.SelectedItem == null || txtloaigiuong.SelectedItem == null || txtviewroom.SelectedItem == null || txtgia.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Room room = new Room
                {
                    maphong = int.Parse(txtmaphong.Text),
                    roomnumber = int.Parse(txtsophong.Text),
                    roomtype = txtloaiphong.SelectedItem.ToString(),
                    numbed = int.Parse(txtloaigiuong.SelectedItem.ToString()),
                    view_room = txtviewroom.SelectedItem.ToString(),
                    price = int.Parse(txtgia.Text)
                };

                DAL dal = new DAL();
                string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
                //string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addroom(room, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }

        private void RefreshControl()
        {
            clearAll();
            LoadRoomData();
        }

        public void clearAll()
        {
            txtmaphong.Clear();
            txtsophong.Clear();
            txtloaiphong.SelectedIndex = -1;
            txtloaigiuong.SelectedIndex = -1;
            txtviewroom.SelectedIndex = -1;
            txtgia.Clear();
        }
        private void UC_Bookings_Leave(object sender, EventArgs e)
        {
            clearAll();
            clearAll_Cus();
        }
        private void UC_Bookings_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadRoomData();
                LoadCustomerData();
                DataGridView1.Refresh();
                DataGridView2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoomData()
        {
            DAL dal = new DAL();
            string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
            //string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getroom(conn);
                if (response.list != null && response.list.Count > 0)
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list;
                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No room data available or " + response.statusmessage);
                }
            }
        }
        /// -------------------------------------------------------------------
        /// btn addcustomer
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (ValidateInput_Cus())
            {
                Customer customer = new Customer
                {
                    cccd_cus = txtcccd_cus.Text,
                    first_name = txtfirstname_cus.Text,
                    last_name = txtlastname_cus.Text,
                    sdt = txtsdt_cus.Text,
                    email = txtemail_cus.Text,
                    gioitinh = txtgioitinh_cus.SelectedItem.ToString(),
                    ngaysinh = txtngaysinh_cus.Value,
                    address_cus = txtaddress.Text,
                };

                DAL dal = new DAL();
                string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
                //string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addcustomer(customer, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Them customer thanh cong"))
                    {
                        RefreshControl_Cus();
                    }
                }
            }
        }

        private bool ValidateInput_Cus()
        {
            if (txtcccd_cus.Text == "" || txtfirstname_cus.Text == "" || txtlastname_cus.Text == "" || txtsdt_cus.Text == "" || txtemail_cus.Text == "" || txtgioitinh_cus.SelectedItem == null || txtngaysinh_cus.Value == null || txtaddress.Text == null)
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }

        private void LoadCustomerData()
        {
            DAL dal = new DAL();
            string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
            //string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getcustomer(conn);
                if (response.list1 != null && response.list1.Count > 0)
                {
                    DataGridView2.DataSource = null; // Clear previous data
                    DataGridView2.DataSource = response.list1;
                    DataGridView2.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No customer data available or " + response.statusmessage);
                }
            }
        }

        private void RefreshControl_Cus()
        {
            clearAll_Cus();
            LoadCustomerData();
        }

        public void clearAll_Cus()
        {
            txtcccd_cus.Clear();
            txtfirstname_cus.Clear();
            txtlastname_cus.Clear();
            txtsdt_cus.Clear();
            txtemail_cus.Clear();
            txtgioitinh_cus.SelectedIndex = -1;
            txtngaysinh_cus.Value = DateTime.Now;
            txtaddress.Clear();
        }
        /// -------------------------------------------------------------------
        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }
        private void UC_Bookings_Load(object sender, EventArgs e)
        {

        }

        private void txtsophong_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void txtloaigiuong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtcccd_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtlastname_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsdt_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
