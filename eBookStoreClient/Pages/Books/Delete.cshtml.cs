using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Books
{
    public class DeleteModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public DeleteModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public int BookId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToPage(PageRoute.Books);
                }

                BookId = (int)id;
                TempData["BookId"] = BookId;

                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Books}/{id}?$expand=Publisher,BookAuthors($expand=Author)");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Book = JsonSerializer.Deserialize<Book>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                        return Page();
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToPage(PageRoute.Books);
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
            Book.BookId = (int)TempData.Peek("BookId");
            TempData.Keep("BookId");

            try
            {
                HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                HttpResponseMessage response = await httpClient.DeleteAsync($"{Endpoints.Books}/{BookId}");
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToPage(PageRoute.Books);
                }
                return await OnGetAsync(Book.BookId);
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Books);
        }
    }
}
