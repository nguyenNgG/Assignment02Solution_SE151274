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
    public class CreateModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public CreateModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public string IdTakenMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    Publisher = SessionHelper.GetFromSession<Publisher>(HttpContext.Session, "Publisher");
                    return Page();
                }
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }

        [BindProperty]
        public Publisher Publisher { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            IdTakenMessage = "";
            if (!ModelState.IsValid)
            {
                SessionHelper.SaveToSession(HttpContext.Session, Publisher, "Publisher");
                return await OnGetAsync();
            }
            try
            {
                Publisher = StringTrimmer.TrimPublisher(Publisher);
                HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                StringContent body = new StringContent(JsonSerializer.Serialize(Publisher), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(Endpoints.Publishers, body);
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    Publisher = JsonSerializer.Deserialize<Publisher>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                    return RedirectToPage(PageRoute.Publishers);
                }
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    IdTakenMessage = "ID is taken.";
                    return await OnGetAsync();
                }
            }
            catch
            {
            }
            SessionHelper.SaveToSession(HttpContext.Session, Publisher, "Publisher");
            return await OnGetAsync();
        }
    }
}
