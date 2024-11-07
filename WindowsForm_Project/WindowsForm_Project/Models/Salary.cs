using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_Project.Models
{
    public class Salary
    {
        public int stt { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int tongthu { get; set; }
        public int tongchi { get; set; }
        public int loinhuandoanhnghiep { get; set; }
    }
    public class Chitieu
    {
        public int sttchi { get; set; }
        public DateTime ngay { get; set; }
        public string tendogiadung { get; set; }
        public int gianhapdogiadung { get; set; }
        public string tennguyenlieu { get; set; }
        public int gianhapnguyenlieu { get; set; }
        public string tennhuyeupham { get; set; }
        public int gianhuyeupham { get; set; }
    }
}
