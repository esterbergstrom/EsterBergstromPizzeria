using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedValue);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var serializedValue = session.GetString(key);

            if (serializedValue != null)
            {
                var deserializedValue = JsonConvert.DeserializeObject<T>(serializedValue);
                return deserializedValue;
            }

            return default(T);
        }
    }
}
