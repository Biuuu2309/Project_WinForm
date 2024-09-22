using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                SqlCommand cmd = new SqlCommand("sp_addroom", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maphong", room.maphong);
                cmd.Parameters.AddWithValue("@roomnumber", room.roomnumber);
                cmd.Parameters.AddWithValue("@roomtype", room.roomtype);
                cmd.Parameters.AddWithValue("@numbed", room.numbed);
                cmd.Parameters.AddWithValue("@view_room", room.view_room);
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
                                numbed = int.Parse(reader["numbed"].ToString()),
                                view_room = reader["view_room"].ToString(),
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
        ///DELETE Room
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
        public Response Addemployee(Manage employee, SqlConnection conn)
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
            List<Manage> list = new List<Manage>();
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
                            Manage employee = new Manage
                            {
                                cccd_em = reader["cccd_em"].ToString(),
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                sdt = reader["sdt"].ToString(),
                                email = reader["email"].ToString(),
                                gioitinh = reader["gioitinh"].ToString(),
                                ngaysinh = DateTime.Parse(reader["ngaysinh"].ToString()),
                                luong = reader["luong"].ToString(),
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
        public Response Addupdateroom(Room room, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addroomupdate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maphong", room.maphong);
                cmd.Parameters.AddWithValue("@status_room", room.status_room);
                cmd.Parameters.AddWithValue("@house_keeping", room.house_keeping);
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
        public Response Getupdateroom(SqlConnection conn)
        {
            Response response = new Response();
            List<Room> list = new List<Room>();
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
                            Room room = new Room
                            {
                                maphong = int.Parse(reader["maphong"].ToString()),
                                status_room = reader["status_room"].ToString(),
                                house_keeping = reader["house_keeping"].ToString(),
                            };
                            list.Add(room);
                        }
                    }
                }
                response.list = list;
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
                cmd.Parameters.AddWithValue("@cccd_cus", customer.cccd_cus);
                cmd.Parameters.AddWithValue("@first_name", customer.first_name);
                cmd.Parameters.AddWithValue("@last_name", customer.last_name);
                cmd.Parameters.AddWithValue("@sdt", customer.sdt);
                cmd.Parameters.AddWithValue("@email", customer.email);
                cmd.Parameters.AddWithValue("@gioitinh", customer.gioitinh);
                cmd.Parameters.AddWithValue("@ngaysinh", customer.ngaysinh);
                cmd.Parameters.AddWithValue("@address_cus", customer.address_cus);
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
    }
}
