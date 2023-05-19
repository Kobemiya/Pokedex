﻿using System.Collections.Generic;
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
        private readonly ILogger<IndexModel> _logger;
        private readonly IPokemonsRepository _pokemonsRepo;

        public IEnumerable<Pokemon> RegisteredPokemons { get; set; }


        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _pokemonsRepo = new MockPokemonsRepository();
        }
        /*
        public IndexModel(ILogger<IndexModel> logger, IPokemonsRepository pokemonsRepo)
        {
            _logger = logger;
            _pokemonsRepo = pokemonsRepo;
        }*/

        public async Task<IActionResult> OnGet()
        {
            var pokemons = await _pokemonsRepo.GetAll();
            RegisteredPokemons = pokemons != null ? 
                pokemons.Select(pokemon => new Pokemon
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
            
            if (string.IsNullOrEmpty(SearchQuery))
            {
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
            }
            else
            {
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
                }).Where(pokemon => pokemon.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            return Page();
        }
    }
}
