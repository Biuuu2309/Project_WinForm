using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForm_Project.Models;

namespace WindowsForm_Project.All_User_Control
{
    public partial class UC_Serve : UserControl
    {
        public UC_Serve()
        {
            InitializeComponent();
            this.Leave += new EventHandler(UC_Serve_Leave);
            this.Enter += new EventHandler(UC_Serve_Enter);
        }

        private void UC_Serve_Load(object sender, EventArgs e)
        {
            this.DataGridView1.DefaultCellStyle.ForeColor=Color.Black;
            this.DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            this.DataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            this.DataGridView3.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGridView3.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            this.DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;



        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Serve serve = new Serve
                {
                    cccd_cus = txtcccd.Text,
                    maphong = int.Parse(txtmaphong.Text),
                    other_booking = txtotherbooking.Text,
                    anuong = txtanuong.Text,
                    call_serve = txtcallserveee.Checked,
                    cost = int.Parse(txtcost.Text),
                };

                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Updateserve(serve, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }
        private bool ValidateInput()
        {
            if (txtcccd.Text == "" || txtmaphong.Text == "" || txtotherbooking.Text == "" || txtanuong.Text == "" || txtcallserveee.Text == null || txtcost.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }
        private void RefreshControl()
        {
            clearAll();
            LoadServeData();
            LoadRoomData();
            LoadCustomerData();
        }
        public void clearAll()
        {
            txtcccd.Clear();
            txtmaphong.Clear();
            txtotherbooking.Clear();
            txtanuong.Clear();
            txtcallserveee.Checked = false;
            txtcost.Clear();

        }
        private void UC_Serve_Leave(object sender, EventArgs e)
        {
            
        }
        private void UC_Serve_Enter(object sender, EventArgs e)
        {
            
        }
        private void LoadServeData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getserve(conn);
                if ((response.list7 != null && response.list7.Count > 0))
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list7;
                    DataGridView1.ColumnHeadersHeight = 25;
                    DataGridView1.Columns["stt"].HeaderText = "STT";
                    DataGridView1.Columns["cccd_cus"].HeaderText = "Mã CCCD";
                    DataGridView1.Columns["maphong"].HeaderText = "Mã Phòng";
                    DataGridView1.Columns["other_booking"].HeaderText = "Đặt dịch vụ khác";
                    DataGridView1.Columns["anuong"].HeaderText = "Ăn uống";
                    DataGridView1.Columns["call_serve"].HeaderText = "Gọi dịch vụ";
                    DataGridView1.Columns["cost"].HeaderText = "Phí dich vu";
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView1.Columns["call_serve"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DataGridView1.Columns[DataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }
        private void LoadRoomData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getroom(conn);
                if ((response.list != null && response.list.Count > 0))
                {
                    DataGridView2.DataSource = null; // Clear previous data
                    DataGridView2.DataSource = response.list;
                    DataGridView2.ColumnHeadersHeight = 25;
                    DataGridView2.Columns["maphong"].HeaderText = "ID Phòng";
                    DataGridView2.Columns["roomnumber"].HeaderText = "Mã Phòng";
                    DataGridView2.Columns["roomtype"].HeaderText = "Loại phòng";
                    DataGridView2.Columns["numbed"].HeaderText = "Số phòng đặt";
                    DataGridView2.Columns["view_room"].HeaderText = "Dạng phòng";
                    DataGridView2.Columns["image_room"].HeaderText = "Anh phong";
                    DataGridView2.Columns["price"].HeaderText = "Giá cả";
                    DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView2.Columns[DataGridView2.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    DataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView2.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }
        private void LoadCustomerData()
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
                    DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    DataGridView3.Columns["stt"].HeaderText = "STT";
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
                    DataGridView3.Columns["cccd_em"].HeaderText = "CCCD Employee";
                    DataGridView3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView3.Refresh(); // Refresh the grid view
                }
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
          uC_Report1.Visible = !uC_Report1.Visible;
            if (uC_Report1.Visible)
            {
                btnsubmit2.Image = Properties.Resources.logout;
                btnsubmit2.Text = "";
            }
            else
            {
                btnsubmit2.Text = "Ghi chu";
                btnsubmit2.Image = null;
            }
            uC_Report1.BringToFront();
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtcccd_TextChanged(object sender, EventArgs e)
        {
            string cccd = txtcccd.Text;
            string maPhong=LayMaPhongTuCCCD(cccd);
            if (!string.IsNullOrEmpty(maPhong))
            {
                txtmaphong.Text = maPhong;
            }
            else
            {
                txtmaphong.Clear();
            }

        }
        private string LayMaPhongTuCCCD(string cccd)
        {
            string maphong = null;
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try 
                {
                    conn.Open();
                    string query = "SELECT maphong FROM Bookings WHERE cccd_cus = @cccd";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cccd", cccd);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            maphong = reader["maphong"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("lỗi khi truy vân cơ sở dữ liệu: " + ex.Message); 
                }
            }
            return maphong;
        }
        private void txtcallserve_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtmaphong_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txtotherbooking_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void txtanuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void uC_Report1_Load(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox4_Click(object sender, EventArgs e)
        {

        }

        private void UC_Serve_Enter_1(object sender, EventArgs e)
        {
            try
            {
                LoadServeData();
                DataGridView1.Refresh();
                LoadRoomData();
                DataGridView2.Refresh();
                LoadCustomerData();
                DataGridView3.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        private void UC_Serve_Leave_1(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
