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

        public Task<IEnumerable<Pokemon>> GetAll(string includeTables = "")
        {
            var pokemons = new List<Pokemon>
            {
                new Pokemon { Id = 1, Name = "Pikachu", Type1 = "Electrique", ImagePath = "/Screenshot_2.jpg"},
                new Pokemon { Id = 2, Name = "Salamèche", Type1 = "Feu", Type2 = "Normal", ImagePath = "/Salameche.png"},
                new Pokemon { Id = 3, Name = "Dummy1", Type1 = "Eau", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 4, Name = "Dummy2", Type1 = "Acier", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 5, Name = "Dummy3", Type1 = "Ténèbres", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 6, Name = "Dummy4", Type1 = "Spectre", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 7, Name = "Dummy5", Type1 = "Dragon", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 8, Name = "Dummy6", Type1 = "Insecte", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 9, Name = "Dummy7", Type1 = "Glace", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 10, Name = "Dummy8", Type1 = "Roche", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 11, Name = "Dummy9", Type1 = "Sol", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 12, Name = "Dummy10", Type1 = "Poison", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 13, Name = "Dummy11", Type1 = "Vol", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 14, Name = "Dummy12", Type1 = "Psy", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 15, Name = "Dummy13", Type1 = "Electrique", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 16, Name = "Dummy14", Type1 = "Combat", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 17, Name = "Dummy15", Type1 = "Normal", ImagePath = "/Screenshot_2.jpg" },
                new Pokemon { Id = 18, Name = "Dummy16", Type1 = "Plante", ImagePath = "/Screenshot_2.jpg" }
            };

            return Task.FromResult<IEnumerable<Pokemon>>(pokemons);
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
