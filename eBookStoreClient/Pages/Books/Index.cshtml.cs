using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Books
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/odata/Books");
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            Console.WriteLine(result);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var a = JsonSerializer.Deserialize<Books>(result, options);

            return Page();
        }
    }

    public class Books
    {
        [JsonPropertyName("value")]
        public List<Book> List { get; set; }
    }
}
