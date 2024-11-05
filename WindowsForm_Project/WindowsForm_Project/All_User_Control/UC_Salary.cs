using Guna.UI2.WinForms;
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
using System.Windows.Forms.DataVisualization.Charting;
using WindowsForm_Project.Models;

namespace WindowsForm_Project.All_User_Control
{
    public partial class UC_Salary : UserControl
    {
        public UC_Salary()
        {
            InitializeComponent();
        }

        private void UC_Tongthu_Load(object sender, EventArgs e)
        {
            string connectionString = DatabaseConnection.Connection();
            string query1 = @"  
                                SELECT DISTINCT first_name + ' ' + last_name as full_name, 
                                       CAST(luong * COUNT(*) AS INT) AS total_salary
                                FROM Employee
                                INNER JOIN Chamcong ON Employee.cccd_em = Chamcong.cccd_em
                                WHERE Employee.cccd_em = Chamcong.cccd_em AND (ca1 = 'Co' OR ca2 = 'Co' OR ca3 = 'Co' OR ca4 = 'Co')
                                GROUP BY Employee.cccd_em, first_name, last_name, luong";


            string query2 = @"  
                                	WITH MonthYear AS (
                                        SELECT 
                                            MONTH(date_co) AS month
                                        FROM 
                                            Bookings
                                        GROUP BY 
                                            MONTH(date_co)
                                    )
                                    
                                    SELECT 
                                        my.month,
                                        COALESCE(SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)), 0) AS total_booking
                                    FROM 
                                        MonthYear AS my
                                    LEFT JOIN 
                                        Bookings ON MONTH(Bookings.date_co) = my.month
                                    LEFT JOIN 
                                        Serve ON Bookings.cccd_cus = Serve.cccd_cus
                                    GROUP BY 
                                        my.month
                                    ORDER BY 
                                        my.month;";

