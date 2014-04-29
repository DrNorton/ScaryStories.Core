using System;
using System.IO.IsolatedStorage;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VkontakteInfrastructure.Interfaces;
using VkontakteInfrastructure.Model;

namespace VkontakteDataLayer
{
    public class AuthContextStorage :EntityStorage
    {
        
    }



    public class EntityStorage : IEntityDataStorage
    {
        private readonly string key;

        public EntityStorage()
        {
            key = String.Empty;
        }

        public EntityStorage(string key)
        {
            this.key = key;
        }

        public void SaveEntity<T>(T item)
        {
            var fullKey = GetFullKey(typeof(T));
            IsolatedStorageSettings.ApplicationSettings[fullKey] = item;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public T LoadEntity<T>()
        {
            var fullKey = GetFullKey(typeof(T));

            if (IsolatedStorageSettings.ApplicationSettings.Contains(fullKey))
            {
                return (T)IsolatedStorageSettings.ApplicationSettings[fullKey];
            }
            return default(T);
        }

        public void DeleteEntity<T>()
        {
            var fullKey = GetFullKey(typeof(T));
            if(IsolatedStorageSettings.ApplicationSettings.Contains(fullKey))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(fullKey);
            }
        }

        private string GetFullKey(MemberInfo t)
        {
            return t.Name + "_key_"+key;
        }


        
    }

    
}
