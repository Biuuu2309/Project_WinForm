using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_Project.Models
{
    public class Checkout
    {
        public int id { get; set; }
        public string cccd_cus { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int maphong { get; set; }
        public int sophong { get; set; }
        public DateTime date_ci { get; set; }
        public DateTime date_co { get; set; }
    }
}
