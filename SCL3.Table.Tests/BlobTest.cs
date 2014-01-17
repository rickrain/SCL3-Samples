using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SCL3.Table.Tests
{
    [TestClass]
    public class BlobTest : BaseTest
    {
        private CloudBlobContainer container = null;
        private CloudBlockBlob blockBlob = null;
        private CloudBlobClient blobClient = null;

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
