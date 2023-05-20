using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public Task<IEnumerable<Pokemon>> RegisteredPokemons { get; set; }
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

        private async Task<IEnumerable<Pokemon>> FetchPokemonList()
        {
            var pokemonsResponse = await _httpClient.GetAsync("api/Pokemon");
            if (!pokemonsResponse.IsSuccessStatusCode)
                return Enumerable.Empty<Pokemon>();
            RegisteredPokemons = pokemonsResponse.Content.ReadFromJsonAsync<IEnumerable<Pokemon>>();
            return await RegisteredPokemons;
        }

        private async Task FilterPokemons(bool showFavorites)
        {
            var pokemons = await RegisteredPokemons;
            
            if (showFavorites)
            {
                var userId = HttpContext.Request.Cookies["user"];
                var users = await FetchUsersList();
                var user = users.FirstOrDefault(u => u.Id.ToString() == userId);
                
                if (user != null)
                {
                    RegisteredPokemons = Task.FromResult(pokemons.Where(pokemon => user.Pokemons.Contains(pokemon.Id)));
                }
            }
            
            if (!string.IsNullOrEmpty(SelectedType) && !SelectedType.Equals("Tous les types"))
            {
                RegisteredPokemons = Task.FromResult(pokemons.Where(pokemon =>
                    pokemon.Type1.Equals(SelectedType) || (pokemon.Type2 != null && pokemon.Type2.Equals(SelectedType))));
            }
            
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                RegisteredPokemons = Task.FromResult(pokemons
                    .Where(pokemon => pokemon.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)));
            }
        }

        public async Task<IActionResult> OnGet()
        {
            await FetchPokemonList();
            return Page();
        }

        public async Task OnPostSearch([FromForm] string searchQuery)
        {
            SearchQuery = searchQuery;
            await FetchPokemonList();
            await FilterPokemons(ShowFavorites);
        }
        
        public async Task OnPostFilter([FromForm] string selectedType)
        {
            SelectedType = selectedType;
            await FetchPokemonList();
            await FilterPokemons(ShowFavorites);
        }
        
        public async Task OnPostFavorites([FromForm] string showFavorites)
        {
            ShowFavorites = !showFavorites.IsNullOrEmpty();
            await FetchPokemonList();
            await FilterPokemons(ShowFavorites);
        }
    }
}
