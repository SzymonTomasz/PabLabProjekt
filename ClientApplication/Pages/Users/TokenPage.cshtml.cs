using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientApplication.Pages.Users
{
    public class TokenPageModel : PageModel
    {
        [BindProperty]
        public string Token { get; set; }

        public void OnGet(string token)
        {
            Token = token;
        }
    }
}
