using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using eStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Authors
{
    public class EditModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public EditModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public Author Author { get; set; }

        [TempData]
        public int AuthorId { get; set; }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Author.AuthorId = (int)TempData.Peek("AuthorId");
            TempData.Keep("AuthorId");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);

            try
            {
                Author = StringTrimmer.TrimAuthor(Author);
                string v = JsonSerializer.Serialize(Author);
                StringContent body = new StringContent(v, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync($"{Endpoints.Authors}/{AuthorId}", body);
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Author = JsonSerializer.Deserialize<Author>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                    return RedirectToPage(PageRoute.Authors);
                }
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Authors);
        }
    }
}
