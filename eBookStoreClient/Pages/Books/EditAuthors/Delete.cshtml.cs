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

namespace eBookStoreClient.Pages.Books.EditAuthors
{
    public class DeleteModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public DeleteModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public BookAuthor BookAuthor { get; set; }

        [TempData]
        public int BookId { get; set; }

        [TempData]
        public int AuthorId { get; set; }

        public string AtLeastOneMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? bookId, int? authorId)
        {
            try
            {
                if (bookId == null || authorId == null)
                {
                    return RedirectToPage(PageRoute.Books);
                }

                BookId = (int)bookId;
                TempData["BookId"] = BookId;
                AuthorId = (int)authorId;
                TempData["AuthorId"] = AuthorId;

                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.BookAuthors}(AuthorId={AuthorId},BookId={BookId})?$expand=Author");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        BookAuthor = JsonSerializer.Deserialize<BookAuthor>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                        return Page();
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
                    }
                }
                return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            BookAuthor.BookId = (int)TempData.Peek("BookId");
            BookAuthor.AuthorId = (int)TempData.Peek("AuthorId");
            TempData.Keep("BookId");
            TempData.Keep("AuthorId");

            try
            {
                HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.BookAuthors}?$filter=BookId eq {BookId}");
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var bookAuthors = JsonSerializer.Deserialize<BookAuthors>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                    if (bookAuthors.Count == 1)
                    {
                        AtLeastOneMessage = "Unable to unassign author. There must be at least one author.";
                        return await OnGetAsync(BookId, AuthorId);
                    }
                    httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    response = await httpClient.DeleteAsync($"{Endpoints.BookAuthors}(AuthorId={AuthorId},BookId={BookId})");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
                    }
                }
                return await OnGetAsync(BookId, AuthorId);
            }
            catch
            {
            }
            return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
        }
    }

    class BookAuthors
    {
        [JsonPropertyName("value")]
        public List<BookAuthor> List { get; set; }
    }
}
