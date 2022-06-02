using eBookStoreClient.Constants;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreClient.Utilities
{
    public static class SessionHelper
    {
        public static async Task<HttpResponseMessage> Current(ISession session, HttpSessionStorage sessionStorage)
        {
            HttpClient httpClient = GetHttpClient(session, sessionStorage);
            return await httpClient.GetAsync(Endpoints.Current);
        }

        public static async Task<HttpResponseMessage> Authenticate(ISession session, HttpSessionStorage sessionStorage)
        {
            HttpClient httpClient = GetHttpClient(session, sessionStorage);
            return await httpClient.GetAsync(Endpoints.Authenticate);
        }

        public static async Task<HttpResponseMessage> Authorize(ISession session, HttpSessionStorage sessionStorage)
        {
            HttpClient httpClient = GetHttpClient(session, sessionStorage);
            return await httpClient.GetAsync(Endpoints.Authorize);
        }

        public static void GetNewHttpClient(ISession session, HttpSessionStorage sessionStorage)
        {
            HttpClient httpClient = new HttpClient();
            sessionStorage.HttpClients.Add(httpClient);
            SaveToSession<int?>(session, sessionStorage.HttpClients.IndexOf(httpClient), SessionValue.HttpSessionIndex);
        }

        public static HttpClient GetHttpClient(ISession session, HttpSessionStorage sessionStorage)
        {
            HttpClient httpClient;
            int? httpSessionIndex = GetFromSession<int?>(session, SessionValue.HttpSessionIndex);
            if (httpSessionIndex == null)
            {
                httpClient = new HttpClient();
                sessionStorage.HttpClients.Add(httpClient);
                SaveToSession<int?>(session, sessionStorage.HttpClients.IndexOf(httpClient), SessionValue.HttpSessionIndex);
            }
            else
            {
                httpClient = sessionStorage.HttpClients[(int)httpSessionIndex];
            }
            return httpClient;
        }

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
