using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Models;
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

namespace eBookStoreClient.Pages.Books
{
    public class CreateModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public CreateModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public Book Book { get; set; }
        public Cart Cart { get; set; }
        public List<Publisher> Publishers { get; set; }

        public string IdTakenMessage { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
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

                        response = await httpClient.GetAsync($"{Endpoints.Cart}");
                        content = response.Content;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Cart = JsonSerializer.Deserialize<Cart>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                            if (Cart.CartDetails.Count <= 0)
                            {
                                return RedirectToPage(PageRoute.BooksPrepare);
                            }
                            return Page();
                        }
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                IdTakenMessage = "";
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

                        response = await httpClient.GetAsync($"{Endpoints.Cart}");
                        content = response.Content;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Cart = JsonSerializer.Deserialize<Cart>(await content.ReadAsStringAsync(), SerializerOptions.CaseInsensitive);
                            if (Cart.CartDetails.Count <= 0)
                            {
                                return RedirectToPage(PageRoute.BooksPrepare);
                            }

                            if (!ModelState.IsValid)
                            {
                                return Page();
                            }

                            foreach (var detail in Cart.CartDetails)
                            {
                                BookAuthor bookAuthor = new()
                                {
                                    AuthorId = detail.Author.AuthorId,
                                    AuthorOrder = detail.AuthorOrder,
                                    RoyaltyPercentage = detail.RoyaltyPercentage,
                                };
                                Book.BookAuthors.Add(bookAuthor);
                            }
                            Book = StringTrimmer.TrimBook(Book);
                            Book.PublishedDate = Book.PublishedDate.ToUniversalTime();
                            StringContent bookBody = new StringContent(JsonSerializer.Serialize(Book), Encoding.UTF8, "application/json");
                            response = await httpClient.PostAsync($"{Endpoints.Books}", bookBody);

                            if (response.StatusCode == HttpStatusCode.Conflict)
                            {
                                IdTakenMessage = "ID is taken.";
                                return Page();
                            }

                            if (response.StatusCode == HttpStatusCode.BadRequest)
                            {
                                return Page();
                            }

                            if (response.StatusCode == HttpStatusCode.Created)
                            {
                                Cart.CartDetails.Clear();
                                StringContent cartBody = new StringContent(JsonSerializer.Serialize(Cart), Encoding.UTF8, "application/json");
                                response = await httpClient.PostAsync(Endpoints.Cart, cartBody);

                                return RedirectToPage(PageRoute.Books);
                            }
                        }
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

    class Publishers
    {
        [JsonPropertyName("value")]
        public List<Publisher> List { get; set; }
    }
}
