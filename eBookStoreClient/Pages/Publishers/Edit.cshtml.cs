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

namespace eBookStoreClient.Pages.Publishers
{
    public class EditModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public EditModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public Publisher Publisher { get; set; }

        [TempData]
        public int PublisherId { get; set; }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Publisher.PublisherId = (int)TempData.Peek("PublisherId");
            TempData.Keep("PublisherId");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);

            try
            {
                Publisher = StringTrimmer.TrimPublisher(Publisher);
                string v = JsonSerializer.Serialize(Publisher);
                StringContent body = new StringContent(v, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync($"{Endpoints.Publishers}/{PublisherId}", body);
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Publisher = JsonSerializer.Deserialize<Publisher>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                    return RedirectToPage(PageRoute.Publishers);
                }
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Publishers);
        }
    }
}
