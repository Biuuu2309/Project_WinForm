using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForm_Project.All_User_Control;

namespace WindowsForm_Project
{
    public partial class Dashboard : Form
    {
        public bool IsReservedChecked
        {
            get { return cbreserved.Checked; }
        }
        public bool IsOccupiedChecked
        {
            get { return cboccupied.Checked; }
        }
        public bool IsAvailableChecked
        {
            get { return cbavailable.Checked; }
        }
        public bool IsCheckOutChecked
        {
            get { return cbcheckout.Checked; }
        }
        public bool IsCleanChecked
        {
            get { return cbclean.Checked; }
        }
        public bool IsNotCleanChecked
        {
            get { return cbnotclean.Checked; }
        }
        public bool IsInProgressChecked
        {
            get { return cbinprogress.Checked; }
        }
        public bool IsRepairChecked
        {
            get { return cbrepair.Checked; }
        }
        private DateTime currentDate;
        private WindowsForm_Project.All_User_Control.UC_Dashboard uc_dashboard;
        private WindowsForm_Project.All_User_Control.UC_Bookings uc_bookings;
        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel14_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel17_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel19_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel20_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {

        }

        private void btnCustomerDetail_Click(object sender, EventArgs e)
        {
            ///MovePanel(btncustomerdetail);
            // Show Customer Detail user control
            // Hide other user controls
        }

