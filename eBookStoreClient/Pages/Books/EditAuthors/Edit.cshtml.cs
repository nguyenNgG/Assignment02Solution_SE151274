using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Books.EditAuthors
{
    public class EditModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public EditModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public BookAuthor BookAuthor { get; set; }

        [TempData]
        public int BookId { get; set; }

        [TempData]
        public int AuthorId { get; set; }

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
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            BookAuthor.BookId = (int)TempData.Peek("BookId");
            BookAuthor.AuthorId = (int)TempData.Peek("AuthorId");
            TempData.Keep("BookId");
            TempData.Keep("AuthorId");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);

            try
            {
                string v = JsonSerializer.Serialize(BookAuthor);
                StringContent body = new StringContent(v, Encoding.UTF8, "application/json");
                // to-do check conflict order
                HttpResponseMessage response = await httpClient.PutAsync($"{Endpoints.BookAuthors}(AuthorId={BookAuthor.AuthorId},BookId={BookAuthor.BookId})", body);
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
                }
            }
            catch
            {
            }
            return RedirectToPage($"{PageRoute.BookAuthors}", new { id = BookId });
        }

    }
}
