using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using eStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Books
{
    public class EditModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public EditModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public int BookId { get; set; }
        public List<Publisher> Publishers { get; set; }

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
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Publishers}");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Publishers = JsonSerializer.Deserialize<Publishers>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                        ViewData["PublisherId"] = new SelectList(Publishers, "PublisherId", "PublisherName");

                        httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                        response = await httpClient.GetAsync($"{Endpoints.Books}/{id}");
                        content = response.Content;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Book = JsonSerializer.Deserialize<Book>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                            return Page();
                        }
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            return RedirectToPage(PageRoute.Books);
                        }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Book.BookId = (int)TempData.Peek("BookId");
            TempData.Keep("BookId");

            try
            {
                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Publishers}");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Publishers = JsonSerializer.Deserialize<Publishers>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                        ViewData["PublisherId"] = new SelectList(Publishers, "PublisherId", "PublisherName");
                        if (!ModelState.IsValid)
                        {
                            return Page();
                        }

                        Book = StringTrimmer.TrimBook(Book);
                        Book.PublishedDate = Book.PublishedDate.ToUniversalTime();
                        StringContent bookBody = new StringContent(JsonSerializer.Serialize(Book), Encoding.UTF8, "application/json");
                        response = await httpClient.PutAsync($"{Endpoints.Books}/{BookId}", bookBody);

                        return RedirectToPage(PageRoute.Books);
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
    }
}
