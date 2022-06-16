using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Users
{
    public class DetailsModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public DetailsModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public new User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToPage(PageRoute.Home);
                }

                HttpResponseMessage authResponse = await SessionHelper.Current(HttpContext.Session, sessionStorage);
                HttpContent content = authResponse.Content;
                int _memberId = int.Parse(await content.ReadAsStringAsync());
                if (_memberId == id)
                {
                    authResponse.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                }
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Users}/{id}?$expand=Role,Publisher");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        User = JsonSerializer.Deserialize<User>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                        return Page();
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToPage(PageRoute.Home);
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
