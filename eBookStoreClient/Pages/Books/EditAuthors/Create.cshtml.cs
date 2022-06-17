using BusinessObject;
using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Books.EditAuthors
{
    public class CreateModel : PageModel
    {
        HttpSessionStorage sessionStorage;
        public CreateModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        [BindProperty]
        public BookAuthor BookAuthor { get; set; }

        public List<Author> Authors { get; set; }

        [TempData]
        public int BookId { get; set; }

        public async Task<ActionResult> OnGetAsync(int? bookId)
        {
            try
            {
                if (bookId == null)
                {
                    return RedirectToPage(PageRoute.Books);
                }

                BookId = (int)bookId;
                TempData["BookId"] = BookId;
                BookAuthor.BookId = BookId;

                HttpResponseMessage authResponse = await SessionHelper.Authorize(HttpContext.Session, sessionStorage);
                if (authResponse.StatusCode == HttpStatusCode.OK)
                {
                    //
                }
            }
            catch
            {
            }
            return RedirectToPage(PageRoute.Login);
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
