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
    public partial class UC_Addroom : UserControl
    {
        public UC_Addroom()
        {
            InitializeComponent();
        }

        private void UC_Addroom_Load(object sender, EventArgs e)
        {

        }

        private void txtmaphong_TextChanged(object sender, EventArgs e)
        {

        }
        private bool ValidateInput()
        {
            if (txtmaphong.Text == "" || txtsophong.Text == "" || txtloaiphong.SelectedItem == null || txtloaigiuong.SelectedItem == null || txtviewroom.SelectedItem == null || txtgia.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }
        private bool ValidateInput_update()
        {
            if (txtmaphongupdateroom.Text == "" || txtstatusroom.SelectedItem == null || txthousekeeping.SelectedItem == null)
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
            txtmaphongupdateroom.Clear();
            txtstatusroom.SelectedIndex = -1;
            txthousekeeping.SelectedIndex = -1;
            txtdeletemaphong.Clear();
        }

        private void UC_Addroom_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
        private void UC_Addroom_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadRoomData();
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
            //string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
            string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getroom(conn);
                Response response2 = dal.Getupdateroom(conn);
                if ((response.list != null && response.list.Count > 0) || (response2.list != null && response2.list.Count > 0))
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list;
                    DataGridView1.Refresh(); // Refresh the grid view
                    DataGridView2.DataSource = null; // Clear previous data
                    DataGridView2.DataSource = response2.list;
                    DataGridView2.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No room data available or " + response.statusmessage);
                }
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnaddupdate_Click(object sender, EventArgs e)
        {
            if (ValidateInput_update())
            {
                Room room = new Room
                {
                    maphong = int.Parse(txtmaphongupdateroom.Text),
                    status_room = txtstatusroom.SelectedItem.ToString(),
                    house_keeping = txthousekeeping.SelectedItem.ToString()
                };
                DAL dal = new DAL();
                //string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
                string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addupdateroom(room, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtdeletemaphong.Text != "")
            {
                Room room = new Room
                {
                    maphong = int.Parse(txtdeletemaphong.Text),
                };
                DAL dal = new DAL();
                //string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
                string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Deleteroom(room, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the fields.");
            }
        }
    }
}
