using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;
using SCL3.Table.Tests.Model;
using System.Net;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System.Collections.Generic;

namespace SCL3.Table.Tests
{
    [TestClass]
    public class TableTest : BaseTest
    {
        private CloudTable table = null;
        private string tableName = "rickrainey" + DateTime.UtcNow.Ticks.ToString();

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            table = tableClient.GetTableReference(tableName);
            Assert.IsTrue(table.CreateIfNotExists());
            tableClient.PayloadFormat = TablePayloadFormat.JsonFullMetadata;
            Console.WriteLine("Created table {0}.", table.Name);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Console.WriteLine(tableClient.StorageUri);
            Console.WriteLine("Deleting table {0}.", table.Name);
            Assert.IsTrue(table.DeleteIfExists());
        }

        [TestMethod]
        public void InsertAndRetrieveEntity()
        {
            var customer = CustomerEntityHelper.Create();

            TableOperation insertOperation = TableOperation.Insert(customer);
            var tableResult = table.Execute(insertOperation);
            Assert.IsTrue(tableResult.HttpStatusCode == (int)HttpStatusCode.NoContent);

            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>(
                customer.PartitionKey, customer.RowKey);

            tableResult = table.Execute(retrieveOperation);


            if (tableResult.HttpStatusCode == (int)HttpStatusCode.OK)
            {
                customer = (CustomerEntity)tableResult.Result;
                Console.WriteLine("{0}, {1}\t{2}\t{3}",
                    customer.LastName,
                    customer.FirstName,
                    customer.Email,
                    customer.PhoneNumber);
            }
        }

        [TestMethod]
        public void InsertBatchOfEntities()
        {
            var batchSize = 50;
            var lastName = Guid.NewGuid().ToString();
            var result = Do_InsertBatchOfEntities(lastName, batchSize);
            Assert.AreEqual(result.Count, batchSize);
        }

        private IList<TableResult> Do_InsertBatchOfEntities(string partitionKey, int batchSize)
        {
            TableBatchOperation batchOperation = new TableBatchOperation();
            for (int x = 0; x < batchSize; x++)
            {
                batchOperation.Insert(CustomerEntityHelper.Create(partitionKey));
            }

            return table.ExecuteBatch(batchOperation);
        }

        [TestMethod]
        public void GetAllEntitiesInPartition()
        {
            var batchSize = 50;
            var lastName = Guid.NewGuid().ToString();
            var result = Do_InsertBatchOfEntities(lastName, batchSize);
            Assert.AreEqual(result.Count, batchSize);

            TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(
                TableQuery.GenerateFilterCondition(
                "PartitionKey", QueryComparisons.Equal, lastName));

            foreach (CustomerEntity customer in table.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}",
                    customer.LastName,
                    customer.FirstName,
                    customer.Email,
                    customer.PhoneNumber);
            }
        }

        [TestMethod]
        public void GetRangeOfEntitiesInPartition()
        {
            var batchSize = 50;
            var lastName = Guid.NewGuid().ToString();       
            var result = Do_InsertBatchOfEntities(lastName, batchSize);
            Assert.AreEqual(result.Count, batchSize);

            TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition(
                        "PartitionKey", QueryComparisons.Equal, lastName),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition(
                        "RowKey", QueryComparisons.GreaterThan, "8")));

            foreach (CustomerEntity customer in table.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}",
                    customer.LastName,
                    customer.FirstName,
                    customer.Email,
                    customer.PhoneNumber);
            }
        }
    }
}
