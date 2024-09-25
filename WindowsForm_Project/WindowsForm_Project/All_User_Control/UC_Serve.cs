using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm_Project.All_User_Control
{
    public partial class UC_Serve : UserControl
    {
        public UC_Serve()
        {
            InitializeComponent();
        }

        private void UC_Serve_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
        private bool ValidateInput()
        {
            if (txtcccd.Text == "" || txtmaphong.Text == "" || txtotherbooking.Text == "" || txtanuong.Text == "" || txtcallserve.Text == null)
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }
    }
}
