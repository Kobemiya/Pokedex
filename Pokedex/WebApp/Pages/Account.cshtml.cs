using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class AccountModel : PageModel
{
    private readonly HttpClient _httpClient;
    public string Username;
        
    public AccountModel(IConfiguration defaultConfig)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(defaultConfig.GetValue<String>("apiHost")) };
    }
    
    public async Task<IActionResult> OnGet()
    {
        if (!Request.Cookies.TryGetValue("user", out string userId)) return Redirect("Login");
        
        var userResponse = await _httpClient.GetAsync($"api/User/{userId}");
        if (!userResponse.IsSuccessStatusCode) return Redirect("Login");
        
        var user = await userResponse.Content.ReadFromJsonAsync<User>();
        if (user == null) return Redirect("Index");

        Username = user.Username;
        return Page();
    }

    public IActionResult OnPostLogout()
    {
        if (Request.Cookies.ContainsKey("user"))
            Response.Cookies.Append("user", "-1", new CookieOptions{ Expires = DateTime.Now.AddDays(-1d) });
        return Redirect("Index");
    }
}