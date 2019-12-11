using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyAssignment.Models
{
    public class UserDataAccessLayer : IUserDataAccessLayer
    {
        string connectionString = "Server=FSIND-LT-6\\SQLEXPRESS;Database=CustomerDB;Trusted_Connection=TRUE;";
        public bool AddUser(Users user)
        {
            if (CheckUserDetails(user.Username).Username != user.Username)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        

        public bool CheckLogin(Users user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@val1 AND Password=@val2", con);
                SqlParameter emailParameter = cmd.Parameters.Add("@val1", SqlDbType.VarChar, 30);
                emailParameter.Value = user.Username;
                SqlParameter passParameter = cmd.Parameters.Add("@val2", SqlDbType.VarChar, 30);
                passParameter.Value = user.Password;
                con.Open();
                cmd.Prepare();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string email = rdr["Username"].ToString();
                    string pass = rdr["Password"].ToString();

                    if (email == user.Username && pass == user.Password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }

        }

        public bool UpdatePassword(string Username, string NewPassword)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spChange_pass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
              
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return true;
            }

        }

        public Users CheckUserDetails(string Username)
        {
            Users user = new Users();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCheckUserDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Username", SqlDbType.VarChar, 30).Value = Username;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.Username = rdr["Username"].ToString();
                    user.Password = rdr["Password"].ToString();
                }
            }
            return user;
        }

    }
}
