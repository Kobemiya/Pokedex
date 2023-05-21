using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokedexBackend.DataAccess.EfModels;

namespace WebApp.Pages
{
    public class InfoModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public Pokemon? CurrentPokemon;
        public IEnumerable<Attack> RegisteredAttacks;
        public InfoModel(IConfiguration defaultConfig)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(defaultConfig.GetValue<String>("apiHost")) };
        }

        private async Task FetchCurrentPokemon(long id)
        {
            CurrentPokemon = null;
            var pokemonResponse = await _httpClient.GetAsync($"api/Pokemon/{id}");
            if (!pokemonResponse.IsSuccessStatusCode) return; 
            CurrentPokemon = await pokemonResponse.Content.ReadFromJsonAsync<Pokemon>();
        }

        private async Task FetchAttacklist()
        {
            var attacksResponse = await _httpClient.GetAsync("api/Attack");
            if (attacksResponse.IsSuccessStatusCode)
                RegisteredAttacks = await attacksResponse.Content.ReadFromJsonAsync<IEnumerable<Attack>>();
            else
                RegisteredAttacks = Enumerable.Empty<Attack>();
        }

        public async Task<IActionResult> OnGet(long pokemonId)
        {
            await FetchCurrentPokemon(pokemonId);
            await FetchAttacklist();
            if (CurrentPokemon == null)
                return NotFound();
            return Page();
        }
    }
}
