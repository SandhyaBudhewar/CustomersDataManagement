using MyAssignment.Models;
using System;
using Xunit;
using System.Threading.Tasks;
using Moq;


namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test_Invalid_CustomerId()
        {
            var custDataAccess = new CustDataAccessLayer2(new customerDBContext());
            Customers customer = new Customers { Name = "Sandhya",Address = "Pune", PaymentCategory = "Cash", Phone = "9865236589" };
            Assert.Throws<ArgumentNullException>(() => custDataAccess.AddCustomers(customer));

        }

        [Fact]
        public void Test_Invalid_CustomerName()
        {
            var custDataAccess = new CustDataAccessLayer2(new customerDBContext());
            Customers customer = new Customers { CustomerId="101", Address = "Pune", PaymentCategory = "Cash", Phone = "9865236589" };
            Assert.Throws<ArgumentNullException>(() => custDataAccess.AddCustomers(customer));

        }

        [Fact]
        public void Test_Invalid_CustomerAddress()
        {
            var custDataAccess = new CustDataAccessLayer2(new customerDBContext());
            Customers customer = new Customers { CustomerId = "101", Name = "Sandhya", PaymentCategory = "Cash", Phone = "9865236589" };
            Assert.Throws<ArgumentNullException>(() => custDataAccess.AddCustomers(customer));

        }

        [Fact]
        public void Test_Invalid_CustomerPaymentCategory()
        {
            var custDataAccess = new CustDataAccessLayer2(new customerDBContext());
            Customers customer = new Customers { CustomerId = "101", Name = "Sandhya", Address = "Pune", Phone = "9865236589" };
            Assert.Throws<ArgumentNullException>(() => custDataAccess.AddCustomers(customer));

        }

        [Fact]
        public void Test_Invalid_CustomerPaymentPhone()
        {
            var custDataAccess = new CustDataAccessLayer2(new customerDBContext());
            Customers customer = new Customers { CustomerId = "101", Name = "Sandhya", Address = "Pune", PaymentCategory = "Cash" };
            Assert.Throws<ArgumentNullException>(() => custDataAccess.AddCustomers(customer));

        }
        [Fact]
        public void Test_Valid_Customer()
        {
            var mockCustomers = new Mock<Customers>();

            var custDataAccess = new Mock<ICustDataAccessLayer>();

            custDataAccess.Setup(x => x.AddCustomers(It.IsAny<Customers>())).Returns(true);
            Customers customer = new Customers { CustomerId = "101", Name = "Sandhya", Address = "Pune", PaymentCategory = "Cash" , Phone = "9865236589" };
            Assert.True(custDataAccess.Object.AddCustomers(customer));
        }
    }
}
