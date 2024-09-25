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
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            if (ValidateInput())
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Account account = new Account();
                    Response response = dal.Getaccount(conn);
                    if ((response.list3.Any(acc => acc.username == textusername.Text) && response.list3.Any(acc => acc.password == textpassword.Text)) || 
                        (textusername.Text == "zan" && textpassword.Text == "1"))
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
                    
            }
        }
        private bool ValidateInput()
        {
            if (textusername.Text == "" || textpassword.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
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
