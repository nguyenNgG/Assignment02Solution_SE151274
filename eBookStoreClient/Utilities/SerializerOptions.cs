using System.Text.Json;

namespace eBookStoreClient.Utilities
{
    public static class SerializerOptions
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public static JsonSerializerOptions CaseInsensitive = jsonSerializerOptions;
    }
}
