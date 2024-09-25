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
                    call_serve = bool.Parse(txtcallserve.Text),
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
    }
}
