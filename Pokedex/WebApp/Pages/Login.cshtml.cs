using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        public string username;
        public string password;

        public LoginModel()
        {
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(string username, string password)
        {
            return Page();
        }
    }
}