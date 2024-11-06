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
    public partial class UC_Customer : UserControl
    {
        public UC_Customer()
        {
            InitializeComponent();
            this.Leave += new EventHandler(UC_Customer_Leave);
            this.Enter += new EventHandler(UC_Customer_Enter);
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            


        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
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
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getcustomer(conn);
                if (response.list1 != null && response.list1.Count > 0)
                {
                    DataGridView2.DataSource = null; // Clear previous data
                    DataGridView2.DataSource = response.list1;
                    DataGridView2.ColumnHeadersHeight = 25;
                    DataGridView2.Columns["cccd_cus"].HeaderText = "Mã CCCD";
                    DataGridView2.Columns["first_name"].HeaderText = "Tên đầu";
                    DataGridView2.Columns["last_name"].HeaderText = "Tên cuối";
                    DataGridView2.Columns["sdt"].HeaderText = "Số điện thoại";
                    DataGridView2.Columns["email"].HeaderText = "Email";
                    DataGridView2.Columns["gioitinh"].HeaderText = "Giới tính";
                    DataGridView2.Columns["ngaysinh"].HeaderText = "Ngày sinh";
                    DataGridView2.Columns["address_cus"].HeaderText = "Địa chỉ";
                    DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    DataGridView2.DefaultCellStyle.ForeColor = Color.Black;
                    DataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 12);
                    DataGridView2.Columns["email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


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

            txtcccd_cusup.Clear();
            txtfirst_nameup.Clear();
            txtlast_nameup.Clear();
            txtsdt_cusup.Clear();
            txtemail_cusup.Clear();
            txtgioitinh_cusup.SelectedIndex = -1;
            txtngaysinh_cusup.Value = DateTime.Now;
            txtaddressup.Clear();
        }
        private void UC_Customer_Leave(object sender, EventArgs e)
        {
            
        }
        private void UC_Customer_Enter(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
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
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addcustomer(customer, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
                    {
                        RefreshControl_Cus();
                    }
                }
            }
        }
        private bool ValidateInput_Cus_Up()
        {
            if (txtcccd_cusup.Text == "")
            {
                MessageBox.Show("Please fill in the CCCD fields.");
                return false;
            }
            return true;
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (ValidateInput_Cus_Up())
            {
                Customer customer = new Customer
                {
                    cccd_cus = txtcccd_cusup.Text,
                    first_name = txtfirst_nameup.Text,
                    last_name = txtlast_nameup.Text,
                    sdt = txtsdt_cusup.Text,
                    email = txtemail_cusup.Text,
                    gioitinh = txtgioitinh_cusup.SelectedItem.ToString(),
                    ngaysinh = txtngaysinh_cusup.Value,
                    address_cus = txtaddressup.Text,
                };

                DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Updatecustomer(customer, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
                    {
                        RefreshControl_Cus();
                    }
                }
            }
        }
        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void txtemail_cusup_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtemail_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel17_Click(object sender, EventArgs e)
        {

        }

        private void txtcccd_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtfirstname_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtaddressup_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void txtcccd_cusup_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtaddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void txtngaysinh_cus_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel15_Click(object sender, EventArgs e)
        {

        }

        private void txtgioitinh_cus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel14_Click(object sender, EventArgs e)
        {

        }

        private void txtsdt_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel12_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel13_Click(object sender, EventArgs e)
        {

        }

        private void txtlastname_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtngaysinh_cusup_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txtgioitinh_cusup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtsdt_cusup_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void txtlast_nameup_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void txtfirst_nameup_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel16_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void UC_Customer_Enter_1(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerData();
                DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                DataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                DataGridView2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UC_Customer_Leave_1(object sender, EventArgs e)
        {
            clearAll_Cus();
        }

        private void guna2HtmlLabel1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
