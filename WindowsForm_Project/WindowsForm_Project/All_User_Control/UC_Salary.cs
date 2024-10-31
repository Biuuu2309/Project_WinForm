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

            List<string> fullname = new List<string>();
            List<int> counthour = new List<int>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fullname.Add(reader.GetString(0));
                            counthour.Add(Convert.ToInt32(reader[1]));
                        }
                    }
                }
            }

            for (int i = 0; i < fullname.Count; i++)
            {
                Salary.Series.Add(fullname[i]);
                Salary.Series[fullname[i]].Points.AddXY(i, counthour[i]);
            }
            chart1.Series.Clear();

            Series salesSeries = new Series("Monthly Sales");
            salesSeries.ChartType = SeriesChartType.Spline;
            salesSeries.BorderWidth = 2;

            salesSeries.Points.AddXY("January", 1000);
            salesSeries.Points.AddXY("February", 1200);
            salesSeries.Points.AddXY("March", 900);
            salesSeries.Points.AddXY("April", 1400);
            salesSeries.Points.AddXY("May", 1300);
            salesSeries.Points.AddXY("June", 1500);
            salesSeries.Points.AddXY("July", 1700);
            salesSeries.Points.AddXY("August", 1600);
            salesSeries.Points.AddXY("September", 1800);
            salesSeries.Points.AddXY("October", 1900);
            salesSeries.Points.AddXY("November", 2000);
            salesSeries.Points.AddXY("December", 2200);

            Series expensesSeries = new Series("Monthly Expenses");
            expensesSeries.ChartType = SeriesChartType.Spline;
            expensesSeries.BorderWidth = 2;
            expensesSeries.Color = System.Drawing.Color.Red; 

            expensesSeries.Points.AddXY("January", 800);
            expensesSeries.Points.AddXY("February", 900);
            expensesSeries.Points.AddXY("March", 950);
            expensesSeries.Points.AddXY("April", 1000);
            expensesSeries.Points.AddXY("May", 1100);
            expensesSeries.Points.AddXY("June", 1150);
            expensesSeries.Points.AddXY("July", 1200);
            expensesSeries.Points.AddXY("August", 1250);
            expensesSeries.Points.AddXY("September", 1300);
            expensesSeries.Points.AddXY("October", 1350);
            expensesSeries.Points.AddXY("November", 1400);
            expensesSeries.Points.AddXY("December", 1450);

            chart1.Series.Add(salesSeries);
            chart1.Series.Add(expensesSeries);

            chart1.ChartAreas[0].AxisX.Title = "Month";
            chart1.ChartAreas[0].AxisY.Title = "Amount (USD)";
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Interval = 500;
            chart1.ChartAreas[0].RecalculateAxesScale();
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
    }
}
