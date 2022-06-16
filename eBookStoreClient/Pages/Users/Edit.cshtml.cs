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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Users
{
    public class EditModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public EditModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public new User User { get; set; }

        [TempData]
        public int UserId { get; set; }

        public List<Publisher> Publishers { get; set; }
        public List<Role> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToPage(PageRoute.Home);
                }

                UserId = (int)id;
                TempData["UserId"] = UserId;

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
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Publishers}");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Publishers = JsonSerializer.Deserialize<Publishers>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                        ViewData["PublisherId"] = new SelectList(Publishers, "PublisherId", "PublisherName");

                        httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                        response = await httpClient.GetAsync($"{Endpoints.Roles}");
                        content = response.Content;

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Roles = JsonSerializer.Deserialize<Roles>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                            ViewData["RoleId"] = new SelectList(Roles, "RoleId", "RoleDesc");
                            httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                            response = await httpClient.GetAsync($"{Endpoints.Users}/{id}");
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
            User.UserId = (int)TempData.Peek("UserId");
            TempData.Keep("UserId");

            try
            {
                HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Publishers}");
                HttpContent content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Publishers = JsonSerializer.Deserialize<Publishers>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                    ViewData["PublisherId"] = new SelectList(Publishers, "PublisherId", "PublisherName");

                    httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    response = await httpClient.GetAsync($"{Endpoints.Roles}");
                    content = response.Content;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Roles = JsonSerializer.Deserialize<Roles>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                        ViewData["RoleId"] = new SelectList(Roles, "RoleId", "RoleDesc");
                    }
                }

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                User.HireDate = User.HireDate.ToUniversalTime();
                User = StringTrimmer.TrimUser(User);
                httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                string v = JsonSerializer.Serialize(User);
                StringContent body = new StringContent(v, Encoding.UTF8, "application/json");
                response = await httpClient.PutAsync($"{Endpoints.Users}/{UserId}", body);
                content = response.Content;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    User = JsonSerializer.Deserialize<User>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);

                    HttpResponseMessage authResponse = await SessionHelper.Current(HttpContext.Session, sessionStorage);
                    content = authResponse.Content;
                    int _memberId = int.Parse(await content.ReadAsStringAsync());
                    if (_memberId == User.UserId)
                    {
                        return RedirectToPage(PageRoute.Profile, new { id = _memberId });
                    }
                    return RedirectToPage(PageRoute.Home);
                }
            }
            catch
            {
            }
            return Page();
        }
    }

    public class Publishers
    {
        [JsonPropertyName("value")]
        public List<Publisher> List { get; set; }
    }

    public class Roles
    {
        [JsonPropertyName("value")]
        public List<Role> List { get; set; }
    }
}
