using System.Collections.Generic;

namespace MyAssignment.Models
{
    public interface ICustDataAccessLayer
    {
        bool AddCustomers(Customers cust);
        IEnumerable<Customers> Customers();
        void DeleteCustomers(string id);
        Customers GetCustomerData(string id);
        void UpdateCustomers(Customers cust);
    }
}