        private void Panelmoving_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        

        

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel13_Click(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void Clean_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel12_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel14_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click_2(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public Dashboard()
        {
            InitializeComponent();
            currentDate = DateTime.Now; // Initialize with the current date
            uc_dashboard = new WindowsForm_Project.All_User_Control.UC_Dashboard();
            uc_dashboard.Location = new Point(220, 114);
            Controls.Add(uc_dashboard);
            //this.Load += new EventHandler(uC_Dashboard2_Load_1);
            this.Enter += new EventHandler(cbreserved_Enter);
            this.MouseEnter += new EventHandler(guna2Panel3_MouseEnter);
            this.cbreserved.CheckedChanged += new System.EventHandler(this.guna2CheckBox1_CheckedChanged);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            UpdateDateLabel();
        }

        private void UpdateDateLabel()
        {
            // Create and configure the label
            Label dateLabel = new Label();
            dateLabel.Text = currentDate.ToString("dd MMMM yyyy"); // Format: day month year
            dateLabel.AutoSize = true;
            dateLabel.Font = new Font("Arial", 20, FontStyle.Bold); // Adjust font as needed
            dateLabel.BackColor = Color.Transparent;
            dateLabel.ForeColor = ColorTranslator.FromHtml("#f9abfa");

            // Center the label in the panel
            dateLabel.Location = new Point(
                (dashboardpaneldate.Width - dateLabel.Width) / 2,
                (dashboardpaneldate.Height - dateLabel.Height) / 2
            );
            Guna2ImageButton imageButton1 = new Guna2ImageButton();
            Guna2ImageButton imageButton2 = new Guna2ImageButton();
            imageButton1.Image = Properties.Resources.left__3_;
            imageButton1.Size = new Size(35, 45);
            imageButton1.ImageSize = new Size(64, 64);
            imageButton1.Location = new Point(37, 6);
            imageButton1.BackColor = Color.Transparent;
            imageButton2.Image = Properties.Resources.right__3_;
            imageButton2.Size = new Size(35, 45);
            imageButton2.ImageSize = new Size(64, 64);
            imageButton2.Location = new Point(1844, 6);
            imageButton2.BackColor = Color.Transparent;
            imageButton1.BringToFront();
            imageButton2.BringToFront();
            imageButton1.Visible = true;
            imageButton2.Visible = true;
            imageButton1.Click += (sender, e) =>
            {
                currentDate = currentDate.AddDays(-1); // Subtract one day
                UpdateDateLabel();
            };
            imageButton2.Click += (sender, e) =>
            {
                currentDate = currentDate.AddDays(1); // Add one day
                UpdateDateLabel();
            };
            // Clear previous controls and add the new label to the panel
            dashboardpaneldate.Controls.Clear();
            dashboardpaneldate.Controls.Add(dateLabel);
            dashboardpaneldate.Controls.Add(imageButton1);
            dashboardpaneldate.Controls.Add(imageButton2);
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            LoginFormNew loginFormNew = new LoginFormNew();
            this.Hide();
            loginFormNew.Show();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            //btnnext.BringToFront();
            //btnnext.Visible = true;
            //currentDate = currentDate.AddDays(1); // Subtract one day
            //UpdateDateLabel();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            //btnback.BringToFront();
            //btnback.Visible = true;
            //currentDate = currentDate.AddDays(-1); // Add one day
            //UpdateDateLabel();
        }

        private void btnaddroom_Click(object sender, EventArgs e)
        {
            MovePanel(btnaddroom);
            uC_Addroom1.Visible = true;
            uC_Addroom1.BringToFront();
            // Hide other user controls
            uC_Bookings2.Visible = false;
            uC_Dashboard2.Visible = false;
            uC_Customer1.Visible = false;
            uC_Manage1.Visible = false;
            uC_Serve1.Visible = false;
            uC_Checkout1.Visible = false;

        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            MovePanel(btndashboard);
            uC_Dashboard2.Visible = true;
            uC_Dashboard2.BringToFront();
            // Hide other user controls
            uC_Bookings2.Visible = false;
            uC_Addroom1.Visible = false;
            uC_Customer1.Visible = false;
            uC_Manage1.Visible = false;
            uC_Serve1.Visible = false;
            uC_Checkout1.Visible = false;

            // ... hide other user controls
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {
            MovePanel(btnbookings);
            uC_Bookings2.Visible = true;
            uC_Bookings2.BringToFront();
            // Hide other user controls
            uC_Dashboard2.Visible = false;
            uC_Addroom1.Visible = false;
            uC_Customer1.Visible = false;
            uC_Manage1.Visible = false;
            uC_Serve1.Visible = false;
            uC_Checkout1.Visible = false;


            // ... hide other user controls
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            MovePanel(btncheckout);
            // Show Check Out user control
            // Hide other user controls
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            MovePanel(btnmanage);
            // Show Employee user control
            // Hide other user controls
        }

        private void MovePanel(Guna.UI2.WinForms.Guna2GradientButton clickedButton)
        {
            Panelmoving.Left = clickedButton.Left;
            Panelmoving.Width = clickedButton.Width;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            //Panelmoving.Left = btndashboard.Left + 223;
            //uC_Dashboard2.Visible = false;
            //uC_Bookings2.Visible = true;
            //guna2Panel3.Visible = true;
            //uC_Bookings2.BringToFront();


            MovePanel(btnbookings);
            uC_Bookings2.Visible = true;
            uC_Bookings2.BringToFront();
            // Hide other user controls
            uC_Dashboard2.Visible = false;
            uC_Addroom1.Visible = false;
            uC_Customer1.Visible = false;
            uC_Manage1.Visible = false;
            uC_Serve1.Visible = false;
            uC_Checkout1.Visible = false;

        }

        private void uC_Dashboard1_Load(object sender, EventArgs e)
        {
            uC_Dashboard2.Visible = false;
            btndashboard.PerformClick();
            Panelmoving.Left = btndashboard.Left;
        }

        private void uC_Bookings1_Load(object sender, EventArgs e)
        {
            uC_Dashboard2.Visible = false;
            btndashboard.PerformClick();
            Panelmoving.Left = btndashboard.Left;
        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            MovePanel(btncustomer);
            uC_Customer1.Visible = true;
            uC_Customer1.BringToFront();
            // Hide other user controls
            uC_Dashboard2.Visible = false;
            uC_Addroom1.Visible = false;
            uC_Bookings2.Visible = false;
            uC_Manage1.Visible = false;
            uC_Serve1.Visible = false;
            uC_Checkout1.Visible = false;

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            MovePanel(btnserve);
            uC_Serve1.Visible = true;
            uC_Serve1.BringToFront();
            // Hide other user controls
            uC_Dashboard2.Visible = false;
            uC_Addroom1.Visible = false;
            uC_Bookings2.Visible = false;
            uC_Manage1.Visible = false;
            uC_Customer1.Visible = false;
            uC_Checkout1.Visible = false;

        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            //MovePanel(btnreport);
        }
        int count = 0;
        private void btnemployee_Click_1(object sender, EventArgs e)
        {
            LoginFormNew loginFormNew = new LoginFormNew();
            if (count == 0)
            {
                DialogResult result = MessageBox.Show("Bạn có muốn tiếp tục dang nhap duoi quyen quan ly khong ?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    if (loginFormNew.ShowDialog() == DialogResult.OK)
                    {
                        if (loginFormNew.Username == "admin" && loginFormNew.Password == "admin")
                        {
                            MovePanel(btnmanage);
                            uC_Manage1.Visible = true;
                            uC_Manage1.BringToFront();
                            uC_Dashboard2.Visible = false;
                            uC_Addroom1.Visible = false;
                            uC_Bookings2.Visible = false;
                            uC_Customer1.Visible = false;
                            uC_Serve1.Visible = false;
                            uC_Checkout1.Visible = false;
                            count++;
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        MessageBox.Show("Đã hủy bỏ thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Tiep tuc dang nhap duoi quyen la nhan vien");
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                }
            }
            else if (count != 0)
            {
                MovePanel(btnmanage);
                uC_Manage1.Visible = true;
                uC_Manage1.BringToFront();
                uC_Dashboard2.Visible = false;
                uC_Addroom1.Visible = false;
                uC_Bookings2.Visible = false;
                uC_Customer1.Visible = false;
                uC_Serve1.Visible = false;
                uC_Checkout1.Visible = false;
            }
        }


        private void btncheckout_Click_1(object sender, EventArgs e)
        {
            MovePanel(btncheckout);
            uC_Checkout1.Visible = true;
            uC_Checkout1.BringToFront();
            // Hide other user controls
            uC_Dashboard2.Visible = false;
            uC_Addroom1.Visible = false;
            uC_Bookings2.Visible = false;
            uC_Customer1.Visible = false;
            uC_Serve1.Visible = false;
            uC_Manage1.Visible = false;
        }

        private void uC_Manage1_Load(object sender, EventArgs e)
        {

        }

        private void uC_Customer1_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click_2(object sender, EventArgs e)
        {
            //MovePanel(btnmanage);
            //uC_Manage1.Visible = true;
            //uC_Manage1.BringToFront();
            //// Hide other user controls
            //uC_Dashboard2.Visible = false;
            //uC_Addroom1.Visible = false;
            //uC_Bookings2.Visible = false;
            //uC_Customer1.Visible = false;
            //uC_Serve1.Visible = false;

        }

        private void uC_Dashboard2_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbreserved.Checked && cbclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Reserved", "Clean");
                uc_dashboard.LoadDoubleRoom("Reserved", "Clean");
                uc_dashboard.LoadTripleRoom("Reserved", "Clean");
            }
            else if (cbreserved.Checked && cbnotclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Reserved", "Not Clean");
                uc_dashboard.LoadDoubleRoom("Reserved", "Not Clean");
                uc_dashboard.LoadTripleRoom("Reserved", "Not Clean");
            }
            else if (cbreserved.Checked && cbinprogress.Checked)
            {
                uc_dashboard.LoadSingleRoom("Reserved", "In Progress");
                uc_dashboard.LoadDoubleRoom("Reserved", "In Progress");
                uc_dashboard.LoadTripleRoom("Reserved", "In Progress");
            }
            else if (cbreserved.Checked && cbrepair.Checked)
            {
                uc_dashboard.LoadSingleRoom("Reserved", "Repair");
                uc_dashboard.LoadDoubleRoom("Reserved", "Repair");
                uc_dashboard.LoadTripleRoom("Reserved", "Repair");
            }
            else if (cbreserved.Checked)
            {
                uc_dashboard.LoadSingleRoom("Reserved");
                uc_dashboard.LoadDoubleRoom("Reserved");
                uc_dashboard.LoadTripleRoom("Reserved");
            }
            else
            {
                uc_dashboard.LoadSingleRoom();
                uc_dashboard.LoadDoubleRoom();
                uc_dashboard.LoadTripleRoom();
            }
        }

        private void uC_Dashboard2_Load_1(object sender, EventArgs e)
        {
            uc_dashboard = new WindowsForm_Project.All_User_Control.UC_Dashboard();
            Controls.Add(uc_dashboard);
        }

        private void cboccupied_CheckedChanged(object sender, EventArgs e)
        {
            if (cboccupied.Checked && cbclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Occupied", "Clean");
                uc_dashboard.LoadDoubleRoom("Occupied", "Clean");
                uc_dashboard.LoadTripleRoom("Occupied", "Clean");
            }
            else if (cboccupied.Checked && cbnotclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Occupied", "Not Clean");
                uc_dashboard.LoadDoubleRoom("Occupied", "Not Clean");
                uc_dashboard.LoadTripleRoom("Occupied", "Not Clean");
            }
            else if (cboccupied.Checked && cbinprogress.Checked)
            {
                uc_dashboard.LoadSingleRoom("Occupied", "In Progress");
                uc_dashboard.LoadDoubleRoom("Occupied", "In Progress");
                uc_dashboard.LoadTripleRoom("Occupied", "In Progress");
            }
            else if (cboccupied.Checked && cbrepair.Checked)
            {
                uc_dashboard.LoadSingleRoom("Occupied", "Repair");
                uc_dashboard.LoadDoubleRoom("Occupied", "Repair");
                uc_dashboard.LoadTripleRoom("Occupied", "Repair");
            }
            else if(cboccupied.Checked)
            {
                uc_dashboard.LoadSingleRoom("Occupied");
                uc_dashboard.LoadDoubleRoom("Occupied");
                uc_dashboard.LoadTripleRoom("Occupied");

            }
            else
            {
                uc_dashboard.LoadSingleRoom();
                uc_dashboard.LoadDoubleRoom();
                uc_dashboard.LoadTripleRoom();

            }
        }
        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbavailable.Checked && cbclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Available", "Clean");
                uc_dashboard.LoadDoubleRoom("Available", "Clean");
                uc_dashboard.LoadTripleRoom("Available", "Clean");
            }
            else if (cbavailable.Checked && cbnotclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Available", "Not Clean");
                uc_dashboard.LoadDoubleRoom("Available", "Not Clean");
                uc_dashboard.LoadTripleRoom("Available", "Not Clean");
            }
            else if (cbavailable.Checked && cbinprogress.Checked)
            {
                uc_dashboard.LoadSingleRoom("Available", "In Progress");
                uc_dashboard.LoadDoubleRoom("Available", "In Progress");
                uc_dashboard.LoadTripleRoom("Available", "In Progress");
            }
            else if (cbavailable.Checked && cbrepair.Checked)
            {
                uc_dashboard.LoadSingleRoom("Available", "Repair");
                uc_dashboard.LoadDoubleRoom("Available", "Repair");
                uc_dashboard.LoadTripleRoom("Available", "Repair");
            }
            else if (cbavailable.Checked)
            {
                uc_dashboard.LoadSingleRoom("Available");
                uc_dashboard.LoadDoubleRoom("Available");
                uc_dashboard.LoadTripleRoom("Available");

            }
            else
            {
                uc_dashboard.LoadSingleRoom();
                uc_dashboard.LoadDoubleRoom();
                uc_dashboard.LoadTripleRoom();

            }
        }

        private void cbcheckout_CheckedChanged(object sender, EventArgs e)
        {
            if (cbcheckout.Checked && cbclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Check Out", "Clean");
                uc_dashboard.LoadDoubleRoom("Check Out", "Clean");
                uc_dashboard.LoadTripleRoom("Check Out", "Clean");
            }
            else if (cbcheckout.Checked && cbnotclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Check Out", "Not Clean");
                uc_dashboard.LoadDoubleRoom("Check Out", "Not Clean");
                uc_dashboard.LoadTripleRoom("Check Out", "Not Clean");
            }
            else if (cbcheckout.Checked && cbinprogress.Checked)
            {
                uc_dashboard.LoadSingleRoom("Check Out", "In Progress");
                uc_dashboard.LoadDoubleRoom("Check Out", "In Progress");
                uc_dashboard.LoadTripleRoom("Check Out", "In Progress");
            }
            else if (cbcheckout.Checked && cbrepair.Checked)
            {
                uc_dashboard.LoadSingleRoom("Check Out", "Repair");
                uc_dashboard.LoadDoubleRoom("Check Out", "Repair");
                uc_dashboard.LoadTripleRoom("Check Out", "Repair");
            }
            else if (cbcheckout.Checked)
            {
                uc_dashboard.LoadSingleRoom("Check Out");
                uc_dashboard.LoadDoubleRoom("Check Out");
                uc_dashboard.LoadTripleRoom("Check Out");

            }
            else
            {
                uc_dashboard.LoadSingleRoom();
                uc_dashboard.LoadDoubleRoom();
                uc_dashboard.LoadTripleRoom();

            }
        }

        private void cbsingle_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cbclean_CheckedChanged(object sender, EventArgs e)
        {
            if (cbclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Clean");
                uc_dashboard.LoadDoubleRoom("Clean");
                uc_dashboard.LoadTripleRoom("Clean");

            }
            else
            {
                uc_dashboard.LoadSingleRoom();
                uc_dashboard.LoadDoubleRoom();
                uc_dashboard.LoadTripleRoom();

            }
        }

        private void cbnotclean_CheckedChanged(object sender, EventArgs e)
        {
            if (cbnotclean.Checked)
            {
                uc_dashboard.LoadSingleRoom("Not Clean");
                uc_dashboard.LoadDoubleRoom("Not Clean");
                uc_dashboard.LoadTripleRoom("Not Clean");

            }
            else
            {
                uc_dashboard.LoadSingleRoom();
                uc_dashboard.LoadDoubleRoom();
                uc_dashboard.LoadTripleRoom();

            }
        }

        private void cbinprogress_CheckedChanged(object sender, EventArgs e)
        {
            if (cbinprogress.Checked)
            {
                uc_dashboard.LoadSingleRoom("In Progress");
                uc_dashboard.LoadDoubleRoom("In Progress");
                uc_dashboard.LoadTripleRoom("In Progress");

            }
            else
            {
                uc_dashboard.LoadSingleRoom();
                uc_dashboard.LoadDoubleRoom();
                uc_dashboard.LoadTripleRoom();

            }
        }

        private void cbrepair_CheckedChanged(object sender, EventArgs e)
        {
            if (cbrepair.Checked)
            {
                uc_dashboard.LoadSingleRoom("Repair");
                uc_dashboard.LoadDoubleRoom("Repair");
                uc_dashboard.LoadTripleRoom("Repair");

            }
            else
            {
                uc_dashboard.LoadSingleRoom();
                uc_dashboard.LoadDoubleRoom();
                uc_dashboard.LoadTripleRoom();

            }
        }

        private void cbreserved_Enter(object sender, EventArgs e)
        {
            guna2CheckBox1_CheckedChanged(this, EventArgs.Empty);
        }

        private void guna2Panel3_Enter(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel3_MouseEnter(object sender, EventArgs e)
        {
            int reserved = 0;
            int occupied = 0;
            int available = 0;
            int checkout = 0;
            int singletype = 0;
            int doubletype = 0;
            int tripletype = 0;
            int clean = 0;
            int notclean = 0;
            int inprogress = 0;
            int repair = 0;
            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  SELECT COUNT(maphong) FROM Update_room WHERE status_room = 'Reserved'";
            string query2 = @"  SELECT COUNT(maphong) FROM Update_room WHERE status_room = 'Occupied'";
            string query3 = @"  SELECT COUNT(maphong) FROM Update_room WHERE status_room = 'Available'";
            string query4 = @"  SELECT COUNT(maphong) FROM Update_room WHERE status_room = 'Check Out'";
            string query5 = @"  SELECT COUNT(maphong) FROM Room WHERE numbed = 1";
            string query6 = @"  SELECT COUNT(maphong) FROM Room WHERE numbed = 2";
            string query7 = @"  SELECT COUNT(maphong) FROM Room WHERE numbed = 3";
            string query8 = @"  SELECT COUNT(maphong) FROM Update_room WHERE house_keeping = 'Clean'";
            string query9 = @"  SELECT COUNT(maphong) FROM Update_room WHERE house_keeping = 'Not Clean'";
            string query10 = @"  SELECT COUNT(maphong) FROM Update_room WHERE house_keeping = 'In Progress'";
            string query11 = @"  SELECT COUNT(maphong) FROM Update_room WHERE house_keeping = 'Repair'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            reserved = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            occupied = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query3, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            available = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query4, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            checkout = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query5, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            singletype = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query6, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            doubletype = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query7, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            tripletype = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query8, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            clean = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query9, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            notclean = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query10, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            inprogress = reader.GetInt32(0);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query11, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            repair = reader.GetInt32(0);
                        }
                    }
                }
            }
            Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
            guna2HtmlLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel.Text = reserved.ToString();
            guna2HtmlLabel.Location = new Point(185, 56);
            guna2Panel3.Controls.Add(guna2HtmlLabel);

