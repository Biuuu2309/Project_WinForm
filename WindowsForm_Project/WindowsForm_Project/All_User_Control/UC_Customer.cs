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
            //string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
            string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
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
            clearAll_Cus();
        }
        private void UC_Customer_Enter(object sender, EventArgs e)
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
                //string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
                string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
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
                //string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
                string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Updatecustomer(customer, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Update customer thanh cong"))
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
    }
}
