using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Books.EditAuthors
{
    public class CreateModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public CreateModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public BookAuthor BookAuthor { get; set; }

        public List<Author> Authors { get; set; }

        [TempData]
        public int BookId { get; set; }

        public async Task<ActionResult> OnGetAsync(int? bookId)
        {
            try
            {
                if (bookId == null)
                {
                    return RedirectToPage(PageRoute.Books);
                }

                BookId = (int)bookId;
                TempData["BookId"] = BookId;
                BookAuthor = new BookAuthor
                {
                    BookId = BookId
                };

                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Authors}");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var str = await content.ReadAsStringAsync();
                        Authors = JsonSerializer.Deserialize<Authors>(str, SerializerOptions.CaseInsensitive).List;
                        var authors = Authors.Select(a => new
                        {
                            AuthorId = a.AuthorId,
                            Name = $"{a.FirstName} {a.LastName}"
                        }).ToList();
                        httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                        response = await httpClient.GetAsync($"{Endpoints.BookAuthors}?$filter=BookId eq {BookId}");
                        content = response.Content;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var bookAuthors = JsonSerializer.Deserialize<BookAuthors>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                            foreach (var baut in bookAuthors)
                            {
                                authors.RemoveAll(aut => aut.AuthorId == baut.AuthorId);
                            }
                            ViewData["AuthorId"] = new SelectList(authors, "AuthorId", "Name");
                            return Page();
                        }
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToPage(PageRoute.Books);
                    }
                }
                return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<ActionResult> OnPostAsync()
        {
            BookAuthor.BookId = (int)TempData.Peek("BookId");
            TempData.Keep("BookId");

            try
            {
                HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Authors}");
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var str = await content.ReadAsStringAsync();
                    Authors = JsonSerializer.Deserialize<Authors>(str, SerializerOptions.CaseInsensitive).List;
                    var authors = Authors.Select(a => new
                    {
                        AuthorId = a.AuthorId,
                        Name = $"{a.FirstName} {a.LastName}"
                    }).ToList();
                    httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    response = await httpClient.GetAsync($"{Endpoints.BookAuthors}?$filter=BookId eq {BookId}");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var bookAuthors = JsonSerializer.Deserialize<BookAuthors>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                        foreach (var baut in bookAuthors)
                        {
                            authors.RemoveAll(aut => aut.AuthorId == baut.AuthorId);
                        }
                        ViewData["AuthorId"] = new SelectList(authors, "AuthorId", "Name");

                        if (!ModelState.IsValid)
                        {
                            return Page();
                        }

                        httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                        string v = JsonSerializer.Serialize(BookAuthor);
                        StringContent body = new StringContent(v, Encoding.UTF8, "application/json");
                        response = await httpClient.PostAsync($"{Endpoints.BookAuthors}", body);
                        content = response.Content;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
                        }
                    }
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return RedirectToPage(PageRoute.Books);
                }
            }
            catch
            {
            }
            return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
        }
    }

    class Authors
    {
        [JsonPropertyName("value")]
        public List<Author> List { get; set; }
    }
}
