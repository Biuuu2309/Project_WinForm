using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm_Project
{
    public partial class LoginFormNew : Form
    {
        public LoginFormNew()
        {
            InitializeComponent();
            this.textusername.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
            this.textpassword.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (textusername.Text == "BiuBiu" && textpassword.Text == "2309" || textusername.Text == "zan" && textpassword.Text == "1")
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
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                guna2Button1_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void textusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void textpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void errormess_Click(object sender, EventArgs e)
        {

        }
    }
}
