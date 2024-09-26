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
    public partial class UC_Report : UserControl
    {
        public UC_Report()
        {
            InitializeComponent();
            this.Leave += new EventHandler(UC_Report_Leave);
            this.Enter += new EventHandler(UC_Report_Enter);
        }

        private void Report_Load(object sender, EventArgs e)
        {
            this.DataGridView1.DefaultCellStyle.ForeColor = Color.Black;
        }
        private bool ValidateInput()
        {
            if (txtcccd_cus.Text == "" || txtmaphong.Text == "" || txtghichu.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Report report = new Report
                {
                    cccd_cus = txtcccd_cus.Text,
                    maphong = int.Parse(txtmaphong.Text),
                    ghichu = txtghichu.Text
                };

                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.Addreport(report, conn);
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
            LoadReportData();
        }
        public void clearAll()
        {
            txtcccd_cus.Clear();
            txtmaphong.Clear();
            txtghichu.Clear();
        }
        private void UC_Report_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
        private void UC_Report_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadReportData();
                DataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadReportData()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Getreport(conn);
                if ((response.list8 != null && response.list8.Count > 0))
                {
                    DataGridView1.DataSource = null; // Clear previous data
                    DataGridView1.DataSource = response.list8;
                    DataGridView1.ColumnHeadersHeight = 25;
                    DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
