using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCL3.Table.Tests.Model
{
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(string lastName, string firstName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
        }

        public CustomerEntity()
        { }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string LastName { get { return this.PartitionKey; } }

        public string FirstName { get { return this.RowKey; } }
    }
}
