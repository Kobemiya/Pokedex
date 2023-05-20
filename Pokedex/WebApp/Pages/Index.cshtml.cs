using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public Task<IEnumerable<Pokemon>> RegisteredPokemons { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SelectedType { get; set; }

        public bool ShowFavorites { get; set; }

        public IndexModel(IConfiguration defaultConfig)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(defaultConfig.GetValue<String>("apiHost")) };
        }

        private async Task FetchPokemonList()
        {
            var pokemonsResponse = await _httpClient.GetAsync("api/Pokemon");
            if (!pokemonsResponse.IsSuccessStatusCode) return;
            RegisteredPokemons = pokemonsResponse.Content.ReadFromJsonAsync<IEnumerable<Pokemon>>();
        }

        private async Task FilterPokemons()
        {
            var pokemons = await RegisteredPokemons;
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
            await FilterPokemons();
        }
        
        public async Task OnPostFilter([FromForm] string selectedType)
        {
            SelectedType = selectedType;
            await FetchPokemonList();
            await FilterPokemons();
        }
    }
}