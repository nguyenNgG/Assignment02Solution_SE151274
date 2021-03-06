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

namespace eBookStoreClient.Pages.Publishers
{
    public class DeleteModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public DeleteModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public Publisher Publisher { get; set; }

        [TempData]
        public int PublisherId { get; set; }

        public string HasBookMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToPage(PageRoute.Publishers);
                }

                PublisherId = (int)id;
                TempData["PublisherId"] = PublisherId;

                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Publishers}/{id}");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Publisher = JsonSerializer.Deserialize<Publisher>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                        return Page();
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToPage(PageRoute.Publishers);
                    }
                }
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Publisher.PublisherId = (int)TempData.Peek("PublisherId");
            TempData.Keep("PublisherId");

            try
            {
                HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Books}?$filter=PublisherId eq {PublisherId}");
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var books = JsonSerializer.Deserialize<Books>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                    if (books.Count > 0)
                    {
                        HasBookMessage = "Unable to delete publisher. This publisher is assigned to book(s).";
                        return await OnGetAsync(PublisherId);
                    }
                    httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    response = await httpClient.DeleteAsync($"{Endpoints.Publishers}/{PublisherId}");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToPage(PageRoute.Publishers);
                    }
                }
                return await OnGetAsync(Publisher.PublisherId);
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Publishers);
        }
    }

    class Books
    {
        [JsonPropertyName("value")]
        public List<Book> List { get; set; }
    }
}
