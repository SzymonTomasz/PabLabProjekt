using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace ClientApplication.Pages.Users
{
    public class RegisterAdminPageModel : PageModel
    {
        [BindProperty]
        public RegisterModel UserData { get; set; }

        public IActionResult OnPost()
        {
            var client = new RestClient("https://localhost:7101/api/identity/");
            var request = new RestRequest("RegisterAdmin", Method.Post);
            var body = new
            {
                Username = UserData.Username,
                Email = UserData.Email,
                Password = UserData.Password
            };

            request.AddBody(body);

            var responseResult = client.Execute(request);

            if (responseResult.IsSuccessStatusCode)
                return RedirectToPage("/Index");

            ModelState.AddModelError("API", "Request Failed!");
            return Page();
        }
    }
}
