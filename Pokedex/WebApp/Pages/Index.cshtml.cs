using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public IEnumerable<Pokemon> RegisteredPokemons { get; set; }
        public Task<IEnumerable<User>> RegisteredUsers { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SelectedType { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ShowFavorites { get; set; }
        
        private async Task<IEnumerable<User>> FetchUsersList()
        {
            var usersResponse = await _httpClient.GetAsync("api/User");
            if (!usersResponse.IsSuccessStatusCode)
                return Enumerable.Empty<User>();
            RegisteredUsers = usersResponse.Content.ReadFromJsonAsync<IEnumerable<User>>();
            return await RegisteredUsers;
        }
        
        public IndexModel(IConfiguration defaultConfig)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(defaultConfig.GetValue<String>("apiHost")) };
        }

        private async Task FetchPokemonList()
        {
            var pokemonsResponse = await _httpClient.GetAsync("api/Pokemon");
            if (pokemonsResponse.IsSuccessStatusCode)
                RegisteredPokemons = await pokemonsResponse.Content.ReadFromJsonAsync<IEnumerable<Pokemon>>();
            else
                RegisteredPokemons = Enumerable.Empty<Pokemon>();
        }

        private async Task FilterPokemons()
        {
            SelectedType = HttpContext.Session.GetString("selectedType") ?? String.Empty;
            SearchQuery = HttpContext.Session.GetString("searchQuery") ?? String.Empty;
            ShowFavorites = HttpContext.Session.GetString("showFavorites") == "on";

            if (ShowFavorites)
            {
                var userId = HttpContext.Request.Cookies["user"];
                var users = await FetchUsersList();
                var user = users.FirstOrDefault(u => u.Id.ToString() == userId);
                
                if (user != null)
                    RegisteredPokemons = RegisteredPokemons.Where(pokemon => user.Pokemons.Contains(pokemon.Id));
            }
            
            if (!string.IsNullOrEmpty(SelectedType) && !SelectedType.Equals("Tous les types"))
            {
                RegisteredPokemons = RegisteredPokemons
                    .Where(pokemon => pokemon.Type1.Equals(SelectedType) ||
                                      (pokemon.Type2 != null && pokemon.Type2.Equals(SelectedType)));
            }
            
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                RegisteredPokemons = RegisteredPokemons
                    .Where(pokemon => pokemon.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }
        }

        public async Task<IActionResult> OnGet()
        {
            HttpContext.Session.SetString("selectedType", String.Empty);
            HttpContext.Session.SetString("searchQuery", String.Empty);
            HttpContext.Session.SetString("showFavorites", "false");
            await FetchPokemonList();
            return Page();
        }

        public async Task OnPostSearch([FromForm] string searchQuery, string selectedType, string showFavorites)
        {
            HttpContext.Session.SetString("searchQuery", searchQuery ?? String.Empty);
            HttpContext.Session.SetString("selectedType", selectedType ?? String.Empty);
            HttpContext.Session.SetString("showFavorites", showFavorites ?? String.Empty);
            SearchQuery = searchQuery;
            SelectedType = selectedType;
            ShowFavorites = showFavorites == "on";
            await FetchPokemonList();
            await FilterPokemons();
        }
    }
}
