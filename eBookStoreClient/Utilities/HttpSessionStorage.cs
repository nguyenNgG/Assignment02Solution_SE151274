using System.Collections.Generic;
using System.Net.Http;

namespace eBookStoreClient.Utilities
{
    public class HttpSessionStorage
    {
        public List<HttpClient> HttpClients { get; set; } = new List<HttpClient>();
    }
}
