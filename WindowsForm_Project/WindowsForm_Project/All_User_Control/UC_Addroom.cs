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
            this.Leave += new EventHandler(UC_Addroom_Leave);
            this.Enter += new EventHandler(UC_Addroom_Enter);
        }

        private void UC_Addroom_Load(object sender, EventArgs e)
        {
            this.DataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            this.DataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 12);


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
                    numbed = txtloaigiuong.SelectedItem.ToString(),
                    view_room = txtviewroom.SelectedItem.ToString(),
                    image_room = Image1.ImageLocation,
                    price = int.Parse(txtgia.Text)
                };

                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addroom(room, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
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
            LoadRoomUpData();
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
            Image1.Image = null;
        }

        private void UC_Addroom_Leave(object sender, EventArgs e)
        {
            
        }
        private void UC_Addroom_Enter(object sender, EventArgs e)
        {
            
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
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list;
                    DataGridView1.ColumnHeadersHeight = 25;
                    DataGridView1.Columns["maphong"].HeaderText = "ID Phòng";
                    DataGridView1.Columns["roomnumber"].HeaderText = "Mã Phòng";
                    DataGridView1.Columns["roomtype"].HeaderText = "Thể loại phòng";
                    DataGridView1.Columns["numbed"].HeaderText = "Số giường";
                    DataGridView1.Columns["view_room"].HeaderText = "Dạng phòng";
                    DataGridView1.Columns["image_room"].HeaderText = "Image room";
                    DataGridView1.Columns["price"].HeaderText = "Giá cả";
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }
        private void LoadRoomUpData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response2 = dal.Getupdateroom(conn);
                if ((response2.list6 != null && response2.list6.Count > 0))
                {
                    DataGridView2.DataSource = null; // Clear previous data
                    DataGridView2.DataSource = response2.list6;
                    DataGridView2.ColumnHeadersHeight = 25;
                    DataGridView2.Columns["maphong"].HeaderText = "ID Phòng";
                    DataGridView2.Columns["roomnumber"].HeaderText = "Mã Phòng";
                    DataGridView2.Columns["status_room"].HeaderText = "Trạng thái phòng";
                    DataGridView2.Columns["house_keeping"].HeaderText = "Tình trạng phòng";
                    DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    DataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    DataGridView2.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response2.statusmessage);
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
                RoomUpdate room = new RoomUpdate
                {
                    maphong = int.Parse(txtmaphongupdateroom.Text),
                    status_room = txtstatusroom.SelectedItem.ToString(),
                    house_keeping = txthousekeeping.SelectedItem.ToString()
                };
                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addupdateroom(room, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
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
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Deleteroom(room, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
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

        private void txtsophong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtstatusroom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UC_Addroom_Enter_1(object sender, EventArgs e)
        {
            try
            {
                LoadRoomData();
                LoadRoomUpData();
                DataGridView1.Refresh();
                DataGridView2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UC_Addroom_Leave_1(object sender, EventArgs e)
        {
            clearAll();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            String imagelocation = "";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imagelocation = openFileDialog.FileName;
                    Image1.ImageLocation = imagelocation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
