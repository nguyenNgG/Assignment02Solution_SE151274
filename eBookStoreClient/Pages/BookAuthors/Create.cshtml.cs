using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Models;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.BookAuthors
{
    public class CreateModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public CreateModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public string AuthorMessage { get; set; }
        public string OrderMessage { get; set; }
        public string RoyaltyMessage { get; set; }

        [BindProperty]
        public Author Author { get; set; }

        [BindProperty]
        public CartDetail CartDetail { get; set; }

        public List<Author> Authors { get; set; }

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
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Authors}");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var str = await content.ReadAsStringAsync();
                        Authors = JsonSerializer.Deserialize<Authors>(str, SerializerOptions.CaseInsensitive).List;
                        var authors = Authors.Select(a => new
                        {
                            AuthorId = a.AuthorId,
                            Name = $"{a.FirstName} {a.LastName}"
                        }).ToList();
                        ViewData["AuthorId"] = new SelectList(authors, "AuthorId", "Name");
                        return Page();
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToPage(PageRoute.Books);
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
                    HttpResponseMessage response = await httpClient.GetAsync($"{Endpoints.Authors}");
                    content = response.Content;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Authors = JsonSerializer.Deserialize<Authors>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive).List;
                        var authors = Authors.Select(a => new
                        {
                            AuthorId = a.AuthorId,
                            Name = $"{a.FirstName} {a.LastName}"
                        }).ToList();
                        ViewData["AuthorId"] = new SelectList(authors, "AuthorId", "Name");

                        response = await httpClient.GetAsync($"{Endpoints.Cart}");
                        content = response.Content;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Cart cart = JsonSerializer.Deserialize<Cart>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);

                            bool isExisted = cart.CartDetails.Where(cartDetail => cartDetail.Author.AuthorId == Author.AuthorId).Any();
                            if (isExisted)
                            {
                                AuthorMessage = "Author has already been assigned.";
                                return Page();
                            }
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
                            response = await httpClient.GetAsync($"{Endpoints.Authors}/{Author.AuthorId}");
                            content = response.Content;
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                Author author = JsonSerializer.Deserialize<Author>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                                CartDetail.Author = author;
                                cart.CartDetails.Add(CartDetail);
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
                                return Page();
                            }
                        }
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToPage(PageRoute.Books);
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

    class Authors
    {
        [JsonPropertyName("value")]
        public List<Author> List { get; set; }
    }
}
