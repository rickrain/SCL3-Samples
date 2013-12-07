using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;

namespace SCL3.Table.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected CloudTableClient tableClient = null;

        [TestInitialize]
        public virtual void Initialize()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            tableClient = storageAccount.CreateCloudTableClient();
        }
    }
}
