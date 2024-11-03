using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_Project.Models
{
    public class Response
    {
        public string statusmessage { get; set; }
        public List<Room> list { get; set; }
        public List<Customer> list1 { get; set; }
        public List<ManageEmployee> list2 { get; set; }
        public List<Account> list3 { get; set; }
        public List<EmployeeWork> list4 { get; set; }
        public List<Total> list5 { get; set; }
        public List<RoomUpdate> list6 { get; set; }
        public List<Serve> list7 { get; set; }
        public List<Report> list8 { get; set; }
        public List<Bookings> list9 { get; set; }
        public List<DetailRoom> list10 { get; set; }
        public List<Checkout> list11 { get; set; }
        public List<Chitieu> list12 { get; set; }

    }
}
