using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCL3.Table.Tests.Model
{
    public static class CustomerEntityHelper
    {
        private static Random rnd = new Random();

        public static CustomerEntity Create()
        {
            // Create a new customer with random first and last name.
            var customer = new CustomerEntity(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            // Create an e-mail address based on first and last name.
            customer.Email = customer.FirstName + "." + customer.LastName + "@contoso.com";

            // Create a random Phone Number
            var phoneNumber = rnd.Next(1000000000, Int32.MaxValue).ToString();
            phoneNumber = phoneNumber.Insert(3, "-");
            phoneNumber = phoneNumber.Insert(7, "-");
            customer.PhoneNumber = phoneNumber;

            return customer;
        }

        public static CustomerEntity Create(string LastName)
        {
            // Create a new customer with random first and last name.
            var customer = new CustomerEntity(
                LastName,
                Guid.NewGuid().ToString());

            // Create an e-mail address based on last name.
            customer.Email = customer.FirstName + "." + customer.LastName + "@contoso.com";

            // Create a random Phone Number
            var phoneNumber = rnd.Next(1000000000, Int32.MaxValue).ToString();
            phoneNumber = phoneNumber.Insert(3, "-");
            phoneNumber = phoneNumber.Insert(7, "-");
            customer.PhoneNumber = phoneNumber;

            return customer;
        }

    }
}
