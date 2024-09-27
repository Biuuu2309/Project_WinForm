using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
            this.DataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGridView3.DefaultCellStyle.ForeColor = Color.Black;

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
                };

                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Updateserve(serve, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }
        private bool ValidateInput()
        {
            if (txtcccd.Text == "" || txtmaphong.Text == "" || txtotherbooking.Text == "" || txtanuong.Text == "" || txtcallserve.Text == null)
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
        }
        private void UC_Serve_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
        private void UC_Serve_Enter(object sender, EventArgs e)
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
                Response response = dal.Getcustomer(conn);
                if (response.list1 != null && response.list1.Count > 0)
                {
                    DataGridView3.DataSource = null; // Clear previous data
                    DataGridView3.DataSource = response.list1;
                    DataGridView3.ColumnHeadersHeight = 25;
                    DataGridView3.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No customer data available or " + response.statusmessage);
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
                btnsubmit2.Text = "Account";
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
    }
}
