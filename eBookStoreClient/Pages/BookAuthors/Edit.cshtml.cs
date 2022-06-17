using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Models;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.BookAuthors
{
    public class EditModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public EditModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public string OrderMessage { get; set; }
        public string RoyaltyMessage { get; set; }

        [FromQuery(Name = "item-index")] public int ItemIndex { get; set; } = -1;
        [BindProperty]
        public CartDetail CartDetail { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                HttpResponseMessage authResponse = await SessionHelper.Current(HttpContext.Session, sessionStorage);
                HttpContent content = authResponse.Content;
                int _memberId = int.Parse(await content.ReadAsStringAsync());
                authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Cart}");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Cart cart = JsonSerializer.Deserialize<Cart>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                        int itemCount = cart.CartDetails.Count;
                        if (ItemIndex <= itemCount && ItemIndex >= 0)
                        {
                            CartDetail = cart.CartDetails[ItemIndex];
                            return Page();
                        }
                        return RedirectToPage(PageRoute.Cart);
                    }
                }
                return RedirectToPage(PageRoute.Books);
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }

        public async Task<ActionResult> OnPostAsync()
        {
            try
            {
                HttpResponseMessage authResponse = await SessionHelper.Current(HttpContext.Session, sessionStorage);
                HttpContent content = authResponse.Content;
                int _memberId = int.Parse(await content.ReadAsStringAsync());
                authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpClient httpClient = SessionHelper.GetHttpClient(HttpContext.Session, sessionStorage);
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Cart}");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Cart cart = JsonSerializer.Deserialize<Cart>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                        int itemCount = cart.CartDetails.Count;
                        if (ItemIndex > itemCount && ItemIndex < 0)
                        {
                            return RedirectToPage(PageRoute.Cart);
                        }
                        CartDetail currentDetail = cart.CartDetails[ItemIndex];
                        response = await httpClient.GetAsync($"{Endpoints.Authors}/{currentDetail.Author.AuthorId}");
                        content = response.Content;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Author author = JsonSerializer.Deserialize<Author>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                            bool invalidOrder = CartDetail.AuthorOrder < 0;
                            if (invalidOrder)
                            {
                                OrderMessage = "Order can't be lower than 0.";
                                return Page();
                            }
                            bool invalidRoyaltyPercentage = CartDetail.RoyaltyPercentage < 0;
                            if (invalidRoyaltyPercentage)
                            {
                                RoyaltyMessage = "Royalty percentage can't be lower than 0.";
                                return Page();
                            }
                            cart.CartDetails[ItemIndex].AuthorOrder = CartDetail.AuthorOrder;
                            cart.CartDetails[ItemIndex].RoyaltyPercentage = CartDetail.RoyaltyPercentage;
                            StringContent body = new StringContent(JsonSerializer.Serialize(cart), Encoding.UTF8, "application/json");
                            response = await httpClient.PostAsync(Endpoints.Cart, body);
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                return RedirectToPage(PageRoute.Cart);
                            }
                            return Page();
                        }
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            return RedirectToPage(PageRoute.Cart);
                        }
                        return RedirectToPage(PageRoute.Cart);
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