            Guna2HtmlLabel guna2HtmlLabel1 = new Guna2HtmlLabel();
            guna2HtmlLabel1.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel1.Text = occupied.ToString();
            guna2HtmlLabel1.Location = new Point(185, 91);
            guna2Panel3.Controls.Add(guna2HtmlLabel1);

            Guna2HtmlLabel guna2HtmlLabel2 = new Guna2HtmlLabel();
            guna2HtmlLabel2.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel2.Text = available.ToString();
            guna2HtmlLabel2.Location = new Point(185, 125);
            guna2Panel3.Controls.Add(guna2HtmlLabel2);

            Guna2HtmlLabel guna2HtmlLabel3 = new Guna2HtmlLabel();
            guna2HtmlLabel3.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel3.Text = checkout.ToString();
            guna2HtmlLabel3.Location = new Point(185, 159);
            guna2Panel3.Controls.Add(guna2HtmlLabel3);

            Guna2HtmlLabel guna2HtmlLabel4 = new Guna2HtmlLabel();
            guna2HtmlLabel4.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel4.Text = singletype.ToString();
            guna2HtmlLabel4.Location = new Point(165, 234);
            guna2Panel3.Controls.Add(guna2HtmlLabel4);

            Guna2HtmlLabel guna2HtmlLabel5 = new Guna2HtmlLabel();
            guna2HtmlLabel5.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel5.Text = doubletype.ToString();
            guna2HtmlLabel5.Location = new Point(165, 269);
            guna2Panel3.Controls.Add(guna2HtmlLabel5);

