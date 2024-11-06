using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_Project.Models
{
    public class DAL
    {
        public Response Addroom(Room room, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageroom = Path.GetFileName(room.image_room);
                SqlCommand cmd = new SqlCommand("sp_addroom", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maphong", room.maphong);
                cmd.Parameters.AddWithValue("@roomnumber", room.roomnumber);
                cmd.Parameters.AddWithValue("@roomtype", room.roomtype);
                cmd.Parameters.AddWithValue("@numbed", room.numbed);
                cmd.Parameters.AddWithValue("@view_room", room.view_room);
                cmd.Parameters.AddWithValue("@image_room", imageroom);
                cmd.Parameters.AddWithValue("@price", room.price);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Gettongchi(SqlConnection conn)
        {
            Response response = new Response();
            List<Chitieu> list = new List<Chitieu>();
            try
            {
                string query = @"SELECT * FROM Chitieu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Chitieu tongchi = new Chitieu
                            {
                                sttchi = int.Parse(reader["sttchi"].ToString()),
                                ngay = DateTime.Parse(reader["ngay"].ToString()),
                                tendogiadung = reader["tendogiadung"].ToString(),
                                gianhapdogiadung = int.Parse(reader["gianhapdogiadung"].ToString()),
                                tennguyenlieu = reader["tennguyenlieu"].ToString(),
                                gianhapnguyenlieu = int.Parse(reader["gianhapnguyenlieu"].ToString()),
                                tennhuyeupham = reader["tennhuyeupham"].ToString(),
                                gianhuyeupham = int.Parse(reader["gianhuyeupham"].ToString())
                            };
                            list.Add(tongchi);
                        }
                    }
                }
                response.list12 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response AddTongchi(Chitieu tongchi, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addtongchi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ngay", tongchi.ngay);
                cmd.Parameters.AddWithValue("@tendogiadung", tongchi.tendogiadung);
                cmd.Parameters.AddWithValue("@gianhapdogiadung", tongchi.gianhapdogiadung);
                cmd.Parameters.AddWithValue("@tennguyenlieu", tongchi.tennguyenlieu);
                cmd.Parameters.AddWithValue("@gianhapnguyenlieu", tongchi.gianhapnguyenlieu);
                cmd.Parameters.AddWithValue("@tennhuyeupham", tongchi.tennhuyeupham);
                cmd.Parameters.AddWithValue("@gianhuyeupham", tongchi.gianhuyeupham);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Addreport(Report report, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatereport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cccd_cus", report.cccd_cus);
                cmd.Parameters.AddWithValue("@maphong", report.maphong);
                cmd.Parameters.AddWithValue("@ghichu", report.ghichu);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Addemployeework(EmployeeWork chamcong, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addchamcong", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cccd_em", chamcong.cccd_em);
                cmd.Parameters.AddWithValue("@ngay", chamcong.ngay);
                cmd.Parameters.AddWithValue("@ca1", chamcong.ca1);
                cmd.Parameters.AddWithValue("@ca2", chamcong.ca2);
                cmd.Parameters.AddWithValue("@ca3", chamcong.ca3);
                cmd.Parameters.AddWithValue("@ca4", chamcong.ca4);
                cmd.Parameters.AddWithValue("@note", chamcong.note);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Addbooking(Bookings booking, SqlConnection conn)
        {
            Response response = new Response();
            try
            {   
                SqlCommand cmd = new SqlCommand("sp_addbooking", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cccd_cus", booking.cccd_cus);
                cmd.Parameters.AddWithValue("@status_room", booking.status_room);
                cmd.Parameters.AddWithValue("@house_keeping", booking.house_keeping);
                cmd.Parameters.AddWithValue("@roomtype", booking.roomtype);
                cmd.Parameters.AddWithValue("@numbed", booking.numbed);
                cmd.Parameters.AddWithValue("@view_room", booking.view_room);
                cmd.Parameters.AddWithValue("@maphong", booking.maphong);
                cmd.Parameters.AddWithValue("@roomnumber", booking.roomnumber);
                cmd.Parameters.AddWithValue("@group_customer", booking.group_customer);
                cmd.Parameters.AddWithValue("@date_ci", booking.date_ci);
                cmd.Parameters.AddWithValue("@date_co", booking.date_co);
                cmd.Parameters.AddWithValue("@cccd_em", booking.cccd_em);
                cmd.Parameters.AddWithValue("@price", booking.price);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Addcheckout(Checkout checkout, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addcheckout", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cccd_cus", checkout.cccd_cus);
                cmd.Parameters.AddWithValue("@first_name", checkout.first_name);
                cmd.Parameters.AddWithValue("@last_name", checkout.last_name);
                cmd.Parameters.AddWithValue("@maphong", checkout.maphong);
                cmd.Parameters.AddWithValue("@sophong", checkout.sophong);
                cmd.Parameters.AddWithValue("@date_ci", checkout.date_ci);
                cmd.Parameters.AddWithValue("@date_co", checkout.date_co);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Addaccount(Account account, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_account", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", account.username);
                cmd.Parameters.AddWithValue("@cccd_em", account.cccd_em);
                cmd.Parameters.AddWithValue("@password", account.password);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Updateserve(Serve serve, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updateserve", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cccd_cus", serve.cccd_cus);
                cmd.Parameters.AddWithValue("@maphong", serve.maphong);
                cmd.Parameters.AddWithValue("@other_booking", serve.other_booking);
                cmd.Parameters.AddWithValue("@anuong", serve.anuong);
                cmd.Parameters.AddWithValue("@call_serve", serve.call_serve);
                cmd.Parameters.AddWithValue("@cost", serve.cost);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = cmd.Parameters["@ErrorMessage"].Value != DBNull.Value ? (string)cmd.Parameters["@ErrorMessage"].Value : "No message returned.";
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Deleteaccount(Account account, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteaccount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", account.id);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Getserve(SqlConnection conn)
        {
            Response response = new Response();
            List<Serve> list = new List<Serve>();
            try
            {
                string query = @"SELECT * FROM Serve";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Serve serve = new Serve
                            {
                                stt = int.Parse(reader["stt"].ToString()),
                                cccd_cus = reader["cccd_cus"].ToString(),
                                maphong = int.Parse(reader["maphong"].ToString()),
                                other_booking = reader["other_booking"].ToString(),
                                anuong = reader["anuong"].ToString(),
                                call_serve = bool.Parse(reader["call_serve"].ToString()),
                                cost = int.Parse(reader["cost"].ToString()),
                            };
                            list.Add(serve);
                        }
                    }
                }
                response.list7 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Getcheckout(SqlConnection conn)
        {
            Response response = new Response();
            List<Checkout> list = new List<Checkout>();
            try
            {
                string query = @"SELECT * FROM Checkout";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Checkout checkout = new Checkout
                            {
                                id = int.Parse(reader["id"].ToString()),
                                cccd_cus = reader["cccd_cus"].ToString(),
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                maphong = int.Parse(reader["maphong"].ToString()),
                                sophong = int.Parse(reader["sophong"].ToString()),
                                date_ci = DateTime.Parse(reader["date_ci"].ToString()),
                                date_co = DateTime.Parse(reader["date_co"].ToString()),
                            };
                            list.Add(checkout);
                        }
                    }
                }
                response.list11 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Getreport(SqlConnection conn)
        {
            Response response = new Response();
            List<Report> list = new List<Report>();
            try
            {
                string query = @"SELECT * FROM Report";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Report report = new Report
                            {
                                cccd_cus = reader["cccd_cus"].ToString(),
                                maphong = int.Parse(reader["maphong"].ToString()),
                                ghichu = reader["ghichu"].ToString()
                            };
                            list.Add(report);
                        }
                    }
                }
                response.list8 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Getroom(SqlConnection conn)
        {
            Response response = new Response();
            List<Room> list = new List<Room>();
            try
            {
                string query= @"SELECT * FROM Room";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Room room = new Room
                            {
                                maphong = int.Parse(reader["maphong"].ToString()),
                                roomnumber = int.Parse(reader["roomnumber"].ToString()),
                                roomtype = reader["roomtype"].ToString(),
                                numbed = reader["numbed"].ToString(),
                                view_room = reader["view_room"].ToString(),
                                image_room = reader["image_room"].ToString(),
                                price = int.Parse(reader["price"].ToString()),
                            };
                            list.Add(room);
                        }
                    }
                }
                response.list = list;
            }
            catch(Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if(conn.State == ConnectionState.Open) 
                    conn.Close();
            }
            return response;
        }
        public Response Getroombook(SqlConnection conn)
        {
            Response response = new Response();
            List<DetailRoom> list = new List<DetailRoom>();
            try
            {
                string query = @"SELECT Room.maphong, roomnumber, roomtype, numbed, view_room, house_keeping, status_room, price
                                FROM Room
                                INNER JOIN Update_room ON Room.maphong = Update_room.maphong
                                WHERE Room.maphong NOT IN (	SELECT maphong
							                                FROM Bookings)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DetailRoom room = new DetailRoom
                            {
                                maphong = int.Parse(reader["maphong"].ToString()),
                                roomnumber = int.Parse(reader["roomnumber"].ToString()),
                                roomtype = reader["roomtype"].ToString(),
                                numbed = reader["numbed"].ToString(),
                                view_room = reader["view_room"].ToString(),
                                house_keeping = reader["house_keeping"].ToString(),
                                status_room = reader["status_room"].ToString(),
                                price = int.Parse(reader["price"].ToString()),
                            };
                            list.Add(room);
                        }
                    }
                }
                response.list10 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Getchamcong(SqlConnection conn)
        {
            Response response = new Response();
            List<EmployeeWork> list = new List<EmployeeWork>();
            try
            {
                string query = @"   SELECT 
                                    Chamcong.cccd_em, first_name, last_name, ngay, ca1, ca2, ca3, ca4, CAST(note AS NVARCHAR(MAX)) AS note
                                    FROM Chamcong
                                    INNER JOIN Employee ON Chamcong.cccd_em = Employee.cccd_em
                                    GROUP BY Chamcong.cccd_em, first_name, last_name, ngay, ca1, ca2, ca3, ca4, CAST(note AS NVARCHAR(MAX))";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeWork employee = new EmployeeWork
                            {
                                cccd_em = reader["cccd_em"].ToString(),
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                ngay = DateTime.Parse(reader["ngay"].ToString()),
                                ca1 = reader["ca1"].ToString(),
                                ca2 = reader["ca2"].ToString(),
                                ca3 = reader["ca3"].ToString(),
                                ca4 = reader["ca4"].ToString(),
                                note = reader["note"].ToString(),
                            };
                            list.Add(employee);
                        }
                    }
                }
                response.list4 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Getaccount(SqlConnection conn)
        {
            Response response = new Response();
            List<Account> list = new List<Account>();
            try
            {
                string query = @"SELECT * FROM Account";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Account account = new Account
                            {
                                id = int.Parse(reader["id"].ToString()),
                                username = reader["username"].ToString(),
                                password = reader["password"].ToString(),
                                cccd_em = reader["cccd_em"].ToString(),
                            };
                            list.Add(account);
                        }
                    }
                }
                response.list3 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Addcustomer(Customer customer, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addcustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cccd_cus", customer.cccd_cus);
                cmd.Parameters.AddWithValue("@first_name", customer.first_name);
                cmd.Parameters.AddWithValue("@last_name", customer.last_name);
                cmd.Parameters.AddWithValue("@sdt", customer.sdt);
                cmd.Parameters.AddWithValue("@email", customer.email);
                cmd.Parameters.AddWithValue("@gioitinh", customer.gioitinh);
                cmd.Parameters.AddWithValue("@ngaysinh", customer.ngaysinh);
                cmd.Parameters.AddWithValue("@address_cus", customer.address_cus);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Addemployee(ManageEmployee employee, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addemployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cccd_em", employee.cccd_em);
                cmd.Parameters.AddWithValue("@first_name", employee.first_name);
                cmd.Parameters.AddWithValue("@last_name", employee.last_name);
                cmd.Parameters.AddWithValue("@sdt", employee.sdt);
                cmd.Parameters.AddWithValue("@email", employee.email);
                cmd.Parameters.AddWithValue("@gioitinh", employee.gioitinh);
                cmd.Parameters.AddWithValue("@ngaysinh", employee.ngaysinh);
                cmd.Parameters.AddWithValue("@luong", employee.luong);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Getemployee(SqlConnection conn)
        {
            Response response = new Response();
            List<ManageEmployee> list = new List<ManageEmployee>();
            try
            {
                string query = @"SELECT * FROM Employee";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ManageEmployee employee = new ManageEmployee
                            {
                                cccd_em = reader["cccd_em"].ToString(),
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                sdt = reader["sdt"].ToString(),
                                email = reader["email"].ToString(),
                                gioitinh = reader["gioitinh"].ToString(),
                                ngaysinh = DateTime.Parse(reader["ngaysinh"].ToString()),
                                luong = float.Parse(reader["luong"].ToString()),
                            };
                            list.Add(employee);
                        }
                    }
                }
                response.list2 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Getcustomer(SqlConnection conn)
        {
            Response response = new Response();
            List<Customer> list = new List<Customer>();
            try
            {
                string query = @"SELECT * FROM Customer";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                cccd_cus = reader["cccd_cus"].ToString(),
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                sdt = reader["sdt"].ToString(),
                                email = reader["email"].ToString(),
                                gioitinh = reader["gioitinh"].ToString(),
                                ngaysinh = DateTime.Parse(reader["ngaysinh"].ToString()),
                                address_cus = reader["address_cus"].ToString(),
                            };
                            list.Add(customer);
                        }
                    }
                }
                response.list1 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Getbooking(SqlConnection conn)
        {
            Response response = new Response();
            List<Bookings> list = new List<Bookings>();
            try
            {
                string query = @"SELECT * FROM Bookings";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bookings bookings = new Bookings
                            {
                                stt = int.Parse(reader["stt"].ToString()),
                                cccd_cus = reader["cccd_cus"].ToString(),
                                status_room = reader["status_room"].ToString(),
                                house_keeping = reader["house_keeping"].ToString(),
                                roomtype = reader["roomtype"].ToString(),
                                numbed = int.Parse(reader["numbed"].ToString()),
                                view_room = reader["view_room"].ToString(),
                                maphong = int.Parse(reader["maphong"].ToString()),
                                roomnumber = int.Parse(reader["roomnumber"].ToString()),
                                group_customer = int.Parse(reader["group_customer"].ToString()),
                                date_ci = DateTime.Parse(reader["date_ci"].ToString()),
                                date_co = DateTime.Parse(reader["date_co"].ToString()),
                                cccd_em = reader["cccd_em"].ToString(),
                                price = int.Parse(reader["price"].ToString()),
                            };
                            list.Add(bookings);
                        }
                    }
                }
                response.list9 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Getcustomerbook(SqlConnection conn)
        {
            Response response = new Response();
            List<Customer> list = new List<Customer>();
            try
            {
                string query = @"   SELECT *
                                    FROM Customer
                                    WHERE cccd_cus NOT IN (	SELECT cccd_cus
                                    						FROM Bookings)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                cccd_cus = reader["cccd_cus"].ToString(),
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                sdt = reader["sdt"].ToString(),
                                email = reader["email"].ToString(),
                                gioitinh = reader["gioitinh"].ToString(),
                                ngaysinh = DateTime.Parse(reader["ngaysinh"].ToString()),
                                address_cus = reader["address_cus"].ToString(),
                            };
                            list.Add(customer);
                        }
                    }
                }
                response.list1 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Addupdateroom(RoomUpdate room, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addroomupdate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(room.maphong.ToString()))
                {
                    cmd.Parameters.AddWithValue("@maphong", room.maphong);
                }
                if (!string.IsNullOrEmpty(room.status_room))
                {
                    cmd.Parameters.AddWithValue("@status_room", room.status_room);
                }
                if (!string.IsNullOrEmpty(room.house_keeping))
                {
                    cmd.Parameters.AddWithValue("@house_keeping", room.house_keeping);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = cmd.Parameters["@ErrorMessage"].Value != DBNull.Value ? (string)cmd.Parameters["@ErrorMessage"].Value : "No message returned.";
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Getupdateroom(SqlConnection conn)
        {
            Response response = new Response();
            List<RoomUpdate> list = new List<RoomUpdate>();
            try
            {
                string query = @"   SELECT Room.maphong, roomnumber, status_room, house_keeping
                                    FROM Room
                                    INNER JOIN Update_room ON Room.maphong = Update_room.maphong
                                    WHERE Room.maphong = Update_room.maphong";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoomUpdate room = new RoomUpdate
                            {
                                maphong = int.Parse(reader["maphong"].ToString()),
                                roomnumber = int.Parse(reader["roomnumber"].ToString()),
                                status_room = reader["status_room"].ToString(),
                                house_keeping = reader["house_keeping"].ToString(),
                            };
                            list.Add(room);
                        }
                    }
                }
                response.list6 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Updatecustomer(Customer customer, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatecustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(customer.cccd_cus))
                {
                    cmd.Parameters.AddWithValue("@cccd_cus", customer.cccd_cus);
                }
                if (!string.IsNullOrEmpty(customer.first_name))
                {
                    cmd.Parameters.AddWithValue("@first_name", customer.first_name);
                }
                if (!string.IsNullOrEmpty(customer.last_name))
                {
                    cmd.Parameters.AddWithValue("@last_name", customer.last_name);
                }
                if (!string.IsNullOrEmpty(customer.sdt))
                {
                    cmd.Parameters.AddWithValue("@sdt", customer.sdt);
                }
                if (!string.IsNullOrEmpty(customer.email))
                {
                    cmd.Parameters.AddWithValue("@email", customer.email);
                }
                if (!string.IsNullOrEmpty(customer.gioitinh))
                {
                    cmd.Parameters.AddWithValue("@gioitinh", customer.gioitinh);
                }
                if (!string.IsNullOrEmpty(customer.ngaysinh.ToString()))
                {
                    cmd.Parameters.AddWithValue("@ngaysinh", customer.ngaysinh);
                }
                if (!string.IsNullOrEmpty(customer.address_cus))
                {
                    cmd.Parameters.AddWithValue("@address_cus", customer.address_cus);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Updateroombooking(RoomUpdate roomupdate, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatestatusroom", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(roomupdate.roomnumber.ToString()))
                {
                    cmd.Parameters.AddWithValue("@roomnumber", roomupdate.roomnumber);
                }
                
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Updateroombooking2(RoomUpdate roomupdate, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updatestatusroom2", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(roomupdate.roomnumber.ToString()))
                {
                    cmd.Parameters.AddWithValue("@roomnumber", roomupdate.roomnumber);
                }

                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Getemployeework(SqlConnection conn)
        {
            Response response = new Response();
            List<EmployeeWork> list = new List<EmployeeWork>();
            try
            {
                string query = @"   SELECT Chamcong.cccd_em, first_name, last_name, ngay, ca1, ca2, ca3, ca4, CAST(note AS NVARCHAR(MAX)) AS note, SUM(CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END + CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END + CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END + CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END) AS total_shifts
                                    FROM Chamcong
                                    INNER JOIN Employee ON Chamcong.cccd_em = Employee.cccd_em
                                    WHERE Chamcong.cccd_em = Employee.cccd_em
                                    GROUP BY Chamcong.cccd_em, first_name, last_name, ngay, ca1, ca2, ca3, ca4, CAST(note AS NVARCHAR(MAX))";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeWork employee = new EmployeeWork
                            {
                                cccd_em = reader["cccd_em"].ToString(),
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                ngay = DateTime.Parse(reader["ngay"].ToString()),
                                ca1 = reader["ca1"].ToString(),
                                ca2 = reader["ca2"].ToString(),
                                ca3 = reader["ca3"].ToString(),
                                ca4 = reader["ca4"].ToString(),
                                note = reader["note"].ToString(),
                                tongca = reader["total_shifts"].ToString(),
                            };
                            list.Add(employee);
                        }
                    }
                }
                response.list4 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Deleteroom(Room room, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteroom", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maphong", room.maphong);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Deletebooking(Bookings bookings, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletebooking", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@roomnumber", bookings.roomnumber);
                cmd.Parameters.AddWithValue("@cccd_cus", bookings.cccd_cus);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
        public Response Gettotal(SqlConnection conn)
        {
            Response response = new Response();
            List<Total> list = new List<Total>();
            try
            {
                string query = @"   SELECT 
                                    Employee.cccd_em, 
                                    first_name, 
                                    last_name, 
                                    DATEDIFF(DAY, MIN(ngay), GETDATE()) AS days_since_start, 
                                    SUM(
                                        CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END + 
                                        CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END + 
                                        CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END + 
                                        CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END
                                    ) AS total_shifts, 
                                    luong, 
                                    luong * SUM(
                                        CASE WHEN ca1 = 'Co' THEN 1 ELSE 0 END + 
                                        CASE WHEN ca2 = 'Co' THEN 1 ELSE 0 END + 
                                        CASE WHEN ca3 = 'Co' THEN 1 ELSE 0 END + 
                                        CASE WHEN ca4 = 'Co' THEN 1 ELSE 0 END
                                    ) AS total_salary
                                    FROM Employee
                                    INNER JOIN Chamcong ON Employee.cccd_em = Chamcong.cccd_em
                                    GROUP BY Employee.cccd_em, first_name, last_name, luong
                                    ORDER BY Employee.cccd_em;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Total total = new Total
                            {
                                cccd_em = reader["cccd_em"].ToString(),
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                tongngay = int.Parse(reader["days_since_start"].ToString()),
                                tongca = int.Parse(reader["total_shifts"].ToString()),
                                luong = float.Parse(reader["luong"].ToString()),
                                total = float.Parse(reader["total_salary"].ToString()),
                            };
                            list.Add(total);
                        }
                    }
                }
                response.list5 = list;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response Updateemployee(ManageEmployee employee, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updateemployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(employee.cccd_em))
                {
                    cmd.Parameters.AddWithValue("@cccd_em", employee.cccd_em);
                }
                if (!string.IsNullOrEmpty(employee.first_name))
                {
                    cmd.Parameters.AddWithValue("@first_name", employee.first_name);
                }
                if (!string.IsNullOrEmpty(employee.last_name))
                {
                    cmd.Parameters.AddWithValue("@last_name", employee.last_name);
                }
                if (!string.IsNullOrEmpty(employee.sdt))
                {
                    cmd.Parameters.AddWithValue("@sdt", employee.sdt);
                }
                if (!string.IsNullOrEmpty(employee.email))
                {
                    cmd.Parameters.AddWithValue("@email", employee.email);
                }
                if (!string.IsNullOrEmpty(employee.gioitinh))
                {
                    cmd.Parameters.AddWithValue("@gioitinh", employee.gioitinh);
                }
                if (!string.IsNullOrEmpty(employee.ngaysinh.ToString()))
                {
                    cmd.Parameters.AddWithValue("@ngaysinh", employee.ngaysinh);
                }
                if (!string.IsNullOrEmpty(employee.luong.ToString()))
                {
                    cmd.Parameters.AddWithValue("@luong", employee.luong);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.statusmessage = mess;
            }
            catch (Exception ex)
            {
                response.statusmessage = ex.Message;
            }
            return response;
        }
    }
}
