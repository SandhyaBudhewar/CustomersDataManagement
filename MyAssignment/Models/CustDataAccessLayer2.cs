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

        public bool AddCustomers(Customers customer)
        {
            if (string.IsNullOrEmpty(customer.CustomerId))
            {
                throw new ArgumentNullException(customer.CustomerId);
            }
            if (string.IsNullOrEmpty(customer.Name))
            {
                throw new ArgumentNullException(customer.Name);
            }
            else if (string.IsNullOrEmpty(customer.Address))
            {
                throw new ArgumentNullException(customer.Address);
            }
            else if (string.IsNullOrEmpty(customer.PaymentCategory))
            {
                throw new ArgumentNullException(customer.PaymentCategory);
            }
            else if (string.IsNullOrEmpty(customer.Phone))
            {
                throw new ArgumentNullException(customer.Phone);
            }
            customerDBContext.Add(customer);
            customerDBContext.SaveChanges();
            return true;
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

