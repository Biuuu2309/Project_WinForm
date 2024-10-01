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

        private void guna2Panel1_Click(object sender, EventArgs e)
        {
            guna2Panel1.AutoScroll = true;
            guna2Panel1.HorizontalScroll.Enabled = true;
            guna2Panel1.HorizontalScroll.Visible = true;
            guna2Panel1.Controls.Clear();
            guna2Panel1.Controls.Clear();

            int spacing = 20;
            int x = 20;
            int y = 20;

            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  SELECT roomnumber FROM Room WHERE numbed = 1"; 
            string query2 = @"  SELECT status_room 
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE Room.numbed = 1";
            string query3 = @"  SELECT first_name + ' ' + last_name as fullname
                                FROM Customer
                                INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
                                WHERE numbed = 1";
            string query4 = @"  SELECT DATEDIFF(DAY, date_ci, date_co) AS demngay
                                FROM Bookings 
                                WHERE numbed = 1";
            string query5 = @"  SELECT house_keeping
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE numbed = 1";
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
                childPanel.Size = new Size(250, 180);
                childPanel.Location = new Point(x, y);
                childPanel.BackColor = Color.White;

                Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
                guna2HtmlLabel.Text = "Room " + roomnumber[i]; 
                guna2HtmlLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                guna2HtmlLabel.Location = new Point(10, 10);
                childPanel.BackColor = Color.Green;


                Guna2HtmlLabel guna2HtmlLabel2 = new Guna2HtmlLabel();
                guna2HtmlLabel2.Location = new Point(180, 10);
                guna2HtmlLabel2.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                guna2HtmlLabel2.Text = statusroom[i];


                PictureBox pictureBox1 = new PictureBox(); 
                Guna2HtmlLabel guna2HtmlLabel1 = new Guna2HtmlLabel();
                Guna2HtmlLabel guna2HtmlLabel3 = new Guna2HtmlLabel();

                if (statusroom[i] == "Available")
                {
                    pictureBox1.Image = Properties.Resources.check__5_;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(46, 52);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Arial", 15, FontStyle.Regular);
                    guna2HtmlLabel1.Text = "Free room";
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Arial", 10, FontStyle.Regular);
                    guna2HtmlLabel3.Text = "0";
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();
                    
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.user_profile;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(46, 52);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Arial", 15, FontStyle.Regular);
                    guna2HtmlLabel1.Text = fullname[i];
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Arial", 10, FontStyle.Regular);
                    guna2HtmlLabel3.Text = demngay[i].ToString();
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();
                }

                Guna2Panel guna2Panel = new Guna2Panel();
                guna2Panel.Height = 40;
                guna2Panel.BackColor = Color.Red;
                guna2Panel.Dock = DockStyle.Bottom;

                PictureBox pictureBox2 = new PictureBox();
                pictureBox2.Image = Properties.Resources.spring_calendar;
                pictureBox2.Size = new Size(36, 36);
                pictureBox2.Location = new Point(3, 3);
                pictureBox2.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel4 = new Guna2HtmlLabel();
                guna2HtmlLabel4.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel4.Text = "days";
                guna2HtmlLabel4.Location = new Point(65, 10);
                guna2HtmlLabel4.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel5 = new Guna2HtmlLabel();
                guna2HtmlLabel5.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel5.Text = housekeeping[i];
                guna2HtmlLabel5.Location = new Point(200, 10);
                guna2HtmlLabel5.BringToFront();

                PictureBox pictureBox3 = new PictureBox();
                if (housekeeping[i] == "Clean")
                {
                    pictureBox3.Image = Properties.Resources.check__7_;
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
                pictureBox3.Size = new Size(36, 36);
                pictureBox3.Location = new Point(160, 3);
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

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Click(object sender, EventArgs e)
        {
            guna2Panel2.AutoScroll = true;
            guna2Panel2.HorizontalScroll.Enabled = true;
            guna2Panel2.HorizontalScroll.Visible = true;
            guna2Panel2.Controls.Clear();
            guna2Panel2.Controls.Clear();

            int spacing = 20;
            int x = 20;
            int y = 20;

            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  SELECT roomnumber FROM Room WHERE numbed = 2";
            string query2 = @"  SELECT status_room 
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE Room.numbed = 2";
            string query3 = @"  SELECT first_name + ' ' + last_name as fullname
                                FROM Customer
                                INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
                                WHERE numbed = 2";
            string query4 = @"  SELECT DATEDIFF(DAY, date_ci, date_co) AS demngay
                                FROM Bookings 
                                WHERE numbed = 2";
            string query5 = @"  SELECT house_keeping
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE numbed = 2";
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
                childPanel.Size = new Size(250, 180);
                childPanel.Location = new Point(x, y);
                childPanel.BackColor = Color.White;

                Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
                guna2HtmlLabel.Text = "Room " + roomnumber[i];
                guna2HtmlLabel.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel.Location = new Point(0, 0);

                Guna2HtmlLabel guna2HtmlLabel2 = new Guna2HtmlLabel();
                guna2HtmlLabel2.Location = new Point(180, 0);
                guna2HtmlLabel2.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel2.Text = statusroom[i];

                PictureBox pictureBox1 = new PictureBox();
                Guna2HtmlLabel guna2HtmlLabel1 = new Guna2HtmlLabel();
                Guna2HtmlLabel guna2HtmlLabel3 = new Guna2HtmlLabel();

                if (statusroom[i] == "Available")
                {
                    pictureBox1.Image = Properties.Resources.check__5_;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(46, 52);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Arial", 15, FontStyle.Regular);
                    guna2HtmlLabel1.Text = "Free room";
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Arial", 10, FontStyle.Regular);
                    guna2HtmlLabel3.Text = "0";
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();

                }
                else
                {
                    pictureBox1.Image = Properties.Resources.user_profile;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(46, 52);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Arial", 15, FontStyle.Regular);
                    guna2HtmlLabel1.Text = fullname[i];
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Arial", 10, FontStyle.Regular);
                    guna2HtmlLabel3.Text = demngay[i].ToString();
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();
                }

                Guna2Panel guna2Panel = new Guna2Panel();
                guna2Panel.Height = 40;
                guna2Panel.BackColor = Color.DarkViolet;
                guna2Panel.Dock = DockStyle.Bottom;

                PictureBox pictureBox2 = new PictureBox();
                pictureBox2.Image = Properties.Resources.spring_calendar;
                pictureBox2.Size = new Size(36, 36);
                pictureBox2.Location = new Point(3, 3);
                pictureBox2.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel4 = new Guna2HtmlLabel();
                guna2HtmlLabel4.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel4.Text = "days";
                guna2HtmlLabel4.Location = new Point(65, 10);
                guna2HtmlLabel4.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel5 = new Guna2HtmlLabel();
                guna2HtmlLabel5.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel5.Text = housekeeping[i];
                guna2HtmlLabel5.Location = new Point(200, 10);
                guna2HtmlLabel5.BringToFront();

                PictureBox pictureBox3 = new PictureBox();
                if (housekeeping[i] == "Clean")
                {
                    pictureBox3.Image = Properties.Resources.check__7_;
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
                pictureBox3.Size = new Size(36, 36);
                pictureBox3.Location = new Point(160, 3);
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

        private void guna2Panel3_Click(object sender, EventArgs e)
        {
            guna2Panel3.AutoScroll = true;
            guna2Panel3.HorizontalScroll.Enabled = true;
            guna2Panel3.HorizontalScroll.Visible = true;
            guna2Panel3.Controls.Clear();
            guna2Panel3.Controls.Clear();

            int spacing = 20;
            int x = 20;
            int y = 20;

            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  SELECT roomnumber FROM Room WHERE numbed = 3";
            string query2 = @"  SELECT status_room 
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE Room.numbed = 3";
            string query3 = @"  SELECT first_name + ' ' + last_name as fullname
                                FROM Customer
                                INNER JOIN Bookings ON Customer.cccd_cus = Bookings.cccd_cus
                                WHERE numbed = 3";
            string query4 = @"  SELECT DATEDIFF(DAY, date_ci, date_co) AS demngay
                                FROM Bookings 
                                WHERE numbed = 3";
            string query5 = @"  SELECT house_keeping
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE numbed = 3";
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
                childPanel.Size = new Size(250, 180);
                childPanel.Location = new Point(x, y);
                childPanel.BackColor = Color.White;

                Guna2HtmlLabel guna2HtmlLabel = new Guna2HtmlLabel();
                guna2HtmlLabel.Text = "Room " + roomnumber[i];
                guna2HtmlLabel.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel.Location = new Point(0, 0);

                Guna2HtmlLabel guna2HtmlLabel2 = new Guna2HtmlLabel();
                guna2HtmlLabel2.Location = new Point(180, 0);
                guna2HtmlLabel2.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel2.Text = statusroom[i];

                PictureBox pictureBox1 = new PictureBox();
                Guna2HtmlLabel guna2HtmlLabel1 = new Guna2HtmlLabel();
                Guna2HtmlLabel guna2HtmlLabel3 = new Guna2HtmlLabel();

                if (statusroom[i] == "Available")
                {
                    pictureBox1.Image = Properties.Resources.check__5_;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(46, 52);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Arial", 15, FontStyle.Regular);
                    guna2HtmlLabel1.Text = "Free room";
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Arial", 10, FontStyle.Regular);
                    guna2HtmlLabel3.Text = "0";
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();

                }
                else
                {
                    pictureBox1.Image = Properties.Resources.user_profile;
                    pictureBox1.Size = new Size(36, 36);
                    pictureBox1.Location = new Point(46, 52);
                    pictureBox1.BringToFront();
                    guna2HtmlLabel1.Font = new Font("Arial", 15, FontStyle.Regular);
                    guna2HtmlLabel1.Text = fullname[i];
                    guna2HtmlLabel1.Location = new Point(95, 56);
                    guna2HtmlLabel1.BringToFront();
                    guna2HtmlLabel3.Font = new Font("Arial", 10, FontStyle.Regular);
                    guna2HtmlLabel3.Text = demngay[i].ToString();
                    guna2HtmlLabel3.Location = new Point(50, 10);
                    guna2HtmlLabel3.BringToFront();
                }

                Guna2Panel guna2Panel = new Guna2Panel();
                guna2Panel.Height = 40;
                guna2Panel.BackColor = Color.DarkViolet;
                guna2Panel.Dock = DockStyle.Bottom;

                PictureBox pictureBox2 = new PictureBox();
                pictureBox2.Image = Properties.Resources.spring_calendar;
                pictureBox2.Size = new Size(36, 36);
                pictureBox2.Location = new Point(3, 3);
                pictureBox2.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel4 = new Guna2HtmlLabel();
                guna2HtmlLabel4.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel4.Text = "days";
                guna2HtmlLabel4.Location = new Point(65, 10);
                guna2HtmlLabel4.BringToFront();

                Guna2HtmlLabel guna2HtmlLabel5 = new Guna2HtmlLabel();
                guna2HtmlLabel5.Font = new Font("Arial", 10, FontStyle.Regular);
                guna2HtmlLabel5.Text = housekeeping[i];
                guna2HtmlLabel5.Location = new Point(200, 10);
                guna2HtmlLabel5.BringToFront();

                PictureBox pictureBox3 = new PictureBox();
                if (housekeeping[i] == "Clean")
                {
                    pictureBox3.Image = Properties.Resources.check__7_;
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
                pictureBox3.Size = new Size(36, 36);
                pictureBox3.Location = new Point(160, 3);
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
            }
        }
    }
}
