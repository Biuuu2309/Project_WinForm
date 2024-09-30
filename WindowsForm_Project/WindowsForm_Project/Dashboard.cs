using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
        private DateTime currentDate;

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

        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
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
            WindowsForm_Project.All_User_Control.UC_Dashboard uc_dashboard = new WindowsForm_Project.All_User_Control.UC_Dashboard();
            uc_dashboard.Location = new Point(220, 114);
            this.Controls.Add(uc_dashboard);
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
            Application.Exit();
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
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            //MovePanel(btnreport);
        }

        private void btnemployee_Click_1(object sender, EventArgs e)
        {
            MovePanel(btnmanage);
            uC_Manage1.Visible = true;
            uC_Manage1.BringToFront();
            // Hide other user controls
            uC_Dashboard2.Visible = false;
            uC_Addroom1.Visible = false;
            uC_Bookings2.Visible = false;
            uC_Customer1.Visible = false;
            uC_Serve1.Visible = false;
        }

        private void btncheckout_Click_1(object sender, EventArgs e)
        {
            MovePanel(btncheckout);
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
    }
}
