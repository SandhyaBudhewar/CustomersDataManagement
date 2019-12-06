using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAssignment.Models
{
    public class CustDataAccessLayer2 : ICustDataAccessLayer
    {
        CustomerDBContext customerDBContext;

        public CustDataAccessLayer2(CustomerDBContext context)
        {
            customerDBContext = context;
        }

        public void AddCustomers(Customers customer)
        {
            customerDBContext.Add(customer);
            customerDBContext.SaveChanges();
        }

        public IEnumerable<Customers> Customers()
        {
            return customerDBContext.Customers.ToList(); ;
        }

        public void DeleteCustomers(string? id)
        {
            var customers = customerDBContext.Customers.Find(id);
            customerDBContext.Customers.Remove(customers);
            customerDBContext.SaveChanges();
        }

        public void UpdateCustomers(Customers customers)
        {
            customerDBContext.Update(customers);
            customerDBContext.SaveChanges();
        }

        public Customers GetCustomerData(string? id)
        {
            Customers customers = customerDBContext.Customers.Find(id);
            return customers;
        }

       
    }
  }