            Guna2HtmlLabel guna2HtmlLabel6 = new Guna2HtmlLabel();
            guna2HtmlLabel6.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel6.Text = tripletype.ToString();
            guna2HtmlLabel6.Location = new Point(165, 303);
            guna2Panel3.Controls.Add(guna2HtmlLabel6);

            Guna2HtmlLabel guna2HtmlLabel7 = new Guna2HtmlLabel();
            guna2HtmlLabel7.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel7.Text = clean.ToString();
            guna2HtmlLabel7.Location = new Point(165, 382);
            guna2Panel3.Controls.Add(guna2HtmlLabel7);

            Guna2HtmlLabel guna2HtmlLabel8 = new Guna2HtmlLabel();
            guna2HtmlLabel8.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel8.Text = notclean.ToString();
            guna2HtmlLabel8.Location = new Point(165, 417);
            guna2Panel3.Controls.Add(guna2HtmlLabel8);

            Guna2HtmlLabel guna2HtmlLabel9 = new Guna2HtmlLabel();
            guna2HtmlLabel9.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel9.Text = inprogress.ToString();
            guna2HtmlLabel9.Location = new Point(165, 451);
            guna2Panel3.Controls.Add(guna2HtmlLabel9);

            Guna2HtmlLabel guna2HtmlLabel10 = new Guna2HtmlLabel();
            guna2HtmlLabel10.Font = new Font("Arial", 12, FontStyle.Regular);
            guna2HtmlLabel10.Text = repair.ToString();
            guna2HtmlLabel10.Location = new Point(165, 485);
            guna2Panel3.Controls.Add(guna2HtmlLabel10);
        }

        private void uC_Dashboard2_Enter(object sender, EventArgs e)
        {

        }
    }
}
