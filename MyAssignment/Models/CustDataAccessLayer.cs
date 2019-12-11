using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyAssignment.Models
{
    public class CustDataAccessLayer : ICustDataAccessLayer
    {
        string connectionString = "Server=FSIND-LT-6\\SQLEXPRESS;Database=CustomerDB;Trusted_Connection=TRUE;";
        public bool AddCustomers(Customers cust)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            {
                SqlCommand cmd = new SqlCommand("spAddCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", cust.CustomerId);
                cmd.Parameters.AddWithValue("@Name", cust.Name);
                cmd.Parameters.AddWithValue("@Address", cust.Address);
                cmd.Parameters.AddWithValue("@PaymentCategory", cust.PaymentCategory);
                cmd.Parameters.AddWithValue("@Phone", cust.Phone);

                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                return true;
            }
        }

        public IEnumerable<Customers> Customers()
        {
            List<Customers> customers = new List<Customers>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllCustomers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customers cust = new Customers();
                    cust.CustomerId = rdr["CustomerId"].ToString(); ;
                    cust.Name = rdr["Name"].ToString();
                    cust.Address = rdr["Address"].ToString();
                    cust.PaymentCategory = rdr["PaymentCategory"].ToString();
                    cust.Phone = rdr["Phone"].ToString();
                    customers.Add(cust);
                }
                cmd.Dispose();
                con.Close();
            }
            return customers;
        }

        public void UpdateCustomers(Customers cust)
        {
           // Debug.WriteLine(cust.CustomerId + cust.Name + cust.Address + cust.PaymentCategory + cust.Phone);
            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("spUpdateCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerId", cust.CustomerId);
            cmd.Parameters.AddWithValue("@Name", cust.Name);
            cmd.Parameters.AddWithValue("@Address", cust.Address);
            cmd.Parameters.AddWithValue("@PaymentCategory", cust.PaymentCategory);
            cmd.Parameters.AddWithValue("@Phone", cust.Phone);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public Customers GetCustomerData(string? id)
        {
            Customers customers = new Customers();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE CustomerId=@id", con);
                SqlParameter idParameter = cmd.Parameters.Add("@id", SqlDbType.VarChar, 100);
                idParameter.Value = id;
                con.Open();
                cmd.Prepare();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    customers.CustomerId = rdr["CustomerId"].ToString();
                    customers.Name = rdr["Name"].ToString();
                    customers.Address = rdr["Address"].ToString();
                    customers.PaymentCategory = rdr["PaymentCategory"].ToString();
                    customers.Phone = rdr["Phone"].ToString();
                }
                cmd.Dispose();
            }
            return customers;
        }

        public void DeleteCustomers(string? id)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("spDeleteCustomer", con);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", id);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

    }
}
