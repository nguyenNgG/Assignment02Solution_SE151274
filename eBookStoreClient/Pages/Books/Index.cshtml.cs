using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using eBookStoreClient.Utilities;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using eBookStoreClient.Constants;

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
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Books}");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        Books = JsonSerializer.Deserialize<List<Book>>(await content.ReadAsStringAsync(), jsonSerializerOptions);
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
}
