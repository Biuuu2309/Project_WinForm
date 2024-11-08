using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_Project
{
    public class DatabaseConnection
    {
        public static string Connection()
        {
           return "Server=BIUUUBIUUU\\MSSQLSERVER02;Initial Catalog=Hotel_Management;User ID=sa;Password=1201;TrustServerCertificate=True;";
           //return "Server=ZAN\\ZAN;Initial Catalog=Hotel_Management1;User ID=sa;Password=123;TrustServerCertificate=True;";
        }
    }
}

