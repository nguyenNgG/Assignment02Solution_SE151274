using eBookStoreClient.Constants;
using eBookStoreClient.Models;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Books
{
    public class PrepareModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public PrepareModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public string Message { get; set; }
        public string HasItems { get; set; }
        public Cart Cart { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Cart}");
                    HttpContent content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Cart = JsonSerializer.Deserialize<Cart>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                        Message = (Cart.CartDetails.Count <= 0) ? "No authors have been assigned to book." : "";
                        HasItems = (Cart.CartDetails.Count <= 0) ? "disabled" : "";
                        return Page();
                    }
                }
                return RedirectToPage(PageRoute.Books);
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }
    }
}
