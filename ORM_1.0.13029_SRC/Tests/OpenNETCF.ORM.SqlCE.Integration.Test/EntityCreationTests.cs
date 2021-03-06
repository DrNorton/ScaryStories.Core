﻿using OpenNETCF.ORM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using OpenNETCF.ORM.Test;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using OpenNETCF.ORM.Test.Entities;

namespace OpenNETCF.ORM.SqlCE.Integration.Test
{
    [TestClass()]
    public class EntityCreationTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void DelegatePerfTest()
        {
            var iterations = 1000;
            var sw1 = new Stopwatch();
            var sw2 = new Stopwatch();

            var store = new SqlCeDataStore("test.sdf");
            store.AddType<TestItem>();
            store.AddType<TestItemD>();
            store.CreateOrUpdateStore();

            // populate test data
            var generator = new DataGenerator();
            var items = generator.GenerateTestItems(100);
            store.BulkInsert(items);
            foreach (var i in items)
            {
                store.Insert((TestItemD)i);
            }


            // no delegate
            sw1.Reset();
            sw1.Start();
            for (int i = 0; i < iterations; i++)
            {
                var list = store.Select<TestItem>();
            }
            sw1.Stop();
            // with delegate
            sw2.Reset();
            sw2.Start();
            for (int i = 0; i < iterations; i++)
            {
                var list = store.Select<TestItemD>();
            }
            sw2.Stop();

            var noDelegate = sw1.ElapsedMilliseconds;
            var withDelegate = sw2.ElapsedMilliseconds;

            Debug.WriteLine(string.Format("Delegate gave a {0}% improvement", ((float)(noDelegate - withDelegate) / withDelegate) * 100f));
        }

        [TestMethod()]
        public void SeekTestStatic()
        {
            var sw1 = new Stopwatch();

            var store = new SqlCeDataStore("test.sdf");
            store.AddType<SeekItem>();
            store.CreateOrUpdateStore();

            // populate test data
            var generator = new DataGenerator();
            var items = generator.GenerateSeekItems(100);
            store.BulkInsert(items);


            // no delegate
            sw1.Reset();
            sw1.Start();

            var item = store.First<SeekItem>(System.Data.SqlServerCe.DbSeekOptions.BeforeEqual, "SeekField", 11);
            sw1.Stop();

            // item should have a value of 10
            Assert.AreEqual(10, item.SeekField);

        }

        [TestMethod()]
        public void SeekTestDynamic()
        {
            var sw1 = new Stopwatch();

            var store = new SqlCeDataStore("test.sdf");
            store.CreateOrUpdateStore();

            // populate test data
            var fieldList = new List<FieldAttribute>();
            fieldList.Add(new FieldAttribute()
            {
                FieldName = "ID",
                IsPrimaryKey = true,
                DataType = System.Data.DbType.Int32
            });

            fieldList.Add(new FieldAttribute()
            {
                FieldName = "SeekField",
                DataType = System.Data.DbType.Int64,
                AllowsNulls = false,
                SearchOrder = FieldSearchOrder.Ascending
            });

            fieldList.Add(new FieldAttribute()
            {
                FieldName = "Data",
                DataType = System.Data.DbType.String,
                AllowsNulls = false
            });

            var definition = new DynamicEntityDefinition("DynamicSeekItem", fieldList, KeyScheme.Identity);
            store.RegisterDynamicEntity(definition);


            for (int i = 0; i < 100; i++)
            {
                var de = new DynamicEntity("DynamicSeekItem");
                de.Fields["SeekField"] = i * 10;
                de.Fields["Data"] = "Item " +  (i * 10).ToString();

                store.Insert(de);
            }



            // no delegate
            sw1.Reset();
            sw1.Start();

            var item = store.First("DynamicSeekItem", System.Data.SqlServerCe.DbSeekOptions.BeforeEqual, "SeekField", 11);
            sw1.Stop();

            // item should have a value of 10


        }
    }
}
