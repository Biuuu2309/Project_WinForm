using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_Project.Models
{
    public class Room
    {
        public int maphong {  get; set; }
        public int roomnumber { get; set; }
        public string roomtype { get; set; }
        public string numbed { get; set; }
        public string view_room { get; set; }
        public string image_room { get; set; }
        public int price { get; set; }
    }
    public class RoomUpdate
    {
        public int maphong { get; set; }
        public int roomnumber { get; set; }
        public string status_room { get; set; }
        public string house_keeping { get; set; }
    }
    public class DetailRoom : Room
    {
        public string status_room { get; set; }
        public string house_keeping { get; set; }
    }
}
