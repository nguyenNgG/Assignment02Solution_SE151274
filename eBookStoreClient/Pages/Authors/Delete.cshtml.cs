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

namespace eBookStoreClient.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public DeleteModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public Author Author { get; set; }

        [TempData]
        public int AuthorId { get; set; }

        public string HasBookMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToPage(PageRoute.Authors);
                }

                AuthorId = (int)id;
                TempData["AuthorId"] = AuthorId;

                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Authors}/{id}");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Author = JsonSerializer.Deserialize<Author>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                        return Page();
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToPage(PageRoute.Authors);
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
            Author.AuthorId = (int)TempData.Peek("AuthorId");
            TempData.Keep("AuthorId");

            try
            {
                HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.BookAuthors}?$filter=AuthorId eq {AuthorId}");
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var bookAuthors = JsonSerializer.Deserialize<BookAuthors>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                    if (bookAuthors.Count > 0)
                    {
                        HasBookMessage = "Unable to delete author. This author is assigned to book(s).";
                        return await OnGetAsync(AuthorId);
                    }
                    httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    response = await httpClient.DeleteAsync($"{Endpoints.Authors}/{AuthorId}");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToPage(PageRoute.Authors);
                    }
                }
                return await OnGetAsync(Author.AuthorId);
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Authors);
        }
    }

    class BookAuthors
    {
        [JsonPropertyName("value")]
        public List<BookAuthor> List { get; set; }
    }
}
