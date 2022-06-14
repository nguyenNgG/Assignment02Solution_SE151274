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
    public class CreateModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public CreateModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    Author = SessionHelper.GetFromSession<Author>(HttpContext.Session, "Author");
                    return Page();
                }
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }

        [BindProperty]
        public Author Author { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Author = StringTrimmer.TrimAuthor(Author);
            if (!ModelState.IsValid)
            {
                SessionHelper.SaveToSession(HttpContext.Session, Author, "Author");
                return await OnGetAsync();
            }
            try
            {
                HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                StringContent body = new StringContent(JsonSerializer.Serialize(Author), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(Endpoints.Authors, body);
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    Author = JsonSerializer.Deserialize<Author>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                    return RedirectToPage(PageRoute.Authors);
                }
            }
            catch
            {
            }
            SessionHelper.SaveToSession(HttpContext.Session, Author, "Author");
            return await OnGetAsync();
        }
    }
}
