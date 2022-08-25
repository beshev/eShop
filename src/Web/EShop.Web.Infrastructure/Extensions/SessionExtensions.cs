namespace EShop.Web.Infrastructure.Extensions
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static void SetCollection<T>(this ISession session, string key, ICollection<T> value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static ICollection<T> GetCollection<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<ICollection<T>>(value);
        }
    }
}
