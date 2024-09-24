using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_Project.Models
{
    public class ManageEmployee
    {
        public string cccd_em { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string sdt { get; set;}
        public string email { get; set; }
        public string gioitinh { get; set; }
        public DateTime ngaysinh { get; set; }
        public float luong { get; set; }
    }
    public class EmployeeWork
    {
        public int maphieu { get; set; }
        public string cccd_em { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime ngay { get; set; }
        public string ca1 { get; set; }
        public string ca2 { get; set; }
        public string ca3 { get; set; }
        public string ca4 { get; set; }
        public string note { get; set; }
        public string tongca { get; set; }
    }
    public class Total
    {
        public string cccd_em { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int tongngay { get; set; }
        public int tongca { get; set; }
        public float luong { get; set; }
        public float total { get; set; }
    }
}
