using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
                                            MONTH(date_co) AS month,
                                            YEAR(date_co) AS year
                                        FROM 
                                            Bookings
                                        GROUP BY 
                                            MONTH(date_co), YEAR(date_co)
                                    )
                                    
                                    SELECT 
                                        my.month,
                                        my.year,
                                        COALESCE(SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)), 0) + 
                                        COALESCE(SUM(material_count.material_cost), 0) AS total_booking
                                    FROM 
                                        MonthYear AS my
                                    LEFT JOIN 
                                        Bookings ON MONTH(Bookings.date_co) = my.month AND YEAR(Bookings.date_co) = my.year
                                    LEFT JOIN 
                                        Serve ON Bookings.cccd_cus = Serve.cccd_cus
                                    LEFT JOIN 
                                        (
                                            SELECT 
                                                MONTH(ngay) AS month,
                                                YEAR(ngay) AS year,
                                                COUNT(tennguyenlieu) * 70000 AS material_cost
                                            FROM 
                                                Chitieu
                                            GROUP BY 
                                                MONTH(ngay), YEAR(ngay)
                                        ) AS material_count ON my.month = material_count.month AND my.year = material_count.year
                                    GROUP BY 
                                        my.month, my.year
                                    ORDER BY 
                                        my.year, my.month;";

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
            string query4 = @"  SELECT 
                                COALESCE(SUM(CAST(price AS INT)), 0) + 
                                COALESCE(SUM(CAST(cost AS INT)), 0) + 
                                COALESCE(SUM(material_count.material_cost), 0) AS total_income
                            FROM 
                                Bookings
                            INNER JOIN 
                                Serve ON Bookings.cccd_cus = Serve.cccd_cus
                            LEFT JOIN 
                                (
                                    SELECT 
                                        COUNT(tennguyenlieu) * 70000 AS material_cost
                                    FROM 
                                        Chitieu
                                ) AS material_count ON 1 = 1;";
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
            List<int> month = new List<int>();//
            List<int> total_booking = new List<int>();///
            List<int> month1 = new List<int>();
            List<int> total_chitieu = new List<int>();//
            List<int> year = new List<int>();//
            int income = 0;
            int chitieu = 0;
            int luongnv = 0;
            int loinhuan = 0;//
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
                                    year.Add(reader.GetInt32(1));
                                    total_booking.Add(reader.GetInt32(2));
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

            // Xóa tất cả các series trước khi thêm mới
            chart1.Series.Clear();

            // Series cho Monthly Income (Thu nhập hàng tháng)
            Series salesSeries = new Series("Monthly Income");
            salesSeries.ChartType = SeriesChartType.Spline;
            salesSeries.BorderWidth = 2;
            salesSeries.Color = System.Drawing.Color.Blue;

            // Series cho Monthly Expenses (Chi tiêu hàng tháng)
            Series expensesSeries = new Series("Monthly Expenses");
            expensesSeries.ChartType = SeriesChartType.Spline;
            expensesSeries.BorderWidth = 2;
            expensesSeries.Color = System.Drawing.Color.Red;

            string[] monthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            // Tạo từ điển cho các tháng và giá trị của hai series
            Dictionary<int, int> incomeData = new Dictionary<int, int>();
            Dictionary<int, int> expensesData = new Dictionary<int, int>();

            // Đưa dữ liệu vào từ điển thu nhập
            for (int i = 0; i < month.Count; i++)
            {
                if (i < total_booking.Count)
                {
                    int monthNumber = month[i];
                    if (monthNumber >= 1 && monthNumber <= 12)
                    {
                        incomeData[monthNumber] = total_booking[i];
                    }
                }
            }

            // Đưa dữ liệu vào từ điển chi tiêu
            for (int i = 0; i < month1.Count; i++)
            {
                if (i < total_chitieu.Count)
                {
                    int monthNumber = month1[i];
                    if (monthNumber >= 1 && monthNumber <= 12)
                    {
                        expensesData[monthNumber] = total_chitieu[i];
                    }
                }
            }

            // Thêm điểm dữ liệu vào Series, đồng bộ các tháng
            for (int monthNumber = 1; monthNumber <= 12; monthNumber++)
            {
                string monthName = monthNames[monthNumber - 1];

                int income1 = incomeData.ContainsKey(monthNumber) ? incomeData[monthNumber] : 0;
                int expense = expensesData.ContainsKey(monthNumber) ? expensesData[monthNumber] : 0;

                salesSeries.Points.AddXY(monthName, income1);
                expensesSeries.Points.AddXY(monthName, expense);
            }

            // Thêm series vào chart
            chart1.Series.Add(salesSeries);
            chart1.Series.Add(expensesSeries);

            // Cài đặt tiêu đề và các thông số cho trục
            chart1.Titles.Clear();
            Title title1 = new Title("Month Income Expenses");
            title1.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            chart1.Titles.Add(title1);

            chart1.ChartAreas[0].AxisX.Title = "Month";
            chart1.ChartAreas[0].AxisY.Title = "Amount (VND)";
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Interval = 500;

            // Hiển thị các mốc chia chính trên trục Y
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = true;

            // Hiển thị các mốc chia phụ trên trục Y (nếu muốn)
            chart1.ChartAreas[0].AxisY.MinorTickMark.Enabled = true;

            // Tùy chỉnh các mốc nhãn trên trục Y để dễ đọc
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "N0"; // Định dạng số nguyên
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash; // Đường lưới kiểu gạch ngang
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


            for (int i = 0; i < month.Count; i++)
            {
                Salary salary = new Salary
                {
                    month = month[i],
                    year = year[i],
                    tongthu = total_booking[i],
                    tongchi = total_chitieu[i],
                    loinhuandoanhnghiep = total_booking[i] - total_chitieu[i]
                };
                DAL dal = new DAL();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Response response = dal.AddSalary(salary, conn);
                }
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
            LoadTotal();
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
        private void LoadTotal()
        {
            DAL dal = new DAL();
            string connectionString = DatabaseConnection.Connection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Response response1 = dal.Getsalary(conn);
                if ((response1.list13 != null && response1.list13.Count > 0))
                {
                    guna2DataGridView2.DataSource = null;
                    guna2DataGridView2.DataSource = response1.list13;
                    guna2DataGridView2.ColumnHeadersHeight = 25;
                    guna2DataGridView2.Columns["stt"].HeaderText = "STT";
                    guna2DataGridView2.Columns["month"].HeaderText = "Thang";
                    guna2DataGridView2.Columns["year"].HeaderText = "Nam";
                    guna2DataGridView2.Columns["tongthu"].HeaderText = "Tong thu";
                    guna2DataGridView2.Columns["tongchi"].HeaderText = "Tong chi";
                    guna2DataGridView2.Columns["loinhuandoanhnghiep"].HeaderText = "Loi nhuan doanh nghiep";
                    guna2DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    guna2DataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                    guna2DataGridView2.Refresh(); // Refresh the grid view
                }
                else
                {
                    MessageBox.Show("No data available or " + response1.statusmessage);
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            paneltotal.Visible = !paneltotal.Visible;
            if (paneltotal.Visible)
            {
                guna2Button2.Image = Properties.Resources.logout;
                guna2Button2.Text = "";
            }
            else
            {
                guna2Button2.Text = "Details total";
                guna2Button2.Image = null;
            }
            paneltotal.BringToFront();
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView2_Enter(object sender, EventArgs e)
        {
            
        }

        private void guna2DataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView2_Enter_1(object sender, EventArgs e)
        {
            try
            {
                LoadTotal();
                guna2DataGridView2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int countt = 0;
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string connectionString = DatabaseConnection.Connection();
            string query = @"
                                WITH MonthYear AS (
                                    SELECT 
                                        YEAR(date_co) AS year,
                                        MONTH(date_co) AS month
                                    FROM 
                                        Bookings
                                    GROUP BY 
                                        YEAR(date_co), MONTH(date_co)
                                ),
                                
                                -- Calculate total income from Bookings for each month
                                TotalBookings AS (
                                    SELECT 
                                        my.year,
                                        my.month,
                                        COALESCE(SUM(CAST(price AS INT)) + SUM(CAST(cost AS INT)), 0) AS total_booking
                                    FROM 
                                        MonthYear AS my
                                    LEFT JOIN 
                                        Bookings ON MONTH(Bookings.date_co) = my.month AND YEAR(Bookings.date_co) = my.year
                                    LEFT JOIN 
                                        Serve ON Bookings.cccd_cus = Serve.cccd_cus
                                    GROUP BY 
                                        my.year, my.month
                                ),
                                
                                -- Calculate total expenses from Chitieu for each month
                                TotalChitieu AS (
                                    SELECT 
                                        YEAR(ngay) AS year,
                                        MONTH(ngay) AS month,
                                        SUM(gianhapdogiadung) + SUM(gianhuyeupham) + COUNT(*) * 100000 AS total_chitieu
                                    FROM 
                                        Chitieu
                                    GROUP BY 
                                        YEAR(ngay), MONTH(ngay)
                                )
                                
                                -- Combine total income from Bookings and total expenses from Chitieu
                                SELECT 
                                    COALESCE(tb.year, tc.year) AS year,
                                    COALESCE(tb.month, tc.month) AS month,
                                    COALESCE(tb.total_booking, 0) AS total_booking,
                                    COALESCE(tc.total_chitieu, 0) AS total_chitieu,
                                    COALESCE(tb.total_booking, 0) - COALESCE(tc.total_chitieu, 0) AS net_income
                                FROM 
                                    TotalBookings AS tb
                                FULL OUTER JOIN 
                                    TotalChitieu AS tc ON tb.year = tc.year AND tb.month = tc.month
                                ORDER BY 
                                    year, month;";

            int count = 1;  
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Salary> salaries = new List<Salary>();
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount > 1)
                                {
                                    salaries.Add(new Salary
                                    {
                                        stt = count++,
                                        year = reader.GetInt32(0),
                                        month = reader.GetInt32(1),
                                        tongthu = reader.GetInt32(2),
                                        tongchi = reader.GetInt32(3),
                                        loinhuandoanhnghiep = reader.GetInt32(4)
                                    });
                                }
                            }
                        }
                    }

                    string filePath = $@"E:\App.NET\WindowsForms_Project\Project_WinForm\WindowsForm_Project\File CSV\Salary\Salary_{countt++}.csv";
                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    {
                        sw.WriteLine("STT, Year, Month, Income, Expenses, Profit");
                        foreach (var salary in salaries)
                        {
                            sw.WriteLine($"{salary.stt}, {salary.year}, {salary.month}, {salary.tongthu}, {salary.tongchi}, {salary.loinhuandoanhnghiep}");
                        }
                    }
                    MessageBox.Show("File exported successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Completed file writing.");
                }
            }

        }
    }
}
