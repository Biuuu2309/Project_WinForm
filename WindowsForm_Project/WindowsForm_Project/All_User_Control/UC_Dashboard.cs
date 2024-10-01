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
            string query1 = @"SELECT roomnumber FROM Room WHERE numbed = 1"; 
            string query2 = @"  SELECT status_room 
                                FROM Update_room
                                INNER JOIN Room ON Update_room.maphong = Room.maphong
                                WHERE Room.numbed = 1"; 

            List<int> roomnumber = new List<int>(); 
            List<string> statusroom = new List<string>();

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
                if (statusroom[i] == "Available")
                {
                    pictureBox1.Image = Properties.Resources;
                }

                Guna2Panel guna2Panel = new Guna2Panel();
                guna2Panel.Height = 40;
                guna2Panel.BackColor = Color.DarkViolet;
                guna2Panel.Dock = DockStyle.Bottom;

                childPanel.Controls.Add(guna2HtmlLabel);
                childPanel.Controls.Add(guna2Panel);
                childPanel.Controls.Add(guna2HtmlLabel2);

                guna2Panel1.Controls.Add(childPanel);

                x += childPanel.Width + spacing;
            }
        }

    }
}
