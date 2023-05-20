using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokedexBackend.Dbo;
using PokedexBackend.DataAccess.Repositories;
using WebApp.MockRepositories;
using WebApp.Pages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPokemonsRepository _pokemonsRepo;
        public IEnumerable<Pokemon> RegisteredPokemons { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SelectedType { get; set; }

        public bool ShowFavorites { get; set; }

        /* Mock data
        public IndexModel()
        {
            _pokemonsRepo = new MockPokemonsRepository();
        }*/
        public IndexModel(IPokemonsRepository pokemonsRepo)
        {
            _pokemonsRepo = pokemonsRepo;
        }

        public async Task<IActionResult> OnGet()
        {
            var pokemons = await _pokemonsRepo.GetAll();
            RegisteredPokemons = pokemons != null ? pokemons.Select(pokemon => new Pokemon
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Def = pokemon.Def,
                DefSpe = pokemon.DefSpe,
                Attack = pokemon.Attack,
                AttackSpe = pokemon.AttackSpe,
                Speed = pokemon.Speed,
                Hp = pokemon.Hp,
                Type1 = pokemon.Type1,
                Type2 = pokemon.Type2,
                Description = pokemon.Description,
                ImagePath = pokemon.ImagePath
            }) : Enumerable.Empty<Pokemon>();

            return Page();
        }

        public async Task<IActionResult> OnPostSearch([FromForm] string searchQuery)
        {
            SearchQuery = searchQuery;
            
            var pokemons = await _pokemonsRepo.GetAll();
            RegisteredPokemons = pokemons.Select(pokemon => new Pokemon
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Def = pokemon.Def,
                DefSpe = pokemon.DefSpe,
                Attack = pokemon.Attack,
                AttackSpe = pokemon.AttackSpe,
                Speed = pokemon.Speed,
                Hp = pokemon.Hp,
                Type1 = pokemon.Type1,
                Type2 = pokemon.Type2,
                Description = pokemon.Description,
                ImagePath = pokemon.ImagePath
            });
            
            if (!string.IsNullOrEmpty(SearchQuery) && !searchQuery.Equals("Tous les types"))
            {
                RegisteredPokemons = pokemons.Select(pokemon => new Pokemon
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    Def = pokemon.Def,
                    DefSpe = pokemon.DefSpe,
                    Attack = pokemon.Attack,
                    AttackSpe = pokemon.AttackSpe,
                    Speed = pokemon.Speed,
                    Hp = pokemon.Hp,
                    Type1 = pokemon.Type1,
                    Type2 = pokemon.Type2,
                    Description = pokemon.Description,
                    ImagePath = pokemon.ImagePath
                }).Where(pokemon => pokemon.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            return Page();
        }
        
        public async Task<IActionResult> OnPostFilter([FromForm] string selectedType)
        {
            SelectedType = selectedType;
            
            var pokemons = await _pokemonsRepo.GetAll();
            RegisteredPokemons = pokemons.Select(pokemon => new Pokemon
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Def = pokemon.Def,
                DefSpe = pokemon.DefSpe,
                Attack = pokemon.Attack,
                AttackSpe = pokemon.AttackSpe,
                Speed = pokemon.Speed,
                Hp = pokemon.Hp,
                Type1 = pokemon.Type1,
                Type2 = pokemon.Type2,
                Description = pokemon.Description,
                ImagePath = pokemon.ImagePath
            });

            if (!string.IsNullOrEmpty(SelectedType))
            {
                RegisteredPokemons = RegisteredPokemons.Where(pokemon =>
                    pokemon.Type1.Equals(SelectedType) || (pokemon.Type2 != null && pokemon.Type2.Equals(SelectedType)));
            }

            return Page();
        }
    }
}