            string query3 = @"  	SELECT 
                                        month,
                                        SUM(total_chitieu + grand_total_salary) AS total_costs
                                    FROM 
                                        (
                                            -- Truy vấn chi tiêu
                                            SELECT 
                                                MONTH(ngay) AS month,
                                                SUM(CAST(gianhapdogiadung AS INT)) + SUM(CAST(gianhuyeupham AS INT)) AS total_chitieu,
                                                0 AS grand_total_salary
                                            FROM 
                                                Chitieu
                                            GROUP BY 
                                                MONTH(ngay)
                                    
                                            UNION ALL
                                            
                                            -- Truy vấn lương nhân viên
                                            SELECT 
                                                MONTH(ngay) AS month,
                                                0 AS total_chitieu,
                                                SUM(CAST(luong AS INT) * diem_danh.so_ca) AS grand_total_salary
                                            FROM 
                                                Employee
                                            INNER JOIN 
                                                Chamcong ON Employee.cccd_em = Chamcong.cccd_em
                                            CROSS APPLY 
                                                (
                                                    SELECT 
                                                        CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END +
                                                        CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END +
                                                        CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END +
                                                        CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END AS so_ca
                                                ) AS diem_danh
                                            GROUP BY 
                                                MONTH(ngay), Employee.cccd_em
                                        ) AS combined
                                    GROUP BY 
                                        month
                                    ORDER BY 
                                        month;";
            string query4 = @"  SELECT SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)) AS total_booking
                                FROM Bookings
                                INNER JOIN Serve ON Bookings.cccd_cus = Serve.cccd_cus";
            string query5 = @"  SELECT SUM(CAST(gianhapdogiadung AS INT)) + SUM(CAST(gianhuyeupham AS INT)) + SUM(CAST(gianhapnguyenlieu AS INT)) AS total_chitieu FROM Chitieu";
            string query6 = @"  SELECT SUM(CAST(total_salary AS INT)) AS grand_total_salary
                                FROM (
                                    SELECT 
                                        SUM(CAST(luong AS INT) * diem_danh.so_ca) AS total_salary
                                    FROM 
                                        Employee
                                    INNER JOIN 
                                        Chamcong ON Employee.cccd_em = Chamcong.cccd_em
                                    CROSS APPLY 
                                        (
                                            SELECT 
                                                CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END +
                                                CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END +
                                                CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END +
                                                CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END AS so_ca
                                        ) AS diem_danh
                                    GROUP BY 
                                        Employee.cccd_em, first_name, last_name, luong
                                ) AS per_employee_salary;";
            string query7 = @"  SELECT 
                                (SELECT SUM(CAST(total_booking AS INT)) 
                                 FROM (
                                     SELECT SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)) AS total_booking
                                     FROM Bookings
                                     INNER JOIN Serve ON Bookings.cccd_cus = Serve.cccd_cus
                                 ) AS total_bk) 
                                -
                                (SELECT SUM(CAST(total_chitieu AS INT)) 
                                 FROM (
                                     SELECT SUM(CAST(gianhapdogiadung AS INT)) + SUM(CAST(gianhuyeupham AS INT)) AS total_chitieu
                                     FROM Chitieu
                                 ) AS total_ct) 
	                            -
	                            (SELECT SUM(CAST(total_salary AS INT)) AS grand_total_salary
	                            FROM (
	                                SELECT 
	                                    SUM(CAST(luong AS INT) * diem_danh.so_ca) AS total_salary
	                                FROM 
	                                    Employee
	                                INNER JOIN 
	                                    Chamcong ON Employee.cccd_em = Chamcong.cccd_em
	                                CROSS APPLY 
	                                    (
	                                        SELECT 
	                                            CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END +
	                                            CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END +
	                                            CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END +
	                                            CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END AS so_ca
	                                    ) AS diem_danh
	                                GROUP BY 
	                                    Employee.cccd_em, first_name, last_name, luong
	                            ) AS per_employee_salary);";

            List<string> fullname = new List<string>();
            List<int> counthour = new List<int>();
            List<int> month = new List<int>();
            List<int> total_booking = new List<int>();
            List<int> month1 = new List<int>();
            List<int> total_chitieu = new List<int>();
            int income = 0;
            int chitieu = 0;
            int luongnv = 0;
            int loinhuan = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1) 
                                {
                                    fullname.Add(reader.GetString(0));
                                    counthour.Add(reader.GetInt32(1));
                                }
                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1) 
                                {
                                    month.Add(reader.GetInt32(0));
                                    total_booking.Add(reader.GetInt32(1));
                                }
                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand(query3, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1) 
                                {
                                    month1.Add(reader.GetInt32(0));
                                    total_chitieu.Add(reader.GetInt32(1));
                                }
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand(query4, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                income = reader.GetInt32(0);
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand(query5, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                chitieu = reader.GetInt32(0);
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand(query6, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                luongnv = reader.GetInt32(0);
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand(query7, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                loinhuan = reader.GetInt32(0);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Title title = new Title("Salary Employee");
            title.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            Salary.Titles.Add(title);

            for (int i = 0; i < fullname.Count; i++)
            {
                Salary.Series.Add(fullname[i]);
                Salary.Series[fullname[i]].Points.AddXY(i, counthour[i]);
            }

            chart1.Series.Clear();

            Series salesSeries = new Series("Monthly Income");
            salesSeries.ChartType = SeriesChartType.Spline;
            salesSeries.BorderWidth = 2;

            string[] monthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            for (int i = 0; i < month.Count; i++)
            {
                if (i < total_booking.Count)
                {
                    int monthNumber = month[i];
                    if (monthNumber >= 1 && monthNumber <= 12)
                    {
                        salesSeries.Points.AddXY(monthNames[monthNumber - 1], total_booking[i]);
                    }
                }
            }

            chart1.Series.Add(salesSeries);

            Series expensesSeries = new Series("Monthly Expenses");
            expensesSeries.ChartType = SeriesChartType.Spline;
            expensesSeries.BorderWidth = 2;
            expensesSeries.Color = System.Drawing.Color.Red;

            for (int i = 0; i < month1.Count; i++)
            {
                if (i < total_chitieu.Count)
                {
                    int monthNumber = month1[i];
                    if (monthNumber >= 1 && monthNumber <= 12)
                    {
                        expensesSeries.Points.AddXY(monthNames[monthNumber - 1], total_chitieu[i]);
                    }
                }
            }
            Title title1 = new Title("Month Income Expenses");
            title1.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);

            chart1.Series.Add(expensesSeries);
            chart1.Titles.Add(title1);
            chart1.ChartAreas[0].AxisX.Title = "Month";
            chart1.ChartAreas[0].AxisY.Title = "Amount (VND)";
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Interval = 500;
            chart1.ChartAreas[0].RecalculateAxesScale();


            Title title2 = new Title("Total Income Expenses Breakdown");
            title2.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            chart2.Series.Clear();
            Series doughnutSeries = new Series("Income Expenses Breakdown");
            doughnutSeries.ChartType = SeriesChartType.Doughnut;
            doughnutSeries.BorderWidth = 2; 
            doughnutSeries.Points.AddXY("Income", income);
            doughnutSeries.Points.AddXY("Expenses", chitieu);
            doughnutSeries.Points.AddXY("Employee Salary", luongnv);
            doughnutSeries.Points.AddXY("Profit", loinhuan);
            chart2.Series.Add(doughnutSeries);
            chart2.ChartAreas[0].Area3DStyle.Enable3D = true; 
            chart2.ChartAreas[0].BackColor = System.Drawing.Color.Transparent; 
            chart2.Legends[0].Enabled = true;
            chart2.Titles.Add(title2);
            doughnutSeries["PieLabelStyle"] = "Outside";
            doughnutSeries["DoughnutRadius"] = "60"; 
            foreach (DataPoint point in doughnutSeries.Points)
            {
                point.Label = $"{point.AxisLabel}: #PERCENT";
            }

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Chitieu tongchi = new Chitieu
                {
                    ngay = txtdate.Value,
                    tendogiadung = txtdgd.Text,
                    gianhapdogiadung = int.Parse(txtgianhapdgd.Text),
                    tennguyenlieu = txtnvl.Text,
                    gianhapnguyenlieu = int.Parse(txtgianhapnvl.Text),
                    tennhuyeupham = txtgianhapnyp.Text,
                    gianhuyeupham = int.Parse(txtgianhapnyp.Text),
                };

                DAL dal = new DAL();
                string connectionString = DatabaseConnection.Connection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.AddTongchi(tongchi, conn);
                    MessageBox.Show(response.statusmessage);
                    if (response.statusmessage.Contains("Successfully"))
                    {
                        RefreshControl();
                    }
                }
            }
        }
        private bool ValidateInput()
        {
            if (txtdgd.Text == "" || txtgianhapdgd.Text == "" || txtnvl.Text == "" || txtgianhapnvl.Text == "" || txtgianhapnvl.Text == "" || txtnyp.Text == "" || txtgianhapnyp.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }
            return true;
        }
        private void RefreshControl()
        {
            clearAll();
            LoadTongChi();
        }
        public void clearAll()
        {
            txtdate.Value = DateTime.Now;
            txtdgd.Clear();
            txtgianhapdgd.Clear();
            txtnvl.Clear();
            txtgianhapnvl.Clear();
            txtnyp.Clear();
            txtgianhapnyp.Clear();

        }
        private void LoadTongChi()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response = dal.Gettongchi(conn);
                if ((response.list12 != null && response.list12.Count > 0))
                {
                    guna2DataGridView1.DataSource = null; 
                    guna2DataGridView1.DataSource = response.list12;
                    guna2DataGridView1.ColumnHeadersHeight = 25;
                    guna2DataGridView1.Columns["sttchi"].HeaderText = "STT";
                    guna2DataGridView1.Columns["date"].HeaderText = "Date";
                    guna2DataGridView1.Columns["tendogiadung"].HeaderText = "Ten do gia dung";
                    guna2DataGridView1.Columns["gianhapdogiadung"].HeaderText = "Gia nhap do gia dung";
                    guna2DataGridView1.Columns["tennguyenvatlieu"].HeaderText = "Ten nguyen vat lieu";
                    guna2DataGridView1.Columns["gianhapnguyenvatlieu"].HeaderText = "Gia nhap nguyen vat lieu";
                    guna2DataGridView1.Columns["tennhuyeupham"].HeaderText = "Ten nhu yeu pham";
                    guna2DataGridView1.Columns["gianhapnhuyeupham"].HeaderText = "Gia nhap nhu yeu pham";
                    guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    guna2DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Both;

                    guna2DataGridView1.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response.statusmessage);
                }
            }
        }

        private void guna2Panel5_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadTongChi();
                guna2DataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2Panel5.Visible = !guna2Panel5.Visible;
            if (guna2Panel5.Visible)
            {
                guna2Button4.Image = Properties.Resources.logout;
                guna2Button4.Text = "";
            }
            else
            {
                guna2Button4.Text = "Details";
                guna2Button4.Image = null;
            }
            guna2Panel5.BringToFront();
        }
    }
}
