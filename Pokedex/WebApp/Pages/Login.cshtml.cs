using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        
        public LoginModel(IConfiguration defaultConfig)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(defaultConfig.GetValue<String>("apiHost")) };
        }

        public async Task<IActionResult> OnPostLogin([FromForm] string username, string password)
        {
            var usersResponse = await _httpClient.GetAsync("api/user");
            if (!usersResponse.IsSuccessStatusCode) return Page();
            
            var users = await usersResponse.Content.ReadFromJsonAsync<IEnumerable<User>>() ?? Enumerable.Empty<User>();
            var found = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (found == null) return Page();
            
            Response.Cookies.Append("user", found.Id.ToString(), new CookieOptions{ Expires = DateTime.Now.AddHours(12) });
            return Redirect("Index");
        }

        public async Task<IActionResult> OnPostSignup([FromForm] string username, string password)
        {
            HttpContent body = new StringContent($$"""{ "username": "{{username}}", "password": "{{password}}" }""", Encoding.UTF8, "application/json");
            var userResponse = await _httpClient.PostAsync("api/User", body);
            if (!userResponse.IsSuccessStatusCode) return Page();
            
            var user = await userResponse.Content.ReadFromJsonAsync<User>();
            if (user == null) return Page();
            
            Response.Cookies.Append("user", user.Id.ToString(), new CookieOptions{ Expires = DateTime.Now.AddHours(12) });
            return Redirect("Index");
        }
    }
}