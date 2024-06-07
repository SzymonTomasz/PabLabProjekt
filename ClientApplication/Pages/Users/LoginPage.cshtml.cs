using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace ClientApplication.Pages.Users
{
    public class LoginPageModel : PageModel
    {
        [BindProperty] 
        public LoginModel UserData { get; set; }

        public IActionResult OnPost()
        {

            var client = new RestClient("https://localhost:7101/api/identity/"); 
            var request = new RestRequest("login", Method.Post); 
            var body = new
            {
                Username = UserData.Username,
                Password = UserData.Password
            }; 

            request.AddBody(body);

            var responseResult = client.Execute(request); 

            if (responseResult.IsSuccessStatusCode) 
                return RedirectToPage("/Users/TokenPage", new { token = responseResult.Content });


            ModelState.AddModelError("API", "Request Failed!"); 
            return Page(); 
        }
    }
}
