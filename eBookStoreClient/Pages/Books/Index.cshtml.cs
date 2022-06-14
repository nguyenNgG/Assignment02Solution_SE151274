using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Books
{
    public class IndexModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public IndexModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public IList<Book> Books { get; set; }

        [BindProperty]
        public string Filter { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Books}?$expand=Publisher");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var str = await content.ReadAsStringAsync();
                        Books = JsonSerializer.Deserialize<Books>(str, SerializerOptions.CaseInsensitive).List;
                        return Page();
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return Page();
                    }
                }
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }
    }

    public class Books
    {
        [JsonPropertyName("value")]
        public List<Book> List { get; set; }
    }
}
