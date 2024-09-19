using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WindowsForm_Project
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.Pic8;
            textpassword.KeyDown += new KeyEventHandler(textpassword_KeyDown); // Subscribe to KeyDown event
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void metroPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (textusername.Text == "BiuBiu" && textpassword.Text == "2309")
            {
                errormess.Visible = false;
                Dashboard ds = new Dashboard();
                this.Hide();
                ds.Show();
            } 
            else
            {
                errormess.Visible = true;
                textpassword.Clear();
            }
        }

        private void textpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                guna2Button1_Click(this, new EventArgs()); // Trigger login button click event
            }
        }
    }
}
