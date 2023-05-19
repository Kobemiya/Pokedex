using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PokedexBackend.Dbo;
using PokedexBackend.DataAccess.Repositories;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IPokemonsRepository _pokemonsRepo;

    public IEnumerable RegisteredPokemons;

    public IndexModel(ILogger<IndexModel> logger, IPokemonsRepository pokemonsRepo)
    {
        _logger = logger;
        _pokemonsRepo = pokemonsRepo;
    }

    private string GenerateRandom() =>
        new(".....".Select(c => "abcedfghijklmnopqrstuvwxyz0123456789"[new Random().Next(36)]).ToArray());
    
    public async Task<IActionResult> OnGet(long id)
    {
        if (id == 0)
        {
            RegisteredPokemons = await _pokemonsRepo.GetAll();
            HttpContext.Session.SetString("id", GenerateRandom());
            return Page();
        }

        var found = _pokemonsRepo.GetById(id);
        if (found == null)
            return NotFound();
        return Redirect(found.Result.Name);
    }

    public async Task OnPost()
    {/*
        var hash = GenerateRandom();
        while (_pokemonsRepo.GetByHash(hash) != null)
            hash = GenerateRandom();
        await _pokemonsRepo.Insert(new Pokemon
        {
            Url = Request.Form["url"],
            Hash = hash,
            SessionId = HttpContext.Session.GetString("id")!
        });
        RegisteredPokemons = _pokemonsRepo.Get();*/
    }
}