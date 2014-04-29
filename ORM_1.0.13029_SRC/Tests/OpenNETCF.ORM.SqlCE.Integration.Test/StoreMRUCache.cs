using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.ORM
{
    class StoreMRUCache
    {
        private IDataStore Store { get; set; }
        private Dictionary<Type, object> Cache { get; set; }
        private object SyncRoot = new object();

        public StoreMRUCache(IDataStore store)
        {
            Store = store;
            Cache = new Dictionary<Type, object>();

            Store.AfterInsert += new EventHandler<EntityInsertArgs>(Store_AfterInsert);
            Store.AfterUpdate += new EventHandler<EntityUpdateArgs>(Store_AfterUpdate);
        }

        void Store_AfterUpdate(object sender, EntityUpdateArgs e)
        {
            UpdateCache(e.Item);
        }

        void Store_AfterInsert(object sender, EntityInsertArgs e)
        {
            UpdateCache(e.Item);
        }

        private void UpdateCache(object item)
        {
            var type = item.GetType();

            lock (SyncRoot)
            {
                if (Cache.ContainsKey(type))
                {
                    Cache[type] = item;
                }
                else
                {
                    Cache.Add(type, item);
                }
            }
        }

        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }

        public bool Contains(Type type)
        {
            lock (SyncRoot)
            {
                return (Cache.ContainsKey(type));
            }
        }

        public T Get<T>()
            where T : class
        {
            return (T)Get(typeof(T));
        }

        public object Get(Type type)
        {
            lock (SyncRoot)
            {
                if (Cache.ContainsKey(type))
                {
                    return Cache[type];
                }

                return null;
            }
        }
    }
}
