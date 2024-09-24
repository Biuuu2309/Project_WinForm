using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForm_Project.Models;

namespace WindowsForm_Project.All_User_Control
{
    public partial class UC_Manage : UserControl
    {
        public UC_Manage()
        {
            InitializeComponent();
            this.Enter += new EventHandler(UC_Manage_Enter); // Ensure this is set to load data
            // Remove the Leave event if not needed
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                ManageEmployee employee = new ManageEmployee
                {
                    cccd_em = txtcccd_em.Text,
                    first_name = txtfirstname.Text,
                    last_name = txtlastname.Text,
                    sdt = txtsdt.Text,
                    email = txtemail.Text,
                    gioitinh = txtgioitinh.SelectedItem.ToString(),
                    ngaysinh = txtngaysinh.Value,
                    luong = float.Parse(txtluong.Text),
                };

                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addemployee(employee, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl_Em();
                    }
                }
            }
        }
        private void RefreshControl_Em()
        {
            clearAll_Em();
            LoadEmployeeData();
            LoadChamCong();
            LoadEmployeeTotal();
        }
        private void LoadEmployeeData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getemployee(conn);
                if (response.list2 != null && response.list2.Count > 0)
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list2; // Set the data source
                    DataGridView1.ColumnHeadersHeight = 25;
                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }
        public void clearAll_Em()
        {
            txtcccd_em.Clear();
            txtfirstname.Clear();
            txtlastname.Clear();
            txtsdt.Clear();
            txtemail.Clear();
            txtgioitinh.SelectedIndex = -1;
            txtngaysinh.Value = DateTime.Now;
            txtluong.Clear();
            txtcccdcc.Clear();
            txtngaycc.Value = DateTime.Now;
            txtca1cc.SelectedIndex = -1;
            txtca2cc.SelectedIndex = -1;
            txtca3cc.SelectedIndex = -1;
            txtca4cc.SelectedIndex = -1;
            txtnotecc.Clear();
            txtcccdup.Clear();
            txtfirstnameup.Clear();
            txtlastnameup.Clear();
            txtsdtup.Clear();
            txtemailup.Clear();
            txtgioitinhup.SelectedIndex = -1;
            txtngaysinhup.Value = DateTime.Now;
            txtluongup.Clear();
        }
        private bool ValidateInput()
        {
            if (txtcccd_em.Text == "" || txtfirstname.Text == "" || txtlastname.Text == "" || txtsdt.Text == "" || txtemail.Text == "" || txtgioitinh.SelectedItem == null || txtngaysinh.Value == null || txtluong.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }
        private void UC_Manage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadEmployeeData(); // Load employee data when entering the control
                LoadChamCong(); // Load attendance data
                LoadEmployeeTotal(); // Load total employee data
                DataGridView1.Refresh(); // Refresh the grid view
                DataGridView2.Refresh(); // Refresh the grid view
                DataGridView3.Refresh(); // Refresh the grid view
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadChamCong()
        {
            DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getchamcong(conn);
                if (response.list4 != null && response.list4.Count > 0)
                {
                    DataGridView3.DataSource = null; // Clear previous data
                    DataGridView3.DataSource = response.list4;
                    DataGridView3.ColumnHeadersHeight = 25;
                    DataGridView3.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadEmployeeTotal()
        {
            DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Gettotal(conn);
                if (response.list5 != null && response.list5.Count > 0)
                {
                    DataGridView2.DataSource = null; // Clear previous data
                    DataGridView2.DataSource = response.list5;
                    DataGridView2.ColumnHeadersHeight = 25;
                    DataGridView2.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (ValidateInput_ChamCong())
            {
                EmployeeWork employee = new EmployeeWork
                {
                    cccd_em = txtcccdcc.Text,
                    ngay = txtngaycc.Value,
                    ca1 = txtca1cc.SelectedItem.ToString(),
                    ca2 = txtca2cc.SelectedItem.ToString(),
                    ca3 = txtca3cc.SelectedItem.ToString(),
                    ca4 = txtca4cc.SelectedItem.ToString(),
                    note = txtnotecc.Text,
                };

                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addemployeework(employee, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl_Em();
                    }
                }
            }
        }
        private bool ValidateInput_ChamCong()
        {
            if (txtcccdcc.Text == "" || txtngaycc.Value == null || txtca1cc.SelectedItem == null || txtca2cc.SelectedItem == null || txtca3cc.SelectedItem == null || txtca4cc.SelectedItem == null || txtnotecc.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (ValidateInput_Update())
            {
                ManageEmployee employee = new ManageEmployee
                {
                    cccd_em = txtcccdup.Text,
                    first_name = txtfirstnameup.Text,
                    last_name = txtlastnameup.Text,
                    sdt = txtsdtup.Text,
                    email = txtemailup.Text,
                    gioitinh = txtgioitinhup.SelectedItem.ToString(),
                    ngaysinh = txtngaysinhup.Value,
                    luong = float.Parse(txtluongup.Text),
                };

                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Updateemployee(employee, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl_Em();
                    }
                }
            }
        }
        private bool ValidateInput_Update()
        {
            if (txtcccdup.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
