using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            // Đầu tiên, bạn cần có một Panel (ví dụ: panel1)
            guna2Panel1.AutoScroll = true;  // Bật tính năng cuộn tự động
            guna2Panel1.HorizontalScroll.Enabled = true;  // Bật thanh cuộn ngang
            guna2Panel1.HorizontalScroll.Visible = true;  // Hiển thị thanh cuộn ngang

            // Tạo một số Control có kích thước lớn hơn chiều rộng của Panel
            Guna2Panel guna2Panel = new Guna2Panel();

            // Thêm button vào Panel
            int x = 20, y = 65;
            List<Guna2Panel> list = new List<Guna2Panel>();
            for (int i = 0; i < 10; i++)
            {
                guna2Panel.Size = new Size(210, 155);
                guna2Panel.Location = new Point(x, y);
                guna2Panel.BackColor = Color.FromArgb(255, 0, 0, 0);
                x = x + 230;
                list.Add(guna2Panel);
            }
            foreach (Guna2Panel panel in list)
            {
                panel.BringToFront();
                guna2Panel1.Controls.Add(panel);
            }
        }
    }
}
