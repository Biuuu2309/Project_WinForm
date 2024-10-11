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
            this.Enter += new EventHandler(UC_Bookings_Enter);
        }
        
        private bool ValidateInput()
        {
            if (txtcccd_cus.Text == "" || txtstatusroom.SelectedItem == null || txthousekeeping.SelectedItem == null || txtloaiphong.SelectedItem == null || txtloaigiuong.SelectedItem == null || txtviewroom.SelectedItem == null || txtdateci.Value == null || txtdateco.Value == null || txtgroupcus.Text == null || txtprice.Text == null || txtmaphong.SelectedItem == null || txtsophong.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void RefreshControl()
        {
            clearAll();
            LoadRoomData();
            LoadCustomerData();
            LoadBookingData();
        }

        public void clearAll()
        {
            txtcccd_cus.Clear();
            txtstatusroom.SelectedItem = -1;
            txthousekeeping.SelectedItem = -1;
            txtloaiphong.SelectedIndex = -1;
            txtloaigiuong.SelectedIndex = -1;
            txtviewroom.SelectedIndex = -1;
            txtdateci.Value = DateTime.Now;
            txtdateco.Value = DateTime.Now;
            txtgroupcus.Clear();
            txtprice.Clear();
            txtsophong.SelectedItem = -1;
            txtmaphong.SelectedIndex = -1;
        }
        private void UC_Bookings_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
        private void UC_Bookings_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadRoomData();
                LoadCustomerData();
                LoadBookingData();
                DataGridView1.Refresh();
                DataGridView2.Refresh();
                DataGridView3.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoomData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getroombook(conn);
                if (response.list10 != null && response.list10.Count > 0)
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list10;
                    DataGridView1.ColumnHeadersHeight = 25;
                    DataGridView1.Columns["maphong"].HeaderText = "ID";
                    DataGridView1.Columns["roomnumber"].HeaderText = "Mã Phòng";
                    DataGridView1.Columns["roomtype"].HeaderText = "Loại phòng";
                    DataGridView1.Columns["numbed"].HeaderText = "So giuong";
                    DataGridView1.Columns["view_room"].HeaderText = "Dạng phòng";
                    DataGridView1.Columns["house_keeping"].HeaderText = "house keeping";
                    DataGridView1.Columns["status_room"].HeaderText = "status room";
                    DataGridView1.Columns["price"].HeaderText = "Giá cả";
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }
        /// -------------------------------------------------------------------
        /// btn addcustomer
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                // Ensure valid parsing of group_customer and price
                if (!int.TryParse(txtgroupcus.Text, out int groupCustomer))
                {
                    MessageBox.Show("Invalid value for Group Customer. Please enter a valid number.");
                    return;
                }

                if (!int.TryParse(txtprice.Text, out int price))
                {
                    MessageBox.Show("Invalid value for Price. Please enter a valid number.");
                    return;
                }

                if (!int.TryParse(txtmaphong.Text, out int maphong))
                {
                    MessageBox.Show("Invalid value for Maphong. Please enter a valid number.");
                    return;
                }

                if (!int.TryParse(txtsophong.Text, out int sophong))
                {
                    MessageBox.Show("Invalid value for Sophong. Please enter a valid number.");
                    return;
                }

                Bookings bookings = new Bookings
                {
                    cccd_cus = txtcccd_cus.Text,
                    status_room = txtstatusroom.SelectedItem.ToString(),
                    house_keeping = txthousekeeping.SelectedItem.ToString(),
                    roomtype = txtloaiphong.SelectedItem.ToString(),
                    numbed = int.Parse(txtloaigiuong.SelectedItem.ToString()),  // Assuming this is always valid
                    view_room = txtviewroom.SelectedItem.ToString(),
                    maphong = maphong,
                    roomnumber = sophong,
                    group_customer = groupCustomer,
                    date_ci = txtdateci.Value,
                    date_co = txtdateco.Value,
                    price = price,
                };
                RoomUpdate roomUpdate = new RoomUpdate
                {
                    roomnumber = int.Parse(txtsophong.SelectedItem.ToString())
                };
                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addbooking(bookings, conn);
                    Response response1 = dal.Updateroombooking2(roomUpdate, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }


        private void LoadCustomerData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getcustomerbook(conn);
                if (response.list1 != null && response.list1.Count > 0)
                {
                    DataGridView2.DataSource = null; // Clear previous data
                    DataGridView2.DataSource = response.list1;
                    DataGridView2.ColumnHeadersHeight = 25;
                    DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    DataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView2.Columns["cccd_cus"].HeaderText = "Mã CCCD";
                    DataGridView2.Columns["first_name"].HeaderText = "Tên đầu";
                    DataGridView2.Columns["last_name"].HeaderText = "Tên cuối";
                    DataGridView2.Columns["sdt"].HeaderText = "Số điện thoại";
                    DataGridView2.Columns["email"].HeaderText = "Email";
                    DataGridView2.Columns["gioitinh"].HeaderText = "Giới tính";
                    DataGridView2.Columns["ngaysinh"].HeaderText = "Ngày sinh";
                    DataGridView2.Columns["address_cus"].HeaderText = "Địa chỉ";
                    DataGridView2.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No customer data available or " + response.statusmessage);
                }
            }
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
                    DataGridView3.DataSource = null; // Clear previous data
                    DataGridView3.DataSource = response.list9;
                    DataGridView3.ColumnHeadersHeight = 25;
                    DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    DataGridView3.Columns["cccd_cus"].HeaderText = "CCCD";
                    DataGridView3.Columns["status_room"].HeaderText = "Trạng thái phòng";
                    DataGridView3.Columns["house_keeping"].HeaderText = "Trạng thái nhà";
                    DataGridView3.Columns["roomtype"].HeaderText = "Loại phòng";
                    DataGridView3.Columns["numbed"].HeaderText = "Số giường";
                    DataGridView3.Columns["date_ci"].HeaderText = "Ngày vào";
                    DataGridView3.Columns["date_co"].HeaderText = "Ngày ra";
                    DataGridView3.Columns["view_room"].HeaderText = "Dạng phòng";
                    DataGridView3.Columns["price"].HeaderText = "Giá cả";
                    DataGridView3.Columns["group_customer"].HeaderText = "Nhóm khách hàng";
                    DataGridView3.Columns["maphong"].HeaderText = "Mã phòng";
                    DataGridView3.Columns["roomnumber"].HeaderText = "Số phòng";
                    DataGridView3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView3.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }

        private void RefreshControl_Cus()
        {

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
            this.DataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGridView3.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            this.DataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            this.DataGridView3.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            this.DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


        }

        private void txtsophong_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
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

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox4_Click(object sender, EventArgs e)
        {

        }

        private void txtstatusroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txthousekeeping.Items.Clear();
            txtprice.Clear();

            string connectionString = DatabaseConnection.Connection();
            string query = @"SELECT house_keeping
                             FROM Update_room 
                             WHERE status_room = @status_room";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status_room", txtstatusroom.SelectedItem.ToString());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txthousekeeping.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            if (txthousekeeping.Items.Count > 0)
            {
                txthousekeeping.SelectedIndex = 0;
            }
        }
        
        private void txthousekeeping_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtloaiphong.Items.Clear();
            txtprice.Clear();

            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT roomtype
                                FROM Room 
                                WHERE maphong IN (  SELECT maphong 
                                				    FROM Update_room 
                                				    WHERE status_room = @status_room AND house_keeping = @house_keeping)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status_room", txtstatusroom.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@house_keeping", txthousekeeping.SelectedItem.ToString());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtloaiphong.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            if (txtloaiphong.Items.Count > 0)
            {
                txtloaiphong.SelectedIndex = 0;
            }
        }

        private void txtloaiphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtloaigiuong.Items.Clear();
            txtprice.Clear();

            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT numbed
                                FROM Room 
                                WHERE roomtype = @roomtype AND maphong IN ( SELECT maphong 
                                										    FROM Update_room 
                                										    WHERE status_room = @status_room AND house_keeping = @house_keeping)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status_room", txtstatusroom.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@house_keeping", txthousekeeping.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@roomtype", txtloaiphong.SelectedItem.ToString());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtloaigiuong.Items.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            if (txtloaigiuong.Items.Count > 0)
            {
                txtloaigiuong.SelectedIndex = 0;
            }
        }

        private void txtloaigiuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtviewroom.Items.Clear();
            txtprice.Clear();

            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT view_room
                                FROM Room 
                                WHERE roomtype = @roomtype AND numbed = @numbed AND maphong IN (SELECT maphong 
                                												                FROM Update_room 
                                												                WHERE status_room = @status_room AND house_keeping = @house_keeping)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status_room", txtstatusroom.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@house_keeping", txthousekeeping.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@roomtype", txtloaiphong.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@numbed", int.Parse(txtloaigiuong.SelectedItem.ToString()));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtviewroom.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            if (txtviewroom.Items.Count > 0)
            {
                txtviewroom.SelectedIndex = 0;
            }
        }

        private void txtdateco_ValueChanged(object sender, EventArgs e)
        {
            // Clear previous price
            txtprice.Clear();

            // Ensure necessary ComboBox selections are not null
            if (txtstatusroom.SelectedItem == null || txthousekeeping.SelectedItem == null ||
                txtloaiphong.SelectedItem == null || txtloaigiuong.SelectedItem == null ||
                txtviewroom.SelectedItem == null)
            {
                MessageBox.Show("Please select all necessary fields before calculating the price.");
                return;
            }

            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT Room.price * DATEDIFF(DAY, @date_ci, @date_co) AS price
                        FROM Room 
                        WHERE roomtype = @roomtype AND numbed = @numbed AND view_room = @view_room 
                        AND maphong IN (SELECT maphong FROM Update_room WHERE status_room = @status_room AND house_keeping = @house_keeping)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Safely add parameters if selections exist
                    command.Parameters.AddWithValue("@status_room", txtstatusroom.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@house_keeping", txthousekeeping.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@roomtype", txtloaiphong.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@numbed", int.Parse(txtloaigiuong.SelectedItem.ToString()));
                    command.Parameters.AddWithValue("@view_room", txtviewroom.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@date_ci", txtdateci.Value);
                    command.Parameters.AddWithValue("@date_co", txtdateco.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtprice.Text = reader.GetInt32(0).ToString();
                        }
                    }
                }
            }

            // Ensure that price is updated correctly
            if (!string.IsNullOrEmpty(txtprice.Text))
            {
                txtprice.Text = txtprice.Text;
            }
        }


        private void txtdateci_ValueChanged(object sender, EventArgs e)
        {
            txtprice.Clear();
        }

        private void txtviewroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtprice.Clear();
            txtmaphong.Items.Clear();
            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT maphong
                                FROM Room 
                                WHERE roomtype = @roomtype AND numbed = @numbed AND view_room = @view_room AND maphong IN ( SELECT maphong 
                                												                                            FROM Update_room 
                                												                                            WHERE status_room = @status_room AND house_keeping = @house_keeping)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status_room", txtstatusroom.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@house_keeping", txthousekeeping.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@roomtype", txtloaiphong.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@numbed", int.Parse(txtloaigiuong.SelectedItem.ToString()));
                    command.Parameters.AddWithValue("@view_room", txtviewroom.SelectedItem.ToString());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtmaphong.Items.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            if (txtmaphong.Items.Count > 0)
            {
                txtmaphong.SelectedIndex = 0;
            }
        }

        private void txtsophong_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtmaphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtprice.Clear();
            txtsophong.Items.Clear();
            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT roomnumber
                                FROM Room 
                                WHERE maphong = @maphong";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maphong", int.Parse(txtmaphong.SelectedItem.ToString()));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtsophong.Items.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            if (txtsophong.Items.Count > 0)
            {
                txtsophong.SelectedIndex = 0;
            }
        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }
    }
}
