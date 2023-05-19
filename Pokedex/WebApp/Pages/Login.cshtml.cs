using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Repositories;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UsersRepository _usersRepository;
        public string username;
        public string password;

        public LoginModel(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(string username, string password)
        {
            var user = _usersRepository.Authenticate(username, password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            Response.Cookies.Append("AuthCookie", user.Username);

            return RedirectToPage("/");
        }
    }
}