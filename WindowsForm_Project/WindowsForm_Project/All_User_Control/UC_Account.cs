using Microsoft.Identity.Client;
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

namespace WindowsForm_Project.All_User_Control
{
    public partial class UC_Account : UserControl
    {
        public UC_Account()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }
        private bool ValidateInput()
        {
            if (txtusername.Text == "" || txtcccd_em.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Account account = new Account
                {
                    username = txtusername.Text,
                    cccd_em = txtcccd_em.Text,
                    password = txtpassword.Text,
                };

                DAL dal = new DAL();
                string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
                //string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addaccount(account, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }
        private void RefreshControl()
        {
            clearAll();
            LoadRoomData();
        }
        public void clearAll()
        {
            txtusername.Clear();
            txtcccd_em.Clear();
            txtpassword.Clear();
            txtid.Clear();
        }
        private void LoadRoomData()
        {
            DAL dal = new DAL();
            string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
            //string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getaccount(conn);
                if ((response.list3 != null && response.list3.Count > 0))
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list3;
                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No account data available or " + response.statusmessage);
                }
            }
        }
        private void UC_Account_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
        private void UC_Account_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadRoomData();
                DataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (ValidateInputDelete())
            {
                Account account = new Account
                {
                    id = int.Parse(txtid.Text),
                };

                DAL dal = new DAL();
                string connectionString = "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
                //string connectionString = "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management;User ID=sa;Password=123;TrustServerCertificate=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Deleteaccount(account, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }
        private bool ValidateInputDelete()
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
