using eBookStoreClient.Constants;
using eBookStoreClient.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace eBookStoreClient.Pages.Members
{
    public class LogoutModel : PageModel
    {
        HttpSessionStorage sessionStorage;

        public LogoutModel(HttpSessionStorage _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        public async Task<ActionResult> OnGet()
        {
            HttpResponseMessage response = await SessionHelper.Authenticate(HttpContext.Session, sessionStorage);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Page();
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return RedirectToPage(PageRoute.Login);
            }

            return RedirectToPage(PageRoute.Login);
        }

        public ActionResult OnPost()
        {
            SessionHelper.GetNewHttpClient(HttpContext.Session, sessionStorage);
            return RedirectToPage(PageRoute.Login);
        }
    }
}
