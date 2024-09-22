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
        public List<Manage> list2 { get; set; }
        public List<Account> list3 { get; set; }
    }
}
