using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WindowsForm_Project.All_User_Control
{
    public partial class UC_Dashboard : UserControl
    {
        public UC_Dashboard()
        {
            InitializeComponent();
            guna2Panel4.Controls.Add(guna2Panel1);
            guna2Panel4.Controls.Add(guna2Panel2);
            guna2Panel4.Controls.Add(guna2Panel3);
        }

        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            guna2Panel1.AutoScroll = true;
            guna2Panel1.HorizontalScroll.Enabled = true;
            guna2Panel1.HorizontalScroll.Visible = true;

        }

        private void guna2Panel1_Scroll(object sender, ScrollEventArgs e)
        {
            guna2Panel1.AutoScroll = true;
            guna2Panel1.HorizontalScroll.Enabled = true;
            guna2Panel1.HorizontalScroll.Visible = true;
        }

        public void LoadSingleRoom(string statusCon1 = "", string statusCon2 = "")
        {
            guna2Panel1.AutoScroll = true;
            guna2Panel1.HorizontalScroll.Enabled = true;
            guna2Panel1.HorizontalScroll.Visible = true;
            guna2Panel1.Controls.Clear();
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.BringToFront();

            Guna2HtmlLabel guna2HtmlLabel6 = new Guna2HtmlLabel();
            guna2HtmlLabel6.Text = "Single room";
            guna2HtmlLabel6.Font = new Font("Comic Sans MS", 20, FontStyle.Bold);
            guna2HtmlLabel6.Location = new Point(25, 17);
            guna2Panel1.Controls.Add(guna2HtmlLabel6);
            int spacing = 20;
            int x = 20;
            int y = 120;

            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  SELECT roomnumber FROM Room INNER JOIN Update_room ON Room.maphong = Update_room.maphong WHERE numbed = 1";
            string query2 = @"  SELECT status_room 
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE Room.numbed = 1";
            string query3 = @"  SELECT first_name + ' ' + last_name as fullname
                                FROM Customer
                                INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
                                INNER JOIN Update_room ON Bookings.maphong = Update_room.maphong
                                WHERE numbed = 1";
            string query4 = @"  SELECT DATEDIFF(DAY, date_ci, date_co) AS demngay
                                FROM Bookings 
                                INNER JOIN Update_room ON Bookings.maphong = Update_room.maphong
                                WHERE numbed = 1";
            string query5 = @"  SELECT house_keeping
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE numbed = 1";

            if (!string.IsNullOrEmpty(statusCon1) && !string.IsNullOrEmpty(statusCon2))
            {
                query1 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query2 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query3 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query4 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query5 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
            }
            else if (!string.IsNullOrEmpty(statusCon1) && string.IsNullOrEmpty(statusCon2))
            {
                if (statusCon1 == "Reserved" || statusCon1 == "Occupied" || statusCon1 == "Available" || statusCon1 == "Check Out")
                {
                    query1 += $" AND Update_room.status_room = '{statusCon1}'";
                    query2 += $" AND Update_room.status_room = '{statusCon1}'";
                    query3 += $" AND Update_room.status_room = '{statusCon1}'";
                    query4 += $" AND Update_room.status_room = '{statusCon1}'";
                    query5 += $" AND Update_room.status_room = '{statusCon1}'";
                }
                else if (statusCon1 == "Clean" || statusCon1 == "Not Clean" || statusCon1 == "In Progress" || statusCon1 == "Repair")
                {
                    query1 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query2 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query3 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query4 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query5 += $" AND Update_room.house_keeping = '{statusCon1}'";
                }
            }
            
            List<int> roomnumber = new List<int>();
            List<string> statusroom = new List<string>();
            List<string> fullname = new List<string>();
            List<int> demngay = new List<int>();
            List<string> housekeeping = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roomnumber.Add(reader.GetInt32(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusroom.Add(reader.GetString(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query3, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fullname.Add(reader.GetString(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query4, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            demngay.Add(reader.GetInt32(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query5, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            housekeeping.Add(reader.GetString(0));
                        }
                    }
                }
            }

            for (int i = 0; i < roomnumber.Count; i++)
            {
                Guna2Panel childPanel = new Guna2Panel();
                childPanel.Size = new Size(270, 180);
                childPanel.Location = new Point(x, y);
                childPanel.BackColor = Color.White;
                childPanel.BackColor = Color.White;

                Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
                guna2HtmlLabel.Text = "Room " + roomnumber[i];
                guna2HtmlLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel.Location = new Point(10, 10);

                Guna2HtmlLabel guna2HtmlLabel2 = new Guna2HtmlLabel();
                guna2HtmlLabel2.Location = new Point(200, 10);
                guna2HtmlLabel2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel2.Text = statusroom[i];


                PictureBox pictureBox1 = new PictureBox();

                pictureBox1.BackColor = Color.Transparent;

                pictureBox1.BackColor = Color.Transparent;
                Guna2HtmlLabel guna2HtmlLabel1 = new Guna2HtmlLabel();
                Guna2HtmlLabel guna2HtmlLabel3 = new Guna2HtmlLabel();

                Guna2Panel guna2Panel = new Guna2Panel();
                guna2Panel.Height = 50;
                guna2Panel.BackColor = Color.White;
                guna2Panel.Dock = DockStyle.Bottom;

                if (statusroom[i] == "Available")
                {
                    pictureBox1.Image = Properties.Resources.check__5_;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(50, 57);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Segoe UI", 15, FontStyle.Bold | FontStyle.Italic);
                    guna2HtmlLabel1.Text = "Free room";
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    childPanel.BackgroundImage = Properties.Resources.Untitled_design__1___1_;
                    guna2Panel.BackgroundImage = Properties.Resources.wallhaven_0w2wdx_360x50__1_;
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    guna2HtmlLabel3.Text = "0";
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();

                }
                else
                {
                    pictureBox1.Image = Properties.Resources.user_profile;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(50, 57);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Segoe UI", 15, FontStyle.Bold | FontStyle.Italic);
                    guna2HtmlLabel1.Text = fullname[i];
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    childPanel.BackgroundImage = Properties.Resources.Untitled_design__4_;
                    guna2Panel.BackgroundImage = Properties.Resources.wallhaven_4lxwoq_360x50__1_;
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    guna2HtmlLabel3.Text = demngay[i].ToString();
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();
                }


                PictureBox pictureBox2 = new PictureBox();
                pictureBox2.Image = Properties.Resources.spring_calendar;
                pictureBox2.BackColor = Color.Transparent;
                pictureBox2.Size = new Size(36, 36);
                pictureBox2.Location = new Point(3, 3);
                pictureBox2.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel4 = new Guna2HtmlLabel();
                guna2HtmlLabel4.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel4.Text = "days";
                guna2HtmlLabel4.Location = new Point(65, 10);
                guna2HtmlLabel4.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel5 = new Guna2HtmlLabel();
                guna2HtmlLabel5.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel5.Text = housekeeping[i];
                guna2HtmlLabel5.Location = new Point(220, 10);
                guna2HtmlLabel5.BringToFront();

                PictureBox pictureBox3 = new PictureBox();
                pictureBox3.BackColor = Color.Transparent;
                pictureBox3.Size = new Size(36, 36);
                pictureBox3.Location = new Point(180, 3);
                if (housekeeping[i] == "Clean")
                {
                    pictureBox3.Image = Properties.Resources.check__7_;
                    guna2HtmlLabel5.Location = new Point(220, 10);
                    pictureBox3.Location = new Point(190, 3);
                }
                else if (housekeeping[i] == "Not Clean")
                {
                    pictureBox3.Image = Properties.Resources.x_mark;
                }
                else if (housekeeping[i] == "In Progress")
                {
                    pictureBox3.Image = Properties.Resources.clock__1_;
                }
                else
                {
                    pictureBox3.Image = Properties.Resources.hair_dryer;
                }

                pictureBox3.BringToFront();

                guna2Panel.Controls.Add(pictureBox2);
                guna2Panel.Controls.Add(guna2HtmlLabel3);
                guna2Panel.Controls.Add(guna2HtmlLabel4);
                guna2Panel.Controls.Add(guna2HtmlLabel5);
                guna2Panel.Controls.Add(pictureBox3);


                childPanel.Controls.Add(guna2HtmlLabel);
                childPanel.Controls.Add(guna2Panel);
                childPanel.Controls.Add(guna2HtmlLabel2);
                childPanel.Controls.Add(pictureBox1);
                childPanel.Controls.Add(guna2HtmlLabel1);

                guna2Panel1.Controls.Add(childPanel);

                x += childPanel.Width + spacing;
            }
        }
        
        private void guna2Panel1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = this.FindForm() as Dashboard;

            if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsCleanChecked)
            {
                LoadSingleRoom("Reserved", "Clean");
            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsNotCleanChecked)
            {
                LoadSingleRoom("Reserved", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsInProgressChecked)
            {
                LoadSingleRoom("Reserved", "In Progress");

            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsRepairChecked)
            {
                LoadSingleRoom("Reserved", "Repair");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsCleanChecked)
            {
                LoadSingleRoom("Occupied", "Clean");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsNotCleanChecked)
            {
                LoadSingleRoom("Occupied", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsInProgressChecked)
            {
                LoadSingleRoom("Occupied", "In Progress");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsRepairChecked)
            {
                LoadSingleRoom("Occupied", "Repair");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsCleanChecked)
            {
                LoadSingleRoom("Available", "Clean");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsNotCleanChecked)
            {
                LoadSingleRoom("Available", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsInProgressChecked)
            {
                LoadSingleRoom("Available", "In Progress");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsRepairChecked)
            {
                LoadSingleRoom("Available", "Repair");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsCleanChecked)
            {
                LoadSingleRoom("Check Out", "Clean");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsNotCleanChecked)
            {
                LoadSingleRoom("Check Out", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsInProgressChecked)
            {
                LoadSingleRoom("Check Out", "In Progress");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsRepairChecked)
            {
                LoadSingleRoom("Check Out", "Repair");

            }
            else if (dashboard != null && dashboard.IsReservedChecked)
            {
                LoadSingleRoom("Reserved");  
            }
            else if (dashboard != null && dashboard.IsOccupiedChecked)
            {
                LoadSingleRoom("Occupied");
            }
            else if (dashboard != null && dashboard.IsAvailableChecked)
            {
                LoadSingleRoom("Available");
            }
            else if (dashboard != null && dashboard.IsCheckOutChecked)
            {
                LoadSingleRoom("Check Out");
            }
            else if (dashboard != null && dashboard.IsCleanChecked)
            {
                LoadSingleRoom("Clean");
            }
            else if (dashboard != null && dashboard.IsNotCleanChecked)
            {
                LoadSingleRoom("Not Clean");
            }
            else if (dashboard != null && dashboard.IsInProgressChecked)
            {
                LoadSingleRoom("In Progress");
            }
            else if (dashboard != null && dashboard.IsRepairChecked)
            {
                LoadSingleRoom("Repair");
            }
            else
            {
                LoadSingleRoom();
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadDoubleRoom(string statusCon1 = "", string statusCon2 = "")
        {
            guna2Panel2.AutoScroll = true;
            guna2Panel2.HorizontalScroll.Enabled = true;
            guna2Panel2.HorizontalScroll.Visible = true;
            guna2Panel2.Controls.Clear();
            guna2Panel2.Controls.Clear();

            Guna2HtmlLabel guna2HtmlLabel6 = new Guna2HtmlLabel();
            guna2HtmlLabel6.Text = "Double room";
            guna2HtmlLabel6.Font = new Font("Comic Sans MS", 20, FontStyle.Bold);
            guna2HtmlLabel6.Location = new Point(25, 17);
            guna2Panel2.Controls.Add(guna2HtmlLabel6);

            int spacing = 20;
            int x = 20;
            int y = 120;

            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  SELECT roomnumber FROM Room INNER JOIN Update_room ON Room.maphong = Update_room.maphong WHERE numbed = 2";
            string query2 = @"  SELECT status_room 
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE Room.numbed = 2";
            string query3 = @"  SELECT first_name + ' ' + last_name as fullname
                                FROM Customer
                                INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
                                INNER JOIN Update_room ON Bookings.maphong = Update_room.maphong
                                WHERE numbed = 2";
            string query4 = @"  SELECT DATEDIFF(DAY, date_ci, date_co) AS demngay
                                FROM Bookings 
                                INNER JOIN Update_room ON Bookings.maphong = Update_room.maphong
                                WHERE numbed = 2";
            string query5 = @"  SELECT house_keeping
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE numbed = 2";

            if (!string.IsNullOrEmpty(statusCon1) && !string.IsNullOrEmpty(statusCon2))
            {
                query1 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query2 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query3 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query4 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query5 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
            }
            else if (!string.IsNullOrEmpty(statusCon1) && string.IsNullOrEmpty(statusCon2))
            {
                if (statusCon1 == "Reserved" || statusCon1 == "Occupied" || statusCon1 == "Available" || statusCon1 == "Check Out")
                {
                    query1 += $" AND Update_room.status_room = '{statusCon1}'";
                    query2 += $" AND Update_room.status_room = '{statusCon1}'";
                    query3 += $" AND Update_room.status_room = '{statusCon1}'";
                    query4 += $" AND Update_room.status_room = '{statusCon1}'";
                    query5 += $" AND Update_room.status_room = '{statusCon1}'";
                }
                else if (statusCon1 == "Clean" || statusCon1 == "Not Clean" || statusCon1 == "In Progress" || statusCon1 == "Repair")
                {
                    query1 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query2 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query3 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query4 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query5 += $" AND Update_room.house_keeping = '{statusCon1}'";
                }
            }

            List<int> roomnumber = new List<int>();
            List<string> statusroom = new List<string>();
            List<string> fullname = new List<string>();
            List<int> demngay = new List<int>();
            List<string> housekeeping = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roomnumber.Add(reader.GetInt32(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusroom.Add(reader.GetString(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query3, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fullname.Add(reader.GetString(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query4, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            demngay.Add(reader.GetInt32(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query5, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            housekeeping.Add(reader.GetString(0));
                        }
                    }
                }
            }

            for (int i = 0; i < roomnumber.Count; i++)
            {
                Guna2Panel childPanel = new Guna2Panel();
                childPanel.Size = new Size(270, 180);
                childPanel.Location = new Point(x, y);
                childPanel.BackColor = Color.White;

                Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
                guna2HtmlLabel.Text = "Room " + roomnumber[i];
                guna2HtmlLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel.Location = new Point(10, 10);

                Guna2HtmlLabel guna2HtmlLabel2 = new Guna2HtmlLabel();
                guna2HtmlLabel2.Location = new Point(200, 10);
                guna2HtmlLabel2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel2.Text = statusroom[i];

                PictureBox pictureBox1 = new PictureBox();
                pictureBox1.BackColor = Color.Transparent;
                Guna2HtmlLabel guna2HtmlLabel1 = new Guna2HtmlLabel();
                Guna2HtmlLabel guna2HtmlLabel3 = new Guna2HtmlLabel();

                Guna2Panel guna2Panel = new Guna2Panel();
                guna2Panel.Height = 50;
                guna2Panel.BackColor = Color.DarkViolet;
                guna2Panel.Dock = DockStyle.Bottom;

                if (statusroom[i] == "Available")
                {
                    pictureBox1.Image = Properties.Resources.check__5_;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(50, 57);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Segoe UI", 15, FontStyle.Bold | FontStyle.Italic);
                    guna2HtmlLabel1.Text = "Free room";
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    childPanel.BackgroundImage = Properties.Resources.Untitled_design__1___1_;
                    guna2Panel.BackgroundImage = Properties.Resources.wallhaven_0w2wdx_360x50__1_;
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    guna2HtmlLabel3.Text = "0";
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();

                }
                else
                {
                    pictureBox1.Image = Properties.Resources.user_profile;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(50, 57);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Segoe UI", 15, FontStyle.Bold | FontStyle.Italic);
                    guna2HtmlLabel1.Text = fullname[i];
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    childPanel.BackgroundImage = Properties.Resources.Untitled_design__4_;
                    guna2Panel.BackgroundImage = Properties.Resources.wallhaven_4lxwoq_360x50__1_;
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    guna2HtmlLabel3.Text = demngay[i].ToString();
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();
                }



                PictureBox pictureBox2 = new PictureBox();
                pictureBox2.Image = Properties.Resources.spring_calendar;
                pictureBox2.BackColor = Color.Transparent;
                pictureBox2.Size = new Size(36, 36);
                pictureBox2.Location = new Point(3, 3);
                pictureBox2.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel4 = new Guna2HtmlLabel();
                guna2HtmlLabel4.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel4.Text = "days";
                guna2HtmlLabel4.Location = new Point(65, 10);
                guna2HtmlLabel4.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel5 = new Guna2HtmlLabel();
                guna2HtmlLabel5.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel5.Text = housekeeping[i];
                guna2HtmlLabel5.Location = new Point(200, 10);
                guna2HtmlLabel5.BringToFront();


                PictureBox pictureBox3 = new PictureBox();
                pictureBox3.BackColor = Color.Transparent;
                pictureBox3.Size = new Size(36, 36);
                pictureBox3.Location = new Point(160, 3);
                if (housekeeping[i] == "Clean")
                {
                    pictureBox3.Image = Properties.Resources.check__7_;
                    guna2HtmlLabel5.Location = new Point(220, 10);
                    pictureBox3.Location = new Point(190, 3);

                }
                else if (housekeeping[i] == "Not Clean")
                {
                    pictureBox3.Image = Properties.Resources.x_mark;

                }
                else if (housekeeping[i] == "In Progress")
                {
                    pictureBox3.Image = Properties.Resources.clock__1_;

                }
                else
                {
                    pictureBox3.Image = Properties.Resources.hair_dryer;
                }

                pictureBox3.BringToFront();

                guna2Panel.Controls.Add(pictureBox2);
                guna2Panel.Controls.Add(guna2HtmlLabel3);
                guna2Panel.Controls.Add(guna2HtmlLabel4);
                guna2Panel.Controls.Add(guna2HtmlLabel5);
                guna2Panel.Controls.Add(pictureBox3);


                childPanel.Controls.Add(guna2HtmlLabel);
                childPanel.Controls.Add(guna2Panel);
                childPanel.Controls.Add(guna2HtmlLabel2);
                childPanel.Controls.Add(pictureBox1);
                childPanel.Controls.Add(guna2HtmlLabel1);

                guna2Panel2.Controls.Add(childPanel);

                x += childPanel.Width + spacing;
            }
        }

        private void guna2Panel2_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = this.FindForm() as Dashboard;

            if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsCleanChecked)
            {
                LoadDoubleRoom("Reserved", "Clean");
            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsNotCleanChecked)
            {
                LoadDoubleRoom("Reserved", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsInProgressChecked)
            {
                LoadDoubleRoom("Reserved", "In Progress");

            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsRepairChecked)
            {
                LoadDoubleRoom("Reserved", "Repair");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsCleanChecked)
            {
                LoadDoubleRoom("Occupied", "Clean");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsNotCleanChecked)
            {
                LoadDoubleRoom("Occupied", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsInProgressChecked)
            {
                LoadDoubleRoom("Occupied", "In Progress");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsRepairChecked)
            {
                LoadDoubleRoom("Occupied", "Repair");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsCleanChecked)
            {
                LoadDoubleRoom("Available", "Clean");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsNotCleanChecked)
            {
                LoadDoubleRoom("Available", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsInProgressChecked)
            {
                LoadDoubleRoom("Available", "In Progress");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsRepairChecked)
            {
                LoadDoubleRoom("Available", "Repair");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsCleanChecked)
            {
                LoadDoubleRoom("Check Out", "Clean");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsNotCleanChecked)
            {
                LoadDoubleRoom("Check Out", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsInProgressChecked)
            {
                LoadDoubleRoom("Check Out", "In Progress");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsRepairChecked)
            {
                LoadDoubleRoom("Check Out", "Repair");

            }
            else if (dashboard != null && dashboard.IsReservedChecked)
            {
                LoadDoubleRoom("Reserved");
            }
            else if (dashboard != null && dashboard.IsOccupiedChecked)
            {
                LoadDoubleRoom("Occupied");
            }
            else if (dashboard != null && dashboard.IsAvailableChecked)
            {
                LoadDoubleRoom("Available");
            }
            else if (dashboard != null && dashboard.IsCheckOutChecked)
            {
                LoadDoubleRoom("Check Out");
            }
            else if (dashboard != null && dashboard.IsCleanChecked)
            {
                LoadDoubleRoom("Clean");
            }
            else if (dashboard != null && dashboard.IsNotCleanChecked)
            {
                LoadDoubleRoom("Not Clean");
            }
            else if (dashboard != null && dashboard.IsInProgressChecked)
            {
                LoadDoubleRoom("In Progress");
            }
            else if (dashboard != null && dashboard.IsRepairChecked)
            {
                LoadDoubleRoom("Repair");
            }
            else
            {
                LoadDoubleRoom();
            }
        }

        public void LoadTripleRoom(string statusCon1 = "", string statusCon2 = "")
        {
            guna2Panel3.AutoScroll = true;
            guna2Panel3.HorizontalScroll.Enabled = true;
            guna2Panel3.HorizontalScroll.Visible = true;
            guna2Panel3.Controls.Clear();
            guna2Panel3.Controls.Clear();

            Guna2HtmlLabel guna2HtmlLabel6 = new Guna2HtmlLabel();
            guna2HtmlLabel6.Text = "Triple room";
            guna2HtmlLabel6.Font = new Font("Comic Sans MS", 20, FontStyle.Bold);
            guna2HtmlLabel6.Location = new Point(25, 17);
            guna2Panel3.Controls.Add(guna2HtmlLabel6);

            int spacing = 40;
            int x = 40;
            int y = 120;

            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  SELECT roomnumber FROM Room INNER JOIN Update_room ON Room.maphong = Update_room.maphong WHERE numbed = 3";
            string query2 = @"  SELECT status_room 
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE Room.numbed = 3";
            string query3 = @"  SELECT first_name + ' ' + last_name as fullname
                                FROM Customer
                                INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
                                INNER JOIN Update_room ON Bookings.maphong = Update_room.maphong
                                WHERE numbed = 3";
            string query4 = @"  SELECT DATEDIFF(DAY, date_ci, date_co) AS demngay
                                FROM Bookings 
                                INNER JOIN Update_room ON Bookings.maphong = Update_room.maphong
                                WHERE numbed = 3";
            string query5 = @"  SELECT house_keeping
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE numbed = 3";

            if (!string.IsNullOrEmpty(statusCon1) && !string.IsNullOrEmpty(statusCon2))
            {
                query1 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query2 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query3 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query4 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
                query5 += $" AND Update_room.status_room = '{statusCon1}' AND Update_room.house_keeping = '{statusCon2}'";
            }
            else if (!string.IsNullOrEmpty(statusCon1) && string.IsNullOrEmpty(statusCon2))
            {
                if (statusCon1 == "Reserved" || statusCon1 == "Occupied" || statusCon1 == "Available" || statusCon1 == "Check Out")
                {
                    query1 += $" AND Update_room.status_room = '{statusCon1}'";
                    query2 += $" AND Update_room.status_room = '{statusCon1}'";
                    query3 += $" AND Update_room.status_room = '{statusCon1}'";
                    query4 += $" AND Update_room.status_room = '{statusCon1}'";
                    query5 += $" AND Update_room.status_room = '{statusCon1}'";
                }
                else if (statusCon1 == "Clean" || statusCon1 == "Not Clean" || statusCon1 == "In Progress" || statusCon1 == "Repair")
                {
                    query1 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query2 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query3 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query4 += $" AND Update_room.house_keeping = '{statusCon1}'";
                    query5 += $" AND Update_room.house_keeping = '{statusCon1}'";
                }
            }

            List<int> roomnumber = new List<int>();
            List<string> statusroom = new List<string>();
            List<string> fullname = new List<string>();
            List<int> demngay = new List<int>();
            List<string> housekeeping = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roomnumber.Add(reader.GetInt32(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusroom.Add(reader.GetString(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query3, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fullname.Add(reader.GetString(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query4, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            demngay.Add(reader.GetInt32(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query5, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            housekeeping.Add(reader.GetString(0));
                        }
                    }
                }
            }

            for (int i = 0; i < roomnumber.Count; i++)
            {
                Guna2Panel childPanel = new Guna2Panel();
                childPanel.Size = new Size(270, 180);
                childPanel.Location = new Point(x, y);
                childPanel.BackColor = Color.White;

                Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
                guna2HtmlLabel.Text = "Room " + roomnumber[i];
                guna2HtmlLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel.Location = new Point(10, 10);

                Guna2HtmlLabel guna2HtmlLabel2 = new Guna2HtmlLabel();
                guna2HtmlLabel2.Location = new Point(200, 10);
                guna2HtmlLabel2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel2.Text = statusroom[i];

                PictureBox pictureBox1 = new PictureBox();
                pictureBox1.BackColor = Color.Transparent;
                Guna2HtmlLabel guna2HtmlLabel1 = new Guna2HtmlLabel();
                Guna2HtmlLabel guna2HtmlLabel3 = new Guna2HtmlLabel();

                Guna2Panel guna2Panel = new Guna2Panel();
                guna2Panel.Height = 50;
                guna2Panel.BackColor = Color.DarkViolet;
                guna2Panel.Dock = DockStyle.Bottom;

                if (statusroom[i] == "Available")
                {
                    pictureBox1.Image = Properties.Resources.check__5_;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(50, 57);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Segoe UI", 15, FontStyle.Bold | FontStyle.Italic);
                    guna2HtmlLabel1.Text = "Free room";
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    childPanel.BackgroundImage = Properties.Resources.Untitled_design__1___1_;
                    guna2Panel.BackgroundImage = Properties.Resources.wallhaven_0w2wdx_360x50__1_;
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    guna2HtmlLabel3.Text = "0";
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();

                }
                else
                {
                    pictureBox1.Image = Properties.Resources.user_profile;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(50, 57);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Segoe UI", 15, FontStyle.Bold | FontStyle.Italic);
                    guna2HtmlLabel1.Text = fullname[i];
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    childPanel.BackgroundImage = Properties.Resources.Untitled_design__4_;
                    guna2Panel.BackgroundImage = Properties.Resources.wallhaven_4lxwoq_360x50__1_;
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    guna2HtmlLabel3.Text = demngay[i].ToString();
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();
                }



                PictureBox pictureBox2 = new PictureBox();
                pictureBox2.Image = Properties.Resources.spring_calendar;
                pictureBox2.BackColor = Color.Transparent;
                pictureBox2.Size = new Size(36, 36);
                pictureBox2.Location = new Point(3, 3);
                pictureBox2.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel4 = new Guna2HtmlLabel();
                guna2HtmlLabel4.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel4.Text = "days";
                guna2HtmlLabel4.Location = new Point(65, 10);
                guna2HtmlLabel4.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel5 = new Guna2HtmlLabel();
                guna2HtmlLabel5.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel5.Text = housekeeping[i];
                guna2HtmlLabel5.Location = new Point(200, 10);
                guna2HtmlLabel5.BringToFront();

                PictureBox pictureBox3 = new PictureBox();
                pictureBox3.BackColor = Color.Transparent;
                pictureBox3.Size = new Size(36, 36);
                pictureBox3.Location = new Point(160, 3);
                if (housekeeping[i] == "Clean")
                {
                    pictureBox3.Image = Properties.Resources.check__7_;
                    guna2HtmlLabel5.Location = new Point(220, 10);
                    pictureBox3.Location = new Point(190, 3);

                }
                else if (housekeeping[i] == "Not Clean")
                {
                    pictureBox3.Image = Properties.Resources.x_mark;
                }
                else if (housekeeping[i] == "In Progress")
                {
                    pictureBox3.Image = Properties.Resources.clock__1_;

                }
                else
                {
                    pictureBox3.Image = Properties.Resources.hair_dryer;
                }
                pictureBox3.BringToFront();

                guna2Panel.Controls.Add(pictureBox2);
                guna2Panel.Controls.Add(guna2HtmlLabel3);
                guna2Panel.Controls.Add(guna2HtmlLabel4);
                guna2Panel.Controls.Add(guna2HtmlLabel5);
                guna2Panel.Controls.Add(pictureBox3);


                childPanel.Controls.Add(guna2HtmlLabel);
                childPanel.Controls.Add(guna2Panel);
                childPanel.Controls.Add(guna2HtmlLabel2);
                childPanel.Controls.Add(pictureBox1);
                childPanel.Controls.Add(guna2HtmlLabel1);

                guna2Panel3.Controls.Add(childPanel);

                x += childPanel.Width + spacing;

                if ((i + 1) % 2 == 0)
                {
                    x = 20;
                    y += childPanel.Height + spacing;
                }
            }
        }

        private void guna2Panel3_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = this.FindForm() as Dashboard;

            if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsCleanChecked)
            {
                LoadTripleRoom("Reserved", "Clean");
            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsNotCleanChecked)
            {
                LoadTripleRoom("Reserved", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsInProgressChecked)
            {
                LoadTripleRoom("Reserved", "In Progress");

            }
            else if (dashboard != null && dashboard.IsReservedChecked && dashboard.IsRepairChecked)
            {
                LoadTripleRoom("Reserved", "Repair");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsCleanChecked)
            {
                LoadTripleRoom("Occupied", "Clean");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsNotCleanChecked)
            {
                LoadTripleRoom("Occupied", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsInProgressChecked)
            {
                LoadTripleRoom("Occupied", "In Progress");

            }
            else if (dashboard != null && dashboard.IsOccupiedChecked && dashboard.IsRepairChecked)
            {
                LoadTripleRoom("Occupied", "Repair");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsCleanChecked)
            {
                LoadTripleRoom("Available", "Clean");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsNotCleanChecked)
            {
                LoadTripleRoom("Available", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsInProgressChecked)
            {
                LoadTripleRoom("Available", "In Progress");

            }
            else if (dashboard != null && dashboard.IsAvailableChecked && dashboard.IsRepairChecked)
            {
                LoadTripleRoom("Available", "Repair");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsCleanChecked)
            {
                LoadTripleRoom("Check Out", "Clean");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsNotCleanChecked)
            {
                LoadTripleRoom("Check Out", "Not Clean");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsInProgressChecked)
            {
                LoadTripleRoom("Check Out", "In Progress");

            }
            else if (dashboard != null && dashboard.IsCheckOutChecked && dashboard.IsRepairChecked)
            {
                LoadTripleRoom("Check Out", "Repair");

            }
            else if (dashboard != null && dashboard.IsReservedChecked)
            {
                LoadTripleRoom("Reserved");
            }
            else if (dashboard != null && dashboard.IsOccupiedChecked)
            {
                LoadTripleRoom("Occupied");
            }
            else if (dashboard != null && dashboard.IsAvailableChecked)
            {
                LoadTripleRoom("Available");
            }
            else if (dashboard != null && dashboard.IsCheckOutChecked)
            {
                LoadTripleRoom("Check Out");
            }
            else if (dashboard != null && dashboard.IsCleanChecked)
            {
                LoadTripleRoom("Clean");
            }
            else if (dashboard != null && dashboard.IsNotCleanChecked)
            {
                LoadTripleRoom("Not Clean");
            }
            else if (dashboard != null && dashboard.IsInProgressChecked)
            {
                LoadTripleRoom("In Progress");
            }
            else if (dashboard != null && dashboard.IsRepairChecked)
            {
                LoadTripleRoom("Repair");
            }
            else
            {
                LoadTripleRoom();
            }
        }
        private void guna2Panel7_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel7_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel7_ControlAdded(object sender, ControlEventArgs e)
        {
            
        }

        private void guna2Panel7_Enter(object sender, EventArgs e)
        {
            int freeroom = 0;
            string connectionString = DatabaseConnection.Connection();
            string query = @"  SELECT COUNT(maphong) as free
                        FROM Room
                        WHERE maphong NOT IN (	SELECT maphong
                              						FROM Bookings)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())  // Kiểm tra nếu có dữ liệu
                        {
                            freeroom = reader.GetInt32(0);
                        }
                    }
                }
            }
            Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
            guna2HtmlLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            guna2HtmlLabel.Text = freeroom.ToString();
            guna2HtmlLabel.Location = new Point(27, 85);
            guna2Panel7.Controls.Add(guna2HtmlLabel);
        }

        private void guna2Panel7_Leave(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel8_Leave(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel9_Leave(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel10_Leave(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel8_Enter(object sender, EventArgs e)
        {
            int cus = 0;
            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT COUNT(cccd_cus) as cus
                                FROM Customer";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cus = reader.GetInt32(0);
                        }
                    }
                }
            }
            Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
            guna2HtmlLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            guna2HtmlLabel.Text = cus.ToString();
            guna2HtmlLabel.Location = new Point(27, 85);
            guna2Panel8.Controls.Add(guna2HtmlLabel);
        }

        private void guna2Panel9_Enter(object sender, EventArgs e)
        {
            int em = 0;
            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT COUNT(cccd_em) as em
                                FROM Employee";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            em = reader.GetInt32(0);
                        }
                    }
                }
            }
            Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
            guna2HtmlLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            guna2HtmlLabel.Text = em.ToString();
            guna2HtmlLabel.Location = new Point(27, 85);
            guna2Panel9.Controls.Add(guna2HtmlLabel);
        }

        private void guna2Panel10_Enter(object sender, EventArgs e)
        {
            int em = 0;
            string connectionString = DatabaseConnection.Connection();
            string query = @"   SELECT COUNT(cccd_em) as em
                                FROM Employee";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            em = reader.GetInt32(0);
                        }
                    }
                }
            }
            Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
            guna2HtmlLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            guna2HtmlLabel.Text = em.ToString();
            guna2HtmlLabel.Location = new Point(27, 85);
            guna2Panel10.Controls.Add(guna2HtmlLabel);
        }


        // Dang sua dung dung vo
        private void guna2Panel5_Click(object sender, EventArgs e)
        {
            guna2Panel5.AutoScroll = true;
            guna2Panel5.HorizontalScroll.Enabled = true;
            guna2Panel5.HorizontalScroll.Visible = true;
            guna2Panel5.Controls.Clear();
            guna2Panel5.Controls.Clear();

            Guna2HtmlLabel guna2HtmlLabel6 = new Guna2HtmlLabel();
            guna2HtmlLabel6.Text = "Check Out";
            guna2HtmlLabel6.Font = new Font("Comic Sans MS", 20, FontStyle.Bold);
            guna2HtmlLabel6.Location = new Point(34, 23);
            guna2Panel5.Controls.Add(guna2HtmlLabel6);

            int spacing = 40;
            int x = 40;
            int y = 120;

            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  SELECT id FROM Checkout";
            string query2 = @"  SELECT sophong 
                                FROM Checkout";
            string query3 = @"  SELECT first_name + ' ' + last_name as fullname
                                FROM Checkout";
            string query4 = @"  SELECT cccd_cus
                                FROM Checkout";
            string query5 = @"  SELECT date_ci
                                FROM Checkout";
            string query6 = @"  SELECT date_co
                                FROM Checkout";
            string query7 = @"  SELECT DATEDIFF(DAY, date_ci, date_co) AS numday
                                FROM Checkout";


            List<int> id = new List<int>();
            List<int> sophong = new List<int>();
            List<string> fullname = new List<string>();
            List<string> cccd = new List<string>();
            List<DateTime> date_ci = new List<DateTime>();
            List<DateTime> date_co = new List<DateTime>();
            List<int> numday = new List<int>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id.Add(reader.GetInt32(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sophong.Add(reader.GetInt32(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query3, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fullname.Add(reader.GetString(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query4, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cccd.Add(reader.GetString(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query5, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            date_ci.Add(reader.GetDateTime(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query6, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            date_co.Add(reader.GetDateTime(0));
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(query7, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            numday.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            for (int i = 0; i < id.Count; i++)
            {
                Guna2Panel childPanel = new Guna2Panel();
                childPanel.Size = new Size(270, 180);
                childPanel.Location = new Point(x, y);
                childPanel.BackColor = Color.White;

                Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
                guna2HtmlLabel.Text = "ID " + id[i];
                guna2HtmlLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel.Location = new Point(10, 10);

                Guna2HtmlLabel guna2HtmlLabel2 = new Guna2HtmlLabel();
                guna2HtmlLabel2.Location = new Point(200, 10);
                guna2HtmlLabel2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel2.Text = "Room" + sophong[i];

                PictureBox pictureBox1 = new PictureBox();
                pictureBox1.BackColor = Color.Transparent;
                Guna2HtmlLabel guna2HtmlLabel1 = new Guna2HtmlLabel();
                Guna2HtmlLabel guna2HtmlLabel3 = new Guna2HtmlLabel();

                Guna2Panel guna2Panel = new Guna2Panel();
                guna2Panel.Height = 50;
                guna2Panel.BackColor = Color.DarkViolet;
                guna2Panel.Dock = DockStyle.Bottom;

                pictureBox1.Image = Properties.Resources.check__5_;
                pictureBox1.Size = new Size(36, 36);
                pictureBox1.Location = new Point(50, 57);
                pictureBox1.BringToFront();
                guna2HtmlLabel1.Font = new Font("Segoe UI", 15, FontStyle.Bold | FontStyle.Italic);
                guna2HtmlLabel1.Text = "Free room";
                guna2HtmlLabel1.Location = new Point(95, 56);
                childPanel.BackgroundImage = Properties.Resources.Untitled_design__1___1_;
                guna2Panel.BackgroundImage = Properties.Resources.wallhaven_0w2wdx_360x50__1_;
                guna2HtmlLabel1.BringToFront();
                guna2HtmlLabel3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel3.Text = "0";
                guna2HtmlLabel3.Location = new Point(50, 10);
                guna2HtmlLabel3.BringToFront();

                pictureBox1.Image = Properties.Resources.user_profile;
                pictureBox1.Size = new Size(36, 36);
                pictureBox1.Location = new Point(50, 57);
                pictureBox1.BringToFront();
                guna2HtmlLabel1.Font = new Font("Segoe UI", 15, FontStyle.Bold | FontStyle.Italic);
                guna2HtmlLabel1.Text = fullname[i];
                guna2HtmlLabel1.Location = new Point(95, 56);
                childPanel.BackgroundImage = Properties.Resources.Untitled_design__4_;
                guna2Panel.BackgroundImage = Properties.Resources.wallhaven_4lxwoq_360x50__1_;
                guna2HtmlLabel1.BringToFront();
                guna2HtmlLabel3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel3.Text = fullname[i].ToString();
                guna2HtmlLabel3.Location = new Point(50, 10);
                guna2HtmlLabel3.BringToFront();



                PictureBox pictureBox2 = new PictureBox();
                pictureBox2.Image = Properties.Resources.spring_calendar;
                pictureBox2.BackColor = Color.Transparent;
                pictureBox2.Size = new Size(36, 36);
                pictureBox2.Location = new Point(3, 3);
                pictureBox2.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel4 = new Guna2HtmlLabel();
                guna2HtmlLabel4.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel4.Text = "days";
                guna2HtmlLabel4.Location = new Point(65, 10);
                guna2HtmlLabel4.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel5 = new Guna2HtmlLabel();
                guna2HtmlLabel5.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                guna2HtmlLabel5.Text = fullname[i];
                guna2HtmlLabel5.Location = new Point(200, 10);
                guna2HtmlLabel5.BringToFront();

                PictureBox pictureBox3 = new PictureBox();
                pictureBox3.BackColor = Color.Transparent;
                pictureBox3.Size = new Size(36, 36);
                pictureBox3.Location = new Point(160, 3);
                pictureBox3.Image = Properties.Resources.check__7_;
                guna2HtmlLabel5.Location = new Point(220, 10);
                pictureBox3.Location = new Point(190, 3);
                pictureBox3.Image = Properties.Resources.x_mark;
                pictureBox3.Image = Properties.Resources.clock__1_;
                pictureBox3.Image = Properties.Resources.hair_dryer;
                pictureBox3.BringToFront();

                guna2Panel.Controls.Add(pictureBox2);
                guna2Panel.Controls.Add(guna2HtmlLabel3);
                guna2Panel.Controls.Add(guna2HtmlLabel4);
                guna2Panel.Controls.Add(guna2HtmlLabel5);
                guna2Panel.Controls.Add(pictureBox3);


                childPanel.Controls.Add(guna2HtmlLabel);
                childPanel.Controls.Add(guna2Panel);
                childPanel.Controls.Add(guna2HtmlLabel2);
                childPanel.Controls.Add(pictureBox1);
                childPanel.Controls.Add(guna2HtmlLabel1);

                guna2Panel5.Controls.Add(childPanel);

                x += childPanel.Width + spacing;

                if ((i + 1) % 2 == 0)
                {
                    x = 20;
                    y += childPanel.Height + spacing;
                }
            }
        }
    }
}
