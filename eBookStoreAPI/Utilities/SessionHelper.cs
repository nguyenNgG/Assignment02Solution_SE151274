using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace eBookStoreAPI.Utilities
{
    public static class SessionHelper
    {
        public static T GetFromSession<T>(ISession session, string sessionValue)
        {
            string jsonData = session.GetString(sessionValue);
            T data;
            try
            {
                data = JsonSerializer.Deserialize<T>(jsonData!);
            }
            catch
            {
                return default;
            }
            return data;
        }

        public static bool SaveToSession<T>(ISession session, T data, string sessionValue)
        {
            string jsonData;
            try
            {
                jsonData = JsonSerializer.Serialize<T>(data);
            }
            catch
            {
                return false;
            }
            session.SetString(sessionValue, jsonData);
            return true;
        }

        public static void RemoveFromSession(ISession session, string sessionValue)
        {
            session.Remove(sessionValue);
        }
    }
}
