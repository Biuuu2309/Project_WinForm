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
    public partial class UC_Employee : UserControl
    {
        public UC_Employee()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Employee employee = new Employee
                {
                    cccd_em = txtcccd_em.Text,
                    first_name = txtfirstname.Text,
                    last_name = txtlastname.Text,
                    sdt = txtsdt.Text,
                    email = txtemail.Text,
                    gioitinh = txtgioitinh.SelectedItem.ToString(),
                    ngaysinh = txtngaysinh.Value,
                    luong = txtluong.Text
                };

                DAL dal = new DAL();
                string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
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
        }
        private void LoadEmployeeData()
        {
            DAL dal = new DAL();
            string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getemployee(conn);
                if (response.list2 != null && response.list2.Count > 0)
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list2;
                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No customer data available or " + response.statusmessage);
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
        private void UC_Bookings_Leave(object sender, EventArgs e)
        {
            clearAll_Em();
        }
        private void UC_Bookings_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadEmployeeData();
                DataGridView1.Refresh();
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
    }
}
