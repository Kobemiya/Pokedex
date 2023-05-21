using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic.CompilerServices;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;
    [BindProperty(SupportsGet = true)]
    public IEnumerable<Pokemon> RegisteredPokemons { get; set; }
        
    [BindProperty(SupportsGet = true)]
    public User? CurrentUser { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SearchQuery { get; set; }
        
    [BindProperty(SupportsGet = true)]
    public string SelectedType { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool ShowFavorites { get; set; }
        
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

    private async Task FetchCurrentUser()
    {
        CurrentUser = null;
        if (!HttpContext.Request.Cookies.ContainsKey("user")) return;
        var userResponse = await _httpClient.GetAsync($"api/User/{HttpContext.Request.Cookies["user"]}");
        if (!userResponse.IsSuccessStatusCode) return; 
        CurrentUser = await userResponse.Content.ReadFromJsonAsync<User>();
    }

    private void FilterPokemons()
    {
        SelectedType = HttpContext.Session.GetString("selectedType") ?? String.Empty;
        SearchQuery = HttpContext.Session.GetString("searchQuery") ?? String.Empty;
        ShowFavorites = HttpContext.Session.GetString("showFavorites") == "on";

        if (ShowFavorites && CurrentUser != null)
        {
            RegisteredPokemons = RegisteredPokemons.Where(pokemon => CurrentUser.Pokemons.Contains(pokemon.Id));
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
        HttpContext.Session.SetString("showFavorites", String.Empty);
        await FetchPokemonList();
        await FetchCurrentUser();
        return Page();
    }

    public async Task<IActionResult> OnPostSearch([FromForm] string searchQuery, string selectedType, string showFavorites)
    {
        HttpContext.Session.SetString("searchQuery", searchQuery ?? String.Empty);
        HttpContext.Session.SetString("selectedType", selectedType ?? String.Empty);
        HttpContext.Session.SetString("showFavorites", showFavorites ?? String.Empty);
        SearchQuery = searchQuery;
        SelectedType = selectedType;
        ShowFavorites = showFavorites == "on";
        await FetchPokemonList();
        await FetchCurrentUser();
        FilterPokemons();
        return Page();
    }

    public async Task OnPostAddFavorite([FromForm] string pokemonId)
    {
        await FetchCurrentUser();
        await FetchPokemonList();
        var response = await _httpClient.PutAsync($"api/User/{CurrentUser?.Id}/favorites/{pokemonId}", null);
        if (response.IsSuccessStatusCode && !(CurrentUser?.Pokemons.Contains(long.Parse(pokemonId)) ?? true))
            CurrentUser?.Pokemons.Add(long.Parse(pokemonId));
    }

    public async Task OnPostRemoveFavorite([FromForm] string pokemonId)
    {
        await FetchCurrentUser();
        await FetchPokemonList();
        var response = await _httpClient.DeleteAsync($"api/User/{CurrentUser?.Id}/favorites/{pokemonId}");
        if (response.IsSuccessStatusCode)
            CurrentUser?.Pokemons.Remove(long.Parse(pokemonId));
    }
}