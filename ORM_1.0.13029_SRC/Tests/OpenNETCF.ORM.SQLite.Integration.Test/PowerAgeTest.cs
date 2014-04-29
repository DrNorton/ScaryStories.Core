using OpenNETCF.ORM.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace OpenNETCF.ORM.SQLite.Integration.Test
{
    [TestClass()]
    public class PowerAgeTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        [DeploymentItem("SQLite.Interop.dll")]
        [DeploymentItem("PowerAge.db")]
        public void FooTEst()
        {
            var store = new SQLiteDataStore("PowerAge.db");
            store.AddType<Order>();
            store.AddType<Customer>();
            store.AddType<Destination>();
            var exists = store.StoreExists;

            var orders = store.Select<Order>(true).ToArray();
            var customers = store.Select<Customer>().ToArray();

            var newOrder = new Order()
            {
                CustomerID = 1,
                OrderNumber = 42,
                OrderNote = "Foo"
            };

            var dest = new Destination()
            {
                Address = "1234  Main st"
            };

            store.Insert(newOrder);

            var newOrder2 = new Order()
            {
                CustomerID = 1,
                OrderNumber = 42,
                OrderNote = "Foo",
                Destination = dest
            };

            store.Insert(newOrder2, true);

            var ordersNew = store.Select<Order>(true).ToArray();
        }
    }
}